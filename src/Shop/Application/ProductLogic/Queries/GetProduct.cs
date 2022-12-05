


namespace Application.ProductLogic.Queries
{
    public record class GetProductQuery(string token):IRequest<List<ProductModel>>{}


    public class GetProductHandler:IRequestHandler<GetProductQuery,List<ProductModel>>
    {
        private readonly IDatabase _database;
        public GetProductHandler(IDatabase database)
        {
            _database = database;
        }

        public async Task<List<ProductModel>> Handle(GetProductQuery query,CancellationToken cancellationToken)
        {
            var admin = ReadToken(query.token).Claims.First(o=>o.Type == "Admin").Value;
         

            if (admin is null) return new List<ProductModel>();

            return await _database.Products.ToListAsync();
        }
    }
}
