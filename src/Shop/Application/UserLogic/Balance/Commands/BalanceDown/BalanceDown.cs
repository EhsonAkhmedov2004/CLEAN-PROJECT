
namespace Application.UserLogic.Balance.Commands.BalanceDown;
public record class BalanceDownCommand(int money, string Token) : IRequest<Response<string>>
{

}
public class BalanceDownHandler : IRequestHandler<BalanceDownCommand, Response<string>>
{
    private readonly IDatabase _database;
    public BalanceDownHandler(IDatabase database)
    {
        _database = database;
    }

    public async Task<Response<string>> Handle(BalanceDownCommand command, CancellationToken cancellationToken)
    {
        if (!isValid(command.Token)) return Respond("Token is not valid", 400);

        var username = ReadToken(command.Token)
                       .Claims
                       .First(i => i.Type == "Username").Value;



        var account      = _database.Users.First(i => i.Username == username);

        if (account.Balance < command.money) return Respond("Do not have enough money", 400);

        if (command.money <= 0) { return Respond("Money should be more than 0 $",400); }
        account.Balance -= command.money;

        await _database.SaveChangesAsync(cancellationToken);

        return Respond($"{command.money}$ has been withdrawn from your account.", 200);
            
            


    }
}