
using BsnssX.Core;
using BsnssX.Core.Models;
using BsnssX.Core.Services;
using BsnssX.Core.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;

namespace ConsoleApp
{
    class Program
    {       
        public static string DbDir { get; set; } = @"M:\BsnssX\Code\Web\wwwroot\db";
        //static void WriteMandants()
        //{
        //    var items = MandantFactory.Create2();

        //    var filePath = Path.Combine(DbDir, "Mandants.json");
        //    var json = JsonSerializer.Serialize(items);
        //    File.WriteAllText(filePath, json);
        //}
        //static void WriteBanks()
        //{
        //    var items = BankFactory.Get() ;
        //    var filePath = Path.Combine(DbDir, "Banks.json");
        //    var json = JsonSerializer.Serialize(items);
        //    File.WriteAllText(filePath, json);
        //}
        //static void Tax()
        //{
        //    var items = TaxFactory.Get();
        //    var filePath = Path.Combine(DbDir, "Tax.json");
        //    var json = JsonSerializer.Serialize(items);
        //    File.WriteAllText(filePath, json);
        //}
        //static void Adr()
        //{
        //    var items = AddressFactory.Get();
        //    var filePath = Path.Combine(DbDir, "Addresses.json");
        //    var json = JsonSerializer.Serialize(items);
        //    File.WriteAllText(filePath, json);
        //}

        //static void Invoices()
        //{
        //    var items = InvoiceFactory.CreateInvoices();
        //    var filePath = Path.Combine(DbDir, "Invoices.json");
        //    var json = JsonSerializer.Serialize(items);
        //    File.WriteAllText(filePath, json);
        //}

        static void CheckDocuments()
        {
            var docService = new DocumentService();
            var docs = docService.Get();

            var blobDir = @"M:\BsnssX\Code\Web\wwwroot\Blobs";

            var nulls = docs.Where(x => x.MandantId == null).ToList();
            nulls.ForEach(x => docService.Delete(x.Id));
            foreach (var doc in docs)
            {
                
                var filePath = Path.Combine(blobDir, doc.MandantId == null ? string.Empty : doc.MandantId, doc.StorageFile);
                if (File.Exists(filePath))
                {
                    Console.WriteLine("Exists: ");
                }
                else {
                    Console.WriteLine("DOSE NOT Exists: ");
                    Console.WriteLine(doc.MandantId);

                    Console.WriteLine(doc.StorageFile);
                }
            }            
        }

        static void Main(string[] args)
        {
            Config.DbDir = @"M:\BsnssX\Code\Web\wwwroot\db";
            Console.WriteLine("hi ...");
            CheckDocuments();
            Console.WriteLine(".... bye");            
        }
    }
}
