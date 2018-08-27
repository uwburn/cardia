using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using OfficeOpenXml;

namespace MGT.HRM.HRP
{
    public class BtHrpLoggerXLSX : BtHrpFileLogger
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

        protected override void BtHrpStart()
        {
            stream = new FileStream(fileName, FileMode.Create);
            excelPackage = new ExcelPackage(stream);
            excelWorksheet = excelPackage.Workbook.Worksheets.Add("Blueatooth HRP log");

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
        }

        protected override void BtHrpStop()
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

        protected override void BtHrpLog(BtHrpPacket btHrpPacket)
        {
            DateTime timestamp = DateTime.Now;

            heartbeats = btHrp.HeartBeats;

            if (btHrp.MinHeartRate < min || min == null)
            {
                min = btHrp.MinHeartRate;
                minTime = timestamp;
            }
            if (btHrp.MaxHeartRate > max || max == null)
            {
                max = btHrp.MaxHeartRate;
                maxTime = timestamp;
            }

            if (!running)
                return;

            write(btHrpPacket, timestamp);
        }

        private void write(BtHrpPacket btHrpPacket, DateTime timestamp)
        {
            excelWorksheet.Cells[row, 1].Value = timestamp;
            excelWorksheet.Cells[row, 1].Style.Numberformat.Format = dateFormat;
            excelWorksheet.Cells[row, 2].Value = btHrpPacket.HeartRate;

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
