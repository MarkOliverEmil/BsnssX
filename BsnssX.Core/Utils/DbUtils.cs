using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace BsnssX.Core.Utils
{
    public class DbUtils<T>
    {
        public static List<T> Load(string fileName)
        {
            var res = new List<T>();
            var filePath = Path.Combine(Config.DbDir, fileName);
            if (File.Exists(filePath))
            {
                var jsonString = File.ReadAllText(filePath);
                res.AddRange(JsonSerializer.Deserialize<List<T>>(jsonString));
            }
            return res;
        }         
        public static void Save<U>(string fileName, List<U> items)
        {
            var filePath = Path.Combine(Config.DbDir, fileName);
            var json = JsonSerializer.Serialize(items);
            File.WriteAllText(filePath, json);
        }
    }
}