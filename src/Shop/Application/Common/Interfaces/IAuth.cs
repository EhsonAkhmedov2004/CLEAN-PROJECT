
namespace Application.Common.Interfaces
{
    public interface IAuth
    {
        public string CreateTokenForUser(UserModel user);
        public string CreateTokenForAdmin(string Name);
        public bool CheckTokenValidation(UserModel user,string jwt);
    }
}
