using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;
namespace Application.Common.Authentication.Cookie
{
    public class Cookies
    {

        public void ReadCookie()
        {

        }
        public CookieOptions Cookie(DateTime expired )
        {
            var cookie     = new CookieOptions();
            cookie.Expires = expired;

            return cookie;


            
            
        }
        public void RemoveCookie()
        {

        }

    }
}
