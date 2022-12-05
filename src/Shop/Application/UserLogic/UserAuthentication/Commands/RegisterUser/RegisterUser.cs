using MediatR;
using Domain.Entities.User;
using Application.Common.Interfaces;

using System.Security.Cryptography;

namespace Application.UserLogic.UserAuthentication.Commands.RegisterUser;
public record class RegisterUserCommand(string Username,
                                        string Password,
                                        string Firstname,
                                        string Lastname) : IRequest<UserModel> { }

public class RegisterUserHandler:IRequestHandler<RegisterUserCommand,UserModel>
{
    private readonly IDatabase _database;
    private readonly IAuth     _auth;
    private readonly IConfiguration _config;
    public RegisterUserHandler(IDatabase database,IAuth auth,IConfiguration config)
    {
        _database = database;
        _auth = auth;
        _config = config;
    }

    public async Task<UserModel> Handle(RegisterUserCommand command,CancellationToken cancellationToken)
    {
        var salt = RandomNumberGenerator.GetBytes(256 / 8);

        var sec_key = Encoding.ASCII.GetBytes(_config.GetSection("SecurityHmacCode").Value);

        byte[] hash;

        using (var hmac = new HMACSHA256(sec_key))
        {
            hash = hmac.ComputeHash(Encoding.ASCII.GetBytes(String.Concat(command.Password, Convert.ToBase64String(salt))));
        }

        var User = new UserModel
        {
            Username        = command.Username,
            Password        = hash,
            SaltPassword    = salt,
            Firstname       = command.Firstname,
            Lastname        = command.Lastname,
            Cart            = new List<ProductModel>()
        };

        _database.Users.Add(User);
        await _database.SaveChangesAsync(cancellationToken);

        return User;
       
    }

}


