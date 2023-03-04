using BsnssX.Core.Models;

namespace BsnssX.Core.Interfaces
{
    public interface IConfigurationService
    {
        public Configuration Get();
        public void Update(Configuration item);
    }
}
