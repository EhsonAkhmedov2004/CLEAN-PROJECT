
namespace Application.UserLogic.Balance.Queries;
public record class BalanceQuery(string Token) : IRequest<Response<string>> { }
public class BalanceQueryHandler : IRequestHandler<BalanceQuery, Response<string>>
{
    private readonly IDatabase _database;
    public BalanceQueryHandler(IDatabase database)
    {
        _database = database;
    }

    public async Task<Response<string>> Handle(BalanceQuery query, CancellationToken cancellationToken)
    {

        if (!isValid(query.Token)) return Respond("Token is not valid", 400);
        var username = ReadToken(query.Token).Claims.First(i => i.Type == "Username").Value;
    

        var account = await _database.Users.FirstAsync(i => i.Username == username);


        return Respond($"In your balance {account.Balance} $", 200);



    }
}
