using BsnssX.Core.Models;
using System.Collections.Generic;

namespace BsnssX.Core.Services
{
    public interface IEmailConfigService
    {
        List<EmailConfiguration> Get();
    }
}
