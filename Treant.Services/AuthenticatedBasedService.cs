namespace Treant.Services
{
    using System.Threading;
    using Treant.Core.Extenders;
    using Treant.Domain;
    using Treant.Services.Authentication;

    public abstract class AuthenticatedBasedService : EntityService
    {
        public User CurrentUser
        {
            get
            {
                return Thread.CurrentPrincipal.GetIdentity<CustomIdentity>().CurrentUser;
            }
        }
        public int CurrentUserID
        {
            get
            {
                return CurrentUser.ID;
            }
        }
    }
}
