using BsnssX.Core.Models;
using System.Collections.Generic;
using System.Linq;

namespace BsnssX.Core.Services
{
    public class DocumentService : Service<Document>, IDocumentService
    {
        public DocumentService()
        {
            FileName = "Documents.json";
            Load();
        }
        public Document GetByOwnerId(string id)
        {
            return Get().FirstOrDefault(x => x.OwnerId == id);
        }
        public List<Document>  GetListByOwnerId(string id)
        {
            var res =  Get().Where(x => x.OwnerId == id)
            .OrderByDescending(x => x.Year)
            .ThenBy(x => x.DocumnentType)
            .ThenBy(x => x.FileName)
            .ToList();
            return res;
        }

        public List<Document> GetForMandant(string id)
            => Get().Where(x => x.MandantId == id)
            .OrderByDescending(x => x.Year)
            .ThenBy(x => x.DocumnentType)
            .ThenBy(x => x.FileName)
            .ToList();

    }    
}
