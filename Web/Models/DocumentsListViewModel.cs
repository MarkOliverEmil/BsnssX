using BsnssX.Core.Models;
using System.Collections.Generic;
using System.Linq;
using Web.Extensions;

namespace Web.Models
{
    public class DocumentsListViewModel
    {
        public List<Document> Documents { get; set; }
        public Mandant Mandant { get; set; }        

        public string SelectedYear { get; set; }
        public string SelectedDocType { get; set; }

        public DocumentsListViewModel FilterDocuments()
        {
            if (SelectedDocType.IsValidFilter())
                Documents = Documents.Where(x => x.DocumnentType == SelectedDocType).ToList();

            if (SelectedYear.IsValidFilter())
                Documents = Documents.Where(x => x.Year.ToString() == SelectedYear).ToList();

            Documents.ForEach(doc => doc.ShortComment = doc.Comment.MakeShort(20));
            return this;
        }
    }
}
