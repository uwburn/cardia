using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using OfficeOpenXml;

namespace MGT.HRM.Zephyr_HxM
{
    public class ZephyrHxMLoggerXLSX : ZephyrHxMFileLogger
    {
        Stream stream;
        ExcelPackage excelPackage;
        ExcelWorksheet excelWorksheet;
        string dateFormat;

        int heartbeats;
        byte? min;
        DateTime minTime;
        byte? max;
        DateTime maxTime;
        int row = 10;

        protected override void ZephyrStart()
        {
            stream = new FileStream(fileName, FileMode.Create);
            excelPackage = new ExcelPackage(stream);
            excelWorksheet = excelPackage.Workbook.Worksheets.Add("Zephyr HxM log");

            dateFormat = System.Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat.LongTimePattern;

            excelWorksheet.Cells[1, 1].Value = "Start time";
            excelWorksheet.Cells[1, 1].Style.Font.Bold = true;
            excelWorksheet.Cells[1, 2].Value = DateTime.Now;
            excelWorksheet.Cells[1, 2].Style.Numberformat.Format = dateFormat;

            excelWorksheet.Cells[2, 1].Value = "End time";
            excelWorksheet.Cells[2, 1].Style.Font.Bold = true;

            excelWorksheet.Cells[4, 1].Value = "Heartbeats";
            excelWorksheet.Cells[4, 1].Style.Font.Bold = true;

            excelWorksheet.Cells[6, 1].Value = "Min Heartrate";
            excelWorksheet.Cells[6, 1].Style.Font.Bold = true;
            excelWorksheet.Cells[7, 1].Value = "Max Heartrate";
            excelWorksheet.Cells[7, 1].Style.Font.Bold = true;

            excelWorksheet.Cells[9, 1].Value = "Time";
            excelWorksheet.Cells[9, 1].Style.Font.Bold = true;
            excelWorksheet.Cells[9, 2].Value = "Heartrate";
            excelWorksheet.Cells[9, 2].Style.Font.Bold = true;
            excelWorksheet.Cells[9, 3].Value = "RR Interval";
            excelWorksheet.Cells[9, 3].Style.Font.Bold = true;
        }

        protected override void ZephyrStop()
        {
            excelWorksheet.Cells[2, 2].Value = DateTime.Now;
            excelWorksheet.Cells[2, 2].Style.Numberformat.Format = dateFormat;

            excelWorksheet.Cells[4, 2].Value = heartbeats;

            excelWorksheet.Cells[6, 2].Value = minTime;
            excelWorksheet.Cells[6, 2].Style.Numberformat.Format = dateFormat;
            excelWorksheet.Cells[6, 3].Value = min;
            excelWorksheet.Cells[7, 2].Value = maxTime;
            excelWorksheet.Cells[7, 2].Style.Numberformat.Format = dateFormat;
            excelWorksheet.Cells[7, 3].Value = max;

            excelWorksheet.Column(1).AutoFit();
            excelWorksheet.Column(2).AutoFit();
            excelWorksheet.Column(3).AutoFit();

            Dispose();
        }

        protected override void ZephyrLog(ZephyrPacket zephyrPacket)
        {
            DateTime timestamp = DateTime.Now;

            heartbeats = zephyrHxM.HeartBeats;

            if (zephyrHxM.MinHeartRate < min || min == null)
            {
                min = zephyrHxM.MinHeartRate;
                minTime = timestamp;
            }
            if (zephyrHxM.MaxHeartRate > max || max == null)
            {
                max = zephyrHxM.MaxHeartRate;
                maxTime = timestamp;
            }

            if (!running)
                return;

            for (int i = 0; i < zephyrHxM.NewRRIntervals.Count; i++)
            {
                write(zephyrPacket, i, timestamp);
            }
        }

        private void write(ZephyrPacket zephyrPacket, int index, DateTime timestamp)
        {
            excelWorksheet.Cells[row, 1].Value = timestamp;
            excelWorksheet.Cells[row, 1].Style.Numberformat.Format = dateFormat;
            excelWorksheet.Cells[row, 2].Value = zephyrPacket.HeartRate;
            excelWorksheet.Cells[row, 3].Value = zephyrPacket.rrIntervals[index];

            row++;
        }

        public override void Dispose()
        {
            excelPackage.Save();
            stream.Close();
            stream.Dispose();
        }
    }
}
