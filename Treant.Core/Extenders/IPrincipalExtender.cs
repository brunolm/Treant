using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Treant.Core.Extenders
{
    public static class IPrincipalExtender
    {
        public static T GetIdentity<T>(this IPrincipal principal) where T : class, IIdentity
        {
            return principal.Identity as T;
        }
    }
}
