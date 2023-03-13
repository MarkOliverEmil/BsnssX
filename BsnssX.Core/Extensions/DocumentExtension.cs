using BsnssX.Core.Models;
using System.IO;

namespace BsnssX.Core.Extensions
{
    public static class DocumentExtension
    {
        public static string GetFilePath(this Document document)
        {            
            var filePath = Path.Combine(Config.RootDir, Config.BlobDir, document.MandantId, document.StorageFile);
            return filePath;
        }
        public static string GetUrlPath(this Document document)
        {
            if (document == null)
                return null;
            var filePath = "~/" + Path.Combine(Config.BlobDir, document.MandantId, document.StorageFile).Replace("\\", "/");                    
            return filePath;
        }
    }
}
