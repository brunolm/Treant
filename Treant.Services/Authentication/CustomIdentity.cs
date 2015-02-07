using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Treant.Domain;

namespace Treant.Services.Authentication
{
    public class CustomIdentity : IIdentity
    {
        public User CurrentUser { get; private set; }

        public string AuthenticationType { get { return "Custom"; } }

        public string Name
        {
            get
            {
                return CurrentUser == null ? "Anonymous" : CurrentUser.Name;
            }
        }

        public bool IsAuthenticated
        {
            get { return CurrentUser != null; }
        }

        public CustomIdentity(User user)
        {
            CurrentUser = user;
        }
    }
}
