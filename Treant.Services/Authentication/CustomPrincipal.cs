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
        public IIdentity Identity { get; }

        public bool IsInRole(string role)
        {
            // TODO: roles
            return true;
        }
    }
}
