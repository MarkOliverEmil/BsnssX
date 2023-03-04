using BsnssX.Core.Interfaces;
using BsnssX.Core.Models;
using BsnssX.Core.Utils;
using System.Collections.Generic;
using System.Linq;

namespace BsnssX.Core.Services
{
    public class BankDetailService : IBankDetailService
    {
        const string FileName = "Banks.json";
        static List<BankDetails> s_items;
        
        public List<BankDetails> Get()
        {
            if (s_items == null)
                s_items = DbUtils<BankDetails>.Load(FileName);
            return s_items;
        }


        public BankDetails Add(BankDetails adr)
        {
            return adr;
        }

        public BankDetails Get(string id) => Get().FirstOrDefault(x => x.Id == id);

        public void Delete(string id)
        {

        }

        public BankDetails Update(BankDetails adr)
        {
            return adr;
        }
    }
}
