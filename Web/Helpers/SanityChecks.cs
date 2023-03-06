using BsnssX.Core;
using BsnssX.Core.Extensions;
using BsnssX.Core.Services;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;

namespace Web.Helpers
{
    public class SanityChecks
    {
        public static string CheckForMissingDocFiles(IDocumentService service)
        {

            StringBuilder sb = new StringBuilder();
            var docs = service.Get();
            foreach (var doc in docs)
            {
                if (doc.MandantId == null)
                {
                    sb.AppendLine("MandantId is null");
                    var json = JsonSerializer.Serialize(doc);
                    sb.AppendLine(json);
                    sb.AppendLine();
                    continue;
                }
                var fn = doc.GetFilePath();
                if (!System.IO.File.Exists(fn))
                {
                    sb.AppendLine($"{fn} does not exist");
                    var json = JsonSerializer.Serialize(doc);
                    sb.AppendLine(json);
                    sb.AppendLine();

                }
            }

            var dirs = Directory.GetDirectories(Path.Combine(Config.RootDir, Config.BlobDir));
            foreach (var dir in dirs) 
            {                
                var files = Directory.GetFiles(dir);
                foreach (var file in files)
                {
                    var fileName = Path.GetFileName(file);
                    if (!docs.Any(x => x.StorageFile == fileName))
                    {
                        sb.AppendLine($"{file} is not used");
                    }
                }
            }
            var res = sb.ToString();
            return res;
        }       
    }
}
