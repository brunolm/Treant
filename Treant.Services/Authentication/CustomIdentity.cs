using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Treant.Services.Authentication
{
    public class CustomIdentity : IIdentity
    {
        public string Name { get; private set; }

        public string AuthenticationType { get { return "Custom"; } }

        public bool IsAuthenticated
        {
            get { return !String.IsNullOrEmpty(Name); }
        }

        public CustomIdentity(string name)
        {
            Name = name;
        }
    }
}
