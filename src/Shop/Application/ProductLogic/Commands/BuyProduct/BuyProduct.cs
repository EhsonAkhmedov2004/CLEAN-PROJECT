

namespace Application.ProductLogic.Commands.BuyProduct;
public record class BuyProductCommand(int Id,string Token) : IRequest<Response<string>> { }


public class BuyProductHandler : IRequestHandler<BuyProductCommand,Response<string>>
{
    private readonly IDatabase _database;
    private readonly IAuth _auth;

    public BuyProductHandler(IDatabase database,IAuth auth)
    {
        _database = database;
        _auth = auth;
    }


    public async Task<Response<string>> Handle(BuyProductCommand command,CancellationToken cancellationToken)
    {
        if (!isValid(command.Token)) return Respond("token is not valid",400);


        var username = ReadToken(command.Token).Claims.First(user => user.Type == "Username").Value;

        var account = await _database
                            .Users
                            .FirstAsync(user => user.Username == username);

        // if (!_auth.CheckTokenValidation(account,command.Token)) 
        // {
        //     return Respond("Token signature is not valid!",400);
        // }

        var product = await _database
                            .Products
                            .FirstOrDefaultAsync(i=>i.Id == command.Id);
        
        if (product == null) return Respond("Product could not be bought,it is not exist!....",401);

        if (account.Balance < product.Cost) return Respond("do not have enough money",400);

        account.Balance -= product.Cost;
        
        var ProductFromCart = account.Cart.FirstOrDefault(i=>i.Id == product.Id);
        
        if (ProductFromCart != null) 
        {
            account.Cart.Remove(ProductFromCart);
        }

        await _database.SaveChangesAsync(cancellationToken);
        
        return Respond("Successfully bought product",200);
    }
}

