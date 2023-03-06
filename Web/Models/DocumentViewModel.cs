using BsnssX.Core;
using BsnssX.Core.Extensions;
using BsnssX.Core.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;

namespace Web.Models
{

    public class DocumentViewModel
    {
        public DocumentViewModel(){}
        public DocumentViewModel(Mandant mandant)
        {            
            Modus = Modus.Create;
            Year = DateTime.Now.Year;
            MandantId = mandant.Id;
            MandantName = mandant.Address.Name;
            OwnerId = mandant.Id;
            DocumentType = Consts.General;
            Info1 = DateTime.Now.ToShortDateString();
        }

        // for edit, display
        public DocumentViewModel(Document doc, Mandant mandant)
        {
            Id = doc.Id;
            MandantId = doc.MandantId;
            MandantName = mandant.Address.Name;
            OwnerId = doc.OwnerId;
            Year = doc.Year;
            DocumentType = doc.DocumnentType;
            Comment = doc.Comment;
            Info1 = doc.Timestamp.ToLongDateString() + " " + doc.Timestamp.ToShortTimeString();
            FileName = doc.FileName;
            Info2 = doc.ContentType;
            Modus = Modus.Edit;
            TheDocument = doc;
        }

        [Key]
        public string Id { get; set; }
        
        public Modus Modus { get; set; }

        public Document TheDocument { get; set; }

        public string MandantId { get; set; }
        public string MandantName { get; set; }
        public int Year { get; set; }
                    
        public string OwnerId { get; set; }
        public string DocumentType { get; set; }
        public string Comment { get; set; }
        public string FileName { get; set; }

        public string PathName { get => TheDocument.GetUrlPath();  }
        
        public bool HasImage { get => TheDocument != null && TheDocument.ContentType.StartsWith("image"); }
        public bool IsText { get => TheDocument != null && TheDocument.ContentType.StartsWith("text"); }

        public string GetText
        {
            get
            {
                if (TheDocument == null || IsText == false)
                    return string.Empty;
                string pn = TheDocument.GetFilePath();
                var res = File.ReadAllText(pn);
                return res;
            }
        }

        [NotMapped]
        public IFormFile Document { get; set; }

        public string Info1 { get; set; }
        public string Info2 { get; set; }        

        public string AspAction { get; set; }
        public string AspActionDelete { get; set; } = "DeleteDocument";
        public string AspController { get; set; }
        public string Tooltip { get; set; }
    }
}
