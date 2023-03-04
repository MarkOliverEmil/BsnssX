using System;

namespace BsnssX.Core.Utils
{
    public class IdGenerator
    {
        public static string CreateId()
        {
            var res = Guid.NewGuid().ToString().Replace("-", string.Empty).ToUpper();
            return res;
        }
        public static string NextId { get => CreateId(); }
    }
}