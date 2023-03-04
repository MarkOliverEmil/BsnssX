using System.IO;
using System;
using System.Linq;
using System.Text;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace Web.Helpers
{
    public class CreateLeistungsnachweis
    {
        static public void Create(string data, int month, int year, string fileName)
        {            
            StringBuilder sb = new StringBuilder();

            var ttk = "C# Software Development";
            var fields = data.Split("\t").ToList();
            DateTime x = new DateTime(year, month, 1);
            sb.AppendLine("Tom Sailor Ltd.");
            sb.AppendLine();
            sb.AppendLine($"Leistungsnachweis {x.ToString("MMMM")} {year}");
            sb.AppendLine("Project: C# Software Development");
            sb.AppendLine("Customer: ACME Company");
            sb.AppendLine();
            int no = 0;

            int sumHours = 0;
            int sumMins = 0;
            foreach (var f in fields)
            {
                if (no > 0 && no <= DateTime.DaysInMonth(year, month))
                {
                    DateTime dt = new DateTime(year, month, no);
                    var day = dt.ToString("ddd");
                    var val = "------\t";
                    if (dt.DayOfWeek == DayOfWeek.Saturday && string.IsNullOrWhiteSpace(f))
                    {
                        ;
                    }
                    else if (dt.DayOfWeek == DayOfWeek.Sunday)
                    {
                        sb.AppendLine();
                    }
                    else
                    {
                        if (!string.IsNullOrWhiteSpace(f))
                            val = $"{f.Trim()}h\t";
                        var f2 = f.Split(":");
                        if (f2.Length == 2)
                        {
                            sumHours += int.Parse(f2[0]);
                            sumMins += int.Parse(f2[1]);
                        }
                        sb.AppendLine($"{day} {no.ToString("D2")}.{month.ToString("D2")}.{year}      {val}      {ttk}");
                    }
                }
                no++;
            }

            var hours = sumMins / 60;
            var mins = sumMins % 60;
            sumHours += hours;

            sb.AppendLine();
            sb.AppendLine($"Sum : {sumHours}:{mins.ToString("D2")} h");
            sb.AppendLine();

            sb.AppendLine("Best regards");
            sb.AppendLine("Tom Sailor");
            var res = sb.ToString();
            Console.WriteLine(res);            
            Pdf(res, fileName);            
        }
        static void Pdf(string text, string fileName)
        {
            // create a new PDF document
            Document document = new Document();

            // create a new FileStream object to write the PDF file to disk
            FileStream fileStream = new FileStream(fileName, FileMode.Create);

            // create a new PdfWriter object to write the PDF file
            PdfWriter.GetInstance(document, fileStream);

            // open the document
            document.Open();

            // add a new paragraph to the document            

            BaseFont courier = BaseFont.CreateFont(BaseFont.COURIER, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);

            // create a new Font object for the Courier font
            Font fontCourier = new Font(courier, 9);

            // add a new paragraph to the document with the Courier font
            Paragraph paragraph = new Paragraph(text, fontCourier);
            document.Add(paragraph);

            // close the document
            document.Close();

            // close the FileStream object
            fileStream.Close();
        }
    }
}
