using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Estac.Domain.Shared
{
    public class Email
    {
        public string Provedor { get; private set; }
        public string UserName { get; private set; }
        public string Password { get; private set; }

        public Email(string provedor, string userName, string password )
        {
            Provedor = provedor;
            UserName = userName;
            Password = password;
        }
    }
}
