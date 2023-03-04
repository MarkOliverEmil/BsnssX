using BsnssX.Core.Models;

namespace BsnssX.Core.Services
{
    public class EmailConfigService : Service<EmailConfiguration>, IEmailConfigService
    {
        public EmailConfigService()
        {
            FileName = "EmailConfig.json";
            Load();
        }        
    }
}
