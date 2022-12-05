
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

namespace Application.Common.Authentication
{
    public class Auth : IAuth
    {
        private readonly IConfiguration _config;
        public Auth(IConfiguration config)
        {
            _config = config;
        }

        //>-/// TOUCH AND FIX !
        public bool CheckTokenValidation(UserModel user,string jwt)
        {
            var newToken = ReadToken(CreateTokenForUser(user));
            var gotToken = ReadToken(jwt);
            if (newToken.SigningCredentials.Key.ToString() != gotToken.SigningCredentials.Key.ToString()) 
            {
                return false;
            }

            return true;
        }
        //>-/// TOUCH AND FIX !
        public string CreateTokenForUser(UserModel user)
        {

            List<Claim> claims = new List<Claim>
            {
                new Claim("UserId",user.Id.ToString()),
                new Claim("Username",user.Username),
                new Claim(ClaimTypes.Role,"User")

            };
            

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("SecurityToken").Value)); // CHANGE ![]
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            var token = new JwtSecurityToken(
                claims:claims,
                expires:DateTime.Now.AddDays(1),
                signingCredentials:cred);

            string jwt = new JwtSecurityTokenHandler().WriteToken(token);




            
            
            
        
            return jwt;

        }


        public string CreateTokenForAdmin(string Name)
        {

            List<Claim> claims = new List<Claim>
            {
                new Claim("Admin",Name),
                new Claim(ClaimTypes.Role,"Admin")

            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("SecurityToken").Value)); 
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: cred);

            string jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;

        }
    }
}
