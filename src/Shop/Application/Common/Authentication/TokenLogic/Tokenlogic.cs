
using System.IdentityModel.Tokens.Jwt;
using Application.Common.Authentication;

namespace Application.Common.Authentication.TokenLogic
{
    public class Tokenlogic
    {
        public static bool isValid(string token)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("{SecurityToken}___--999999}}==09sl;fhq3hfljahdljfhlqejfkmanflhq3hfigbqhfpij1[o gfwahbglnadsgfv]")); // CHANGE ![]
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);
            var handler = new JwtSecurityTokenHandler();
            var Token = handler.ReadJwtToken(token);
            
            


           // var tokenInfo = Token.Claims.First(o=>o.Type == "Username").Value;
            

            

            // CHECK TOKEN SIGNING CREDENTIALS </>...

            
            
             /// TODO HAVE TO !<> MUST !.....
            
         
           
            var time = DateTime.Compare(DateTime.Now,Token.ValidTo);

            
            if (time > 0) return false;

            return true;

        }
        
        public static JwtSecurityToken ReadToken(string jwt)
        {
            
            var handler   = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadJwtToken(jwt);

            
            return jsonToken;
        }
    }
}
