using BsnssX.Core.Models;
using System.Collections.Generic;

namespace BsnssX.Core.Services
{
    public interface IDocumentService
    {
        List<Document> Get();
        List<Document> GetForMandant(string id);
        Document Get(string id);
        Document GetByOwnerId(string id);
        List<Document> GetListByOwnerId(string id);

        Document Add(Document adr);

        void Delete(string id);

        Document Update(Document adr);

    }
}
