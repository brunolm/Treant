using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Treant.Core;
using Treant.DataProvider;

namespace Treant.Services.Authentication
{
    [Export]
    public class AuthenticationService
    {
        public void Authenticate(string name, string password)
        {
            Domain.User user = null;
            var hashPassword = Hash(password, name);

            using (var db = MefBootstrap.Resolve<ApplicationDbContext>())
            {
                #region Temp Create User

                if (db.Users.Count() == 0)
                {
                    db.Users.Add(new Domain.User
                    {
                        Name = "BrunoLM",
                        Email = "brunolm7@mailinator.com",
                        Password = Hash("password", "BrunoLM")
                    });
                    db.SaveChanges();
                }

                #endregion

                user = db.Users
                    .FirstOrDefault(o => o.Name.Equals(name, StringComparison.OrdinalIgnoreCase)
                        && o.Password.Equals(hashPassword, StringComparison.OrdinalIgnoreCase));
            }

            if (user == null)
            {
                throw new UnauthorizedAccessException("Invalid username/password");
            }

            AppDomain.CurrentDomain.SetThreadPrincipal(new CustomPrincipal { Identity = new CustomIdentity(user) });
        }

        private string Hash(string password, string salt)
        {
            return Convert.ToBase64String(new SHA256Managed().ComputeHash(Encoding.UTF8.GetBytes(salt.ToLower() + password + salt.ToUpper())));
        }
    }
}
