using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using OfficeOpenXml;

namespace MGT.HRM.CMS50
{
    public class CMS50LoggerXLSX : CMS50Logger
    {
        Stream stream;
        ExcelPackage excelPackage;
        ExcelWorksheet excelWorksheet;
        string dateFormat;

        int heartbeats;
        byte? minHeartRate;
        DateTime minHeartRateTime;
        byte? maxHeartRate;
        DateTime maxHeartRateTime;
        byte? minSpO2;
        DateTime minSpO2Time;
        byte? maxSpO2;
        DateTime maxSpO2Time;
        int row = 13;

        protected override void CMS50Start()
        {
            stream = new FileStream(fileName, FileMode.Create);
            excelPackage = new ExcelPackage(stream);
            excelWorksheet = excelPackage.Workbook.Worksheets.Add("CMS50 log");

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

            excelWorksheet.Cells[9, 1].Value = "Min SpO2";
            excelWorksheet.Cells[9, 1].Style.Font.Bold = true;
            excelWorksheet.Cells[10, 1].Value = "Max SpO2";
            excelWorksheet.Cells[10, 1].Style.Font.Bold = true;

            excelWorksheet.Cells[12, 1].Value = "Time";
            excelWorksheet.Cells[12, 1].Style.Font.Bold = true;
            excelWorksheet.Cells[12, 2].Value = "Heartrate";
            excelWorksheet.Cells[12, 2].Style.Font.Bold = true;
            excelWorksheet.Cells[12, 3].Value = "SpO2";
            excelWorksheet.Cells[12, 3].Style.Font.Bold = true;
            excelWorksheet.Cells[12, 4].Value = "Waveform";
            excelWorksheet.Cells[12, 4].Style.Font.Bold = true;
        }

        protected override void CMS50Stop()
        {
            excelWorksheet.Cells[2, 2].Value = DateTime.Now;
            excelWorksheet.Cells[2, 2].Style.Numberformat.Format = dateFormat;

            excelWorksheet.Cells[4, 2].Value = heartbeats;

            excelWorksheet.Cells[6, 2].Value = minHeartRateTime;
            excelWorksheet.Cells[6, 2].Style.Numberformat.Format = dateFormat;
            excelWorksheet.Cells[6, 3].Value = minHeartRate;
            excelWorksheet.Cells[7, 2].Value = maxHeartRateTime;
            excelWorksheet.Cells[7, 2].Style.Numberformat.Format = dateFormat;
            excelWorksheet.Cells[7, 3].Value = maxHeartRate;

            excelWorksheet.Cells[9, 2].Value = minSpO2Time;
            excelWorksheet.Cells[9, 2].Style.Numberformat.Format = dateFormat;
            excelWorksheet.Cells[9, 3].Value = minSpO2;
            excelWorksheet.Cells[10, 2].Value = maxSpO2Time;
            excelWorksheet.Cells[10, 2].Style.Numberformat.Format = dateFormat;
            excelWorksheet.Cells[10, 3].Value = maxSpO2;

            excelWorksheet.Column(1).AutoFit();
            excelWorksheet.Column(2).AutoFit();
            excelWorksheet.Column(3).AutoFit();
            excelWorksheet.Column(4).AutoFit();

            Dispose();
        }

        protected override void CMS50Log(CMS50Packet cms50Packet)
        {
            DateTime timestamp = DateTime.Now;

            heartbeats = cms50.HeartBeats;

            if (cms50.MinHeartRate < minHeartRate || minHeartRate == null)
            {
                minHeartRate = cms50.MinHeartRate;
                minHeartRateTime = timestamp;
            }
            if (cms50.MaxHeartRate > maxHeartRate || maxHeartRate == null)
            {
                maxHeartRate = cms50.MaxHeartRate;
                maxHeartRateTime = timestamp;
            }

            if (cms50.MinSpO2 < minSpO2 || minSpO2 == null)
            {
                minSpO2 = cms50.MinSpO2;
                minSpO2Time = timestamp;
            }
            if (cms50.MaxSpO2 > maxSpO2 || maxSpO2 == null)
            {
                maxSpO2 = cms50.MaxSpO2;
                maxSpO2Time = timestamp;
            }

            if (!running)
                return;

            excelWorksheet.Cells[row, 1].Value = timestamp;
            excelWorksheet.Cells[row, 1].Style.Numberformat.Format = dateFormat;
            excelWorksheet.Cells[row, 2].Value = cms50Packet.HeartRate;
            excelWorksheet.Cells[row, 3].Value = cms50Packet.SpO2;
            excelWorksheet.Cells[row, 4].Value = cms50Packet.Waveform;

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
