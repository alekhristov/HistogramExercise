using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;

namespace HistogramExercise
{
    // One histogram for every picture. Fill all data from the histogram in the excell.
    class Histogram
    {
        public void CreateExcelFile()
        {
            Application ExcelApp = new Application();
            Workbook ExcelWorkBook = null;
            Worksheet ExcelWorkSheet = null;

            ExcelApp.Visible = true;
            ExcelWorkBook = ExcelApp.Workbooks.Add(XlWBATemplate.xlWBATWorksheet);
            // ExcelWorkBook.Worksheets.Add(); //Adding New Sheet in Excel Workbook
            try
            {
                ExcelWorkSheet = ExcelWorkBook.Worksheets[1]; // Compulsory Line in which sheet you want to write data
                ExcelWorkSheet.Cells[1, 1] = "Name";
                ExcelWorkSheet.Cells[1, 2] = "TopColor";
                ExcelWorkSheet.Cells[1, 3] = "Histogram";

                //for (int i = 0; i <= 255; i++)
                //{
                //    ExcelWorkSheet.Cells[i+2, 1] = i;
                //}

                ExcelWorkBook.SaveAs(@"C:\Users\alek.hristov\Desktop\HistogramTask\ExcelFile.xlsx");
                ExcelWorkBook.Close();
                ExcelApp.Quit();
                Marshal.ReleaseComObject(ExcelWorkSheet);
                Marshal.ReleaseComObject(ExcelWorkBook);
                Marshal.ReleaseComObject(ExcelApp);
            }
            catch (Exception exHandle)
            {
                Console.WriteLine("Exception: " + exHandle.Message);
                Console.ReadLine();
            }
            finally
            {
                foreach (Process process in Process.GetProcessesByName("Excel"))
                    process.Kill();
            }

        }

        public void FillDataInExcelFile(string imageTopColor, string name)
        {
            var index = name.LastIndexOf(@"\");
            var imageName = name.Substring(index + 1);

            string fileName = @"C:\Users\alek.hristov\Desktop\HistogramTask\ExcelFile.xlsx";
            string connectionString = String.Format(@"Provider=Microsoft.ACE.OLEDB.12.0;" +
                    "Data Source={0};Extended Properties='Excel 12.0;HDR=YES;IMEX=0'", fileName);

            using (OleDbConnection cn = new OleDbConnection(connectionString))
            {
                cn.Open();
                OleDbCommand cmd1 = new OleDbCommand("INSERT INTO [Sheet1$] " +
                     "(Name,TopColor) " +
                     "VALUES(@value1,@value2)", cn);
                cmd1.Parameters.AddWithValue("@value1", imageName);
                cmd1.Parameters.AddWithValue("@value2", imageTopColor);
                cmd1.ExecuteNonQuery();
            }
        }

        public void CreateColorHistrogram(string pictureName, List<string> listOfHistogramPaths)
        {
            Bitmap bmp = new Bitmap(pictureName);
            int[] histogram = new int[256];
            float max = 0;

            for (int i = 0; i < bmp.Width; i++)
            {
                for (int j = 0; j < bmp.Height; j++)
                {
                    double redValue = bmp.GetPixel(i, j).R;
                    double greenValue = bmp.GetPixel(i, j).G;
                    double blueValue = bmp.GetPixel(i, j).B;

                    var regularHistogram = Math.Round((redValue + greenValue + blueValue) / 3);

                    histogram[(int)regularHistogram]++;

                    if (max < histogram[(int)regularHistogram])
                    {
                        max = histogram[(int)regularHistogram];
                    }
                }
            }

            int histHeight = 128;
            Bitmap img = new Bitmap(256, histHeight + 10);
            using (Graphics g = Graphics.FromImage(img))
            {
                for (int i = 0; i < histogram.Length; i++)
                {
                    float pct = histogram[i] / max;   // What percentage of the max is this value?
                    g.DrawLine(Pens.Black,
                        new System.Drawing.Point(i, img.Height - 5),
                        new System.Drawing.Point(i, img.Height - 5 - (int)(pct * histHeight))  // Use that percentage of the height
                        );
                }
            }
            var histogramName = pictureName.Substring(pictureName.LastIndexOf(@"\"));
            histogramName = histogramName.Substring(0, histogramName.LastIndexOf("."));
            img.Save($@"C:\Users\alek.hristov\Desktop\HistogramTask\Histograms\{histogramName.ToString()}.jpeg");

            listOfHistogramPaths.Add($@"C:\Users\alek.hristov\Desktop\HistogramTask\Histograms\{histogramName.ToString()}.jpeg");
        }

        public void ExportHistogramsToExcel(List<string> listOfHistogramPaths)
        {
            var xlApp = new Application();
            Workbook xlWorkBook = xlApp.Workbooks.Open(@"C:\Users\alek.hristov\Desktop\HistogramTask\ExcelFile.xlsx");
            Worksheet xlWorkSheet = xlWorkBook.Sheets[1];

            for (int i = 2; i <= listOfHistogramPaths.Count + 1; i++)
            {
                Range oRange = (Range)xlWorkSheet.Cells[i, 3];
                float Left = (float)((double)oRange.Left);
                float Top = (float)((double)oRange.Top);
                const float ImageSize = 38;
                xlWorkSheet.Shapes.AddPicture(listOfHistogramPaths[i - 2], Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoCTrue, Left, Top, ImageSize, ImageSize);
                oRange.RowHeight = ImageSize + 2;
            }

            xlWorkBook.SaveAs(@"C:\Users\alek.hristov\Desktop\HistogramTask\ExcelFile.xlsx");
            xlWorkBook.Close();
            xlApp.Quit();
        }

    }
}
