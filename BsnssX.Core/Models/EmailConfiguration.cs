using BsnssX.Core.Interfaces;

namespace BsnssX.Core.Models
{
    public class EmailConfiguration :  IWithId
    {                 
        public string server { get; set; }
        public string login { get; set; }
        public string pwd { get; set; }
        public string Id { get ; set; }
    }
}
