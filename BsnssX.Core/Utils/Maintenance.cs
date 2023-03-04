using System.IO;
using System;

namespace BsnssX.Core.Utils
{
    public class Maintenance
    {
        public static void CleanTmpDir()
        {            
            // delete files in tmp dir which are older than 3 minutes
            var files = Directory.GetFiles(Config.TmpDir);

            var now = DateTime.Now;
            foreach (var file in files)
            {
                try
                {
                    var creationTime = File.GetCreationTime(file);
                    var age = now - creationTime;
                    if (age.TotalMinutes >= 3)
                        File.Delete(file);
                }
                catch (Exception ex)
                {
                    string s = ex.ToString();
                }                
            }         
        }
    }
}