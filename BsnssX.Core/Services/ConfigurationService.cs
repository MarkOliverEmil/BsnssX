using BsnssX.Core.Interfaces;
using BsnssX.Core.Models;
using BsnssX.Core.Utils;
using System.Collections.Generic;
using System.Linq;

namespace BsnssX.Core.Services
{
    
    public class ConfigurationService  : IConfigurationService
    {
        const string FileName = "config.json";
        Configuration _item;
        public Configuration Get()
        {
            if (_item != null)
                return _item;
            List<Configuration> items = DbUtils<Configuration>.Load(FileName);            
                
            _item = items.FirstOrDefault();
            if (_item == null)
                _item = new Configuration();                
            return _item;
        }
        
        public void Update(Configuration item)
        {
            List<Configuration> items = new List<Configuration>();
            items.Add(_item);
            DbUtils<Configuration>.Save(FileName, items);
            _item = item;
        }        
    }
}
