

namespace Application.UserLogic.Balance.Commands.BalanceUp;
public record class BalanceUpCommand(int money,string Token):IRequest<Response<string>>
{

}
public class BalanceUpHandler : IRequestHandler<BalanceUpCommand, Response<string>>
{
    private readonly IDatabase _database;
    public BalanceUpHandler(IDatabase database)
    {
        _database = database;
    }

    public async Task<Response<string>> Handle(BalanceUpCommand command,CancellationToken cancellationToken)
    {
        if(!isValid(command.Token)) return Respond("Token is not valid", 400);
        var username = ReadToken(command.Token).Claims.First(i=>i.Type == "Username").Value;

    
        var account = await _database.Users.FirstAsync(i => i.Username == username);

        if (command.money <= 0) 
        {
            return Respond($"Money should be more than 0 $", 400);
        }

        account.Balance += command.money;

        await _database.SaveChangesAsync(cancellationToken);

        return Respond($"your balance replenished with {command.money} ", 200);



    }
}
