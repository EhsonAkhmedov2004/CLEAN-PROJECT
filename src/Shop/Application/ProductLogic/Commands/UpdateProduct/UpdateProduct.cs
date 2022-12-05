using Application.Common.Models.MediatorModels;
namespace Application.ProductLogic.Commands.UpdateProduct
{



    public record class UpdateProductCommand(_ProductModel product) : IRequest<ProductModel> { }
    
    public class UpdateProductHandler : IRequestHandler<UpdateProductCommand,ProductModel>
    {
        public readonly IDatabase _database;
        public UpdateProductHandler(IDatabase database)
        {
            _database = database;
        }

        public async Task<ProductModel> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
        {
            var _product = command.product.Product;
            var username = ReadToken(command.product.Token).Claims.First(i => i.Type == "Username").Value;

            var user = _database.Users.First(u=>u.Username == username);

            

            var product = await _database.Products.FindAsync(new object[] { _product.Id }, cancellationToken);
            product.Type   = _product.Type;
            product!.Title = _product.Title;
            product.Color  = _product.Color;
            product.Cost   = _product.Cost;

            await _database.SaveChangesAsync(cancellationToken);

            return product;
        }
    }
}
