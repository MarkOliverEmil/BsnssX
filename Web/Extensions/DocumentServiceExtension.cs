using BsnssX.Core;
using BsnssX.Core.Extensions;
using BsnssX.Core.Models;
using BsnssX.Core.Services;
using BsnssX.Core.Utils;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Web.Helpers;
using Web.Models;

namespace Web.Extensions
{
    public static class DocumentServiceExtension
    {        
        public static List<SelectListItem> GetTypes(this IDocumentService documentService, string mandantId, string selectedExpense)
        {
            var items = documentService.GetForMandant(mandantId)
                .Select(x => x.DocumnentType)
                .Distinct()
                .OrderBy(x => x)                
                .ToList();      
            items.Insert(0, Consts.All);
            if (!string.IsNullOrEmpty(selectedExpense))
            {
                items = items.Where(x => x != selectedExpense).ToList();
                items.Insert(0, selectedExpense);
            }

            return items.Select(x => ControllerUtils.CreateSelectListItem(x)).ToList();
        }
        public static List<SelectListItem> GetYears(this IDocumentService documentService, string mandantId, string selectedYear)
        {
            var items = documentService.GetForMandant(mandantId)
                .Select(x => x.Year.ToString())
                .Distinct()
                .OrderBy(x => x)
                .ToList();
            items.Insert(0, Consts.All);
            if (!string.IsNullOrEmpty(selectedYear))
            {
                items = items.Where(x => x != selectedYear).ToList();
                items.Insert(0, selectedYear);
            }

            return items.Select(x => ControllerUtils.CreateSelectListItem(x)).ToList();
        }

        public static Document CreateDocument( this IDocumentService documentService, DocumentViewModel model)
        {            
            Document doc = new Document()
            {
                Id = IdGenerator.CreateId(),
                MandantId = model.MandantId,
                Year = model.Year,
                Timestamp = DateTime.Now,
                ContentType = model.Document.ContentType,
                FileName = model.Document.FileName,
                DocumnentType = model.DocumentType,
                OwnerId = model.OwnerId,
                Comment = model.Comment
            };
            doc.StorageFile = doc.Id;
            var extension = Path.GetExtension(doc.FileName);
            if (!string.IsNullOrEmpty(extension))
                doc.StorageFile += extension;
            
            var filePath = doc.GetFilePath();
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                model.Document.CopyTo(fileStream);                
            }
            documentService.Add(doc);
            return doc;
        }
    }
}