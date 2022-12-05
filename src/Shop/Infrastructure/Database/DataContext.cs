using Microsoft.EntityFrameworkCore;
using Domain.Entities.User;
using Domain.Entities.Product;
using Application.Common.Interfaces;
namespace Infrastructure.Database
{
    public class DataContext : DbContext,IDatabase
    {
        public DataContext() { }
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }


        public DbSet<UserModel> Users {get;set;}
        public DbSet<ProductModel> Products { get; set; }




        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            if (!options.IsConfigured)
            {
                options.UseNpgsql("Host=localhost; Port=5432; Database=postgres; Username=postgres;Password=501106020");
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {

            builder.Entity<UserModel>()
                .Property(user => user.Balance).HasMaxLength(100000);

            
            
                
                

            base.OnModelCreating(builder);
            
                

               
        }


    }
}
