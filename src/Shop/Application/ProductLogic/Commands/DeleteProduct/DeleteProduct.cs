

namespace Application.ProductLogic.Commands.DeleteProduct
{
    public record class DeleteProductCommand(int Id,string token):IRequest<ProductModel>;
    public class DeleteProductHandler : IRequestHandler<DeleteProductCommand,ProductModel>
    {
        private readonly IDatabase _database;


        public DeleteProductHandler(IDatabase database)
        {
            _database = database;

        }


        public async Task<ProductModel> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
        {
            var product = await _database.Products.FindAsync(new object[] { command.Id }, cancellationToken);

            _database.Products.Remove(product!);

            await _database.SaveChangesAsync(cancellationToken);

            return product;

        }

    }

}
