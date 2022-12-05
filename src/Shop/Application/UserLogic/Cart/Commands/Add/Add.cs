
namespace Application.UserLogic.Cart.Commands.Add;
public record class AddCommand(string Token, int id) : IRequest<Response<string>> { }


public class AddHandler : IRequestHandler<AddCommand,Response<string>>
{
    private readonly IDatabase _database;

    public AddHandler(IDatabase database)
    {
        _database = database;
       
    }

    public async Task<Response<string>> Handle(AddCommand command,CancellationToken cancellationToken)
    {
        if (!isValid(command.Token)) return Respond("Token is not valid", 400);

        var username = ReadToken(command.Token).Claims.First(u => u.Type == "Username").Value;

        var user    = await _database
                            .Users                            
                            .FirstAsync(i => i.Username == username);

        var product = await _database.Products
                               .FirstOrDefaultAsync(i => i.Id == command.id);


        if (product == null) return Respond("Product not found", 404);

        user.Cart.Add(product);

        await _database.SaveChangesAsync(cancellationToken);



        return Respond("Product has been added to User's cart.", 200);
    }
}