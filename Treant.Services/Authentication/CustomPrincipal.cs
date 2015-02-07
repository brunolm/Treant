using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Treant.Services.Authentication
{
    public class CustomPrincipal : IPrincipal
    {
        private IIdentity identity;
        public IIdentity Identity
        {
            get
            {
                return identity;
            }
            set
            {
                identity = value;
            }
        }

        public bool IsInRole(string role)
        {
            // TODO: roles
            return true;
        }
    }
}
