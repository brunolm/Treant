using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Treant.Services.Authentication
{
    public class AuthenticationService
    {
        public CustomIdentity Authenticate(string name, string password)
        {
            var hashPassword = Hash(password, name);

            // TODO: user
            var user = new CustomIdentity("BrunoLM");//ctx.Users.FirstOrDefault(o => o.Name == name && o.Password == hashPassword);

            if (user == null)
            {
                throw new UnauthorizedAccessException();
            }

            return user;
        }

        private string Hash(string password, string salt)
        {
            return Convert.ToBase64String(new SHA256Managed().ComputeHash(Encoding.UTF8.GetBytes(salt + password + salt)));
        }
    }
}
