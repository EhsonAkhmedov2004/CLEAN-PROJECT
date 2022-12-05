using Application.Common.Models.MediatorModels;
namespace Application.ProductLogic.Commands.CreateProduct
{
    public record class CreateProductCommand(_ProductModel product) : IRequest<ProductModel> 
    { 
        
    }



    public class CreateProductHandler:IRequestHandler<CreateProductCommand,ProductModel>
    {
        private readonly IDatabase _database;
        public CreateProductHandler(IDatabase database)
        {
            _database = database;
        }

        public async Task<ProductModel> Handle(CreateProductCommand command,CancellationToken cancellationToken)
        {
            
            var product = command.product.Product;

            ProductModel model = new ProductModel
            {
                Id    = product.Id,
                Type  = product.Type,
                Title = product.Title,
                Color = product.Color,
                Cost  = product.Cost
            };

            _database.Products.Add(model);


            await _database.SaveChangesAsync(cancellationToken);


            return product;

        }
    }

}
