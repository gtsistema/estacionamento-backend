using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gp.Domain.Auth
{
    public class BearerTokenSettings
    {
        public string Secret { get; set; }
        public int ExpirationInMinutes { get; set; }
        public string Issuer { get; set; }
        public string ValidOn { get; set; }
        public int RefreshExpirationInMinutes { get; set; }
    }
}
