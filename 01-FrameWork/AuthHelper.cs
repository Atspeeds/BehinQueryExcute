using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
namespace _0_FrameWork.FW.Application
{
	public class AuthHelper : IAuthHelper
    {
        private readonly IHttpContextAccessor _contextAccessor;


        public AuthHelper(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        public void SingIn(AuthViewModel account)
        {

            var claims = new List<Claim>
            {
                new Claim("AccountId", account.Id.ToString()),
                new Claim(ClaimTypes.Name, account.FullName),
                new Claim(ClaimTypes.MobilePhone,account.Mobile),

            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
                ExpiresUtc = DateTimeOffset.UtcNow.AddDays(30),
                IsPersistent = true,
            };

            _contextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);
        }

        public void SignOut()
        {
            _contextAccessor.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }

        public bool IsAuthenticated()
        {
            return _contextAccessor.HttpContext.User.Identity.IsAuthenticated;

        }

        public AuthViewModel GetCurrentUserInfo()
        {
            var claims = _contextAccessor.HttpContext.User.Claims.ToList();

            var AuthUser = IsAuthenticated()
             ?
             new AuthViewModel
             {
                 Id = long.Parse(claims.FirstOrDefault(x => x.Type == "AccountId")?.Value),
                 FullName = claims.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value,
                 Mobile = claims.FirstOrDefault(x => x.Type == ClaimTypes.MobilePhone)?.Value,
             }
             : new AuthViewModel();


            return AuthUser;

        }


        public long GetUserId()
        {
            if (!IsAuthenticated()) return 0;

            return long.Parse(_contextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == "AccountId")?.Value);
        }


    }
}
