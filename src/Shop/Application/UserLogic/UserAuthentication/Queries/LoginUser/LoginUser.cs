using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore;



namespace Application.UserLogic.UserAuthentication.Queries.LoginUser;
public record class LoginUserQuery   (string Username,string Password) : IRequest<Response<string>>{ }

public record class LoginAdminQuery (string username, string password) : IRequest<Response<string>>{}
public class LoginUserHandler:IRequestHandler<LoginUserQuery,Response<string>>,IRequestHandler<LoginAdminQuery,Response<string>>
{
    private readonly IDatabase _database;
    private readonly IAuth _auth;
    private readonly IConfiguration _config;



    public LoginUserHandler(IDatabase database,IAuth auth, IConfiguration config)
    {
        _database = database;
        _auth = auth;
        _config = config;
    }

    public async Task<Response<string>> Handle(LoginUserQuery query,CancellationToken cancellationToken)
    {

        UserModel user = await _database.Users.FirstAsync(i => i.Username == query.Username);

        if (user == null) return Respond("Account not found",404);

        var passwordSalt = user.SaltPassword;

        var sec_key = Encoding.ASCII.GetBytes(_config.GetSection("SecurityHmacCode").Value);

        byte[] hashed_password;


        using (var encryption = new HMACSHA256(sec_key))
        { 
            hashed_password = encryption.ComputeHash (
                              Encoding.ASCII.GetBytes(
                              String.Concat(query.Password,Convert.ToBase64String(passwordSalt))));
        }
        

        if (!user.Password.SequenceEqual(hashed_password)) return Respond("Passoword is wrong",400);
        
        string jwt = _auth.CreateTokenForUser(user).ToString();

        if (jwt == string.Empty) 
        {
            return Respond(jwt,400);
        }

        return Respond(jwt,200);
        
    }

    // UPDATE ....
    public async Task<Response<string>> Handle(LoginAdminQuery query,CancellationToken cancellationToken) 
    {
        
        string jwt = _auth.CreateTokenForAdmin(query.username).ToString();
        return Respond(jwt,200);
    }
}

