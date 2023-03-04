using SelectPdf;

namespace Web.Helpers
{
    public class HtmlToPdfHelper
    {
        public static byte[] GeneratePdf(string url)
        {
            HtmlToPdf converter = new HtmlToPdf();
            PdfDocument pdf = converter.ConvertUrl(url);
            var data = pdf.Save();
            return data;
        }        

    }
}
