using BsnssX.Core.Interfaces;
using BsnssX.Core.Models;
using BsnssX.Core.Utils;
using System.Collections.Generic;
using System.Linq;

namespace BsnssX.Core.Services
{
    public class Service<T> where T : IWithId
    {
        public List<T> Items { get; set; }
        public string FileName { get; set; }
        public void Save()
        {
            DbUtils<Note>.Save(FileName, Items);
        }
        public void Load()
        {
            Items = DbUtils<T>.Load(FileName);
        }
        public T Add(T obj)
        {
            Items.Add(obj);
            Save();
            return obj;
        }
        public void Delete(string id)
        {
            Items = Items.Where(x => x.Id != id).ToList();
            Save();
        }
        public List<T> Get()
        {            
            return Items;
        }

        public T Get(string id)
        {
            var res = Items.FirstOrDefault(x => x.Id == id);
            return res;
        }
        public T Update(T item)
        {
            Items = Items.Where(x => x.Id != item.Id).ToList();
            Items.Add(item);
            Save();
            return item;
        }
    }
}
