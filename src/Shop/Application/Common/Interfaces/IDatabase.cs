


namespace Application.Common.Interfaces
{
    public interface IDatabase
    {
        public DbSet<UserModel> Users { get; set; }
        public DbSet<ProductModel> Products { get; set; }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken);

    }
}
