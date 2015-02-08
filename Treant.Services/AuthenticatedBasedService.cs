namespace Treant.Services
{
    using System.Threading;
    using Treant.Core.Extenders;
    using Treant.Services.Authentication;

    public abstract class AuthenticatedBasedService
    {
        public int CurrentUserID
        {
            get
            {
                return Thread.CurrentPrincipal.GetIdentity<CustomIdentity>().CurrentUser.ID;
            }
        }
    }
}
