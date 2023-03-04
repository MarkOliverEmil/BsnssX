using BsnssX.Core;
using BsnssX.Core.Models;
using BsnssX.Core.Services;
using BsnssX.Core.Utils;
using Microsoft.AspNetCore.Http;
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

        public static Document CreateDocument(this IDocumentService documentService,
            DocumentViewModel model, string webRootPath)
        {
            return CreateDocument(documentService, model.MandantId, model.OwnerId, model.Year, model.Document,
                webRootPath, model.DocumentType, model.Comment);
        }


        public static Document CreateDocument(
            this IDocumentService documentService, 
            string mandantId,
            string ownerId,
            int year,
            IFormFile file,
            string webRootPath,
            string docType,
            string comment = null)
        {            
            var dir = Config.BlobDir + "\\" + mandantId;

            Document doc = new Document()
            {
                Id = IdGenerator.CreateId(),
                MandantId = mandantId,
                Directory = dir,
                Year = year,
                Timestamp = DateTime.Now,
                ContentType = file.ContentType,
                FileName = file.FileName,
                DocumnentType = docType,
                OwnerId = ownerId,
                Comment = comment,

            };
            doc.StorageFile = doc.Id;
            var extension = Path.GetExtension(doc.FileName);
            if (!string.IsNullOrEmpty(extension))
                doc.StorageFile += extension;


            dir = Path.Combine(webRootPath, dir);
            Directory.CreateDirectory(dir);
            var filePath = Path.Combine(dir, doc.StorageFile);
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(fileStream);                
            }
            documentService.Add(doc);
            return doc;
        }
    }
}