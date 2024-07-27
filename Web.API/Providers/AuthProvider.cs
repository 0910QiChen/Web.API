using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Web.API.Contexts;

namespace Web.API.Providers
{
    public class AuthProvider : OAuthAuthorizationServerProvider
    {
        private readonly ProductContext _context = new ProductContext();
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }
        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            var identity = new ClaimsIdentity(context.Options.AuthenticationType);
            var admin = _context.Users.FirstOrDefault(u => u.UserName == "Admin");
            var user = _context.Users.FirstOrDefault(u => u.UserName == context.UserName && u.UserPassword == context.Password);
            if (user == null)
            {
                context.SetError("invalid_grant", "Provided username and password is incorrect");
                return;
            }
            else if (context.UserName == admin.UserName && context.Password == admin.UserPassword)
            {
                identity.AddClaim(new Claim("username", context.UserName));
                identity.AddClaim(new Claim(ClaimTypes.Role, "Admin"));
                context.Validated(identity);
            }
            else
            {
                identity.AddClaim(new Claim("username", user.UserName));
                identity.AddClaim(new Claim(ClaimTypes.Role, "User"));
                context.Validated(identity);
            }
        }
    }
}