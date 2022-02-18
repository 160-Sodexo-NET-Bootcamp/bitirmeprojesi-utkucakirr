using Entities.DataModel;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Context
{
    public interface IProjectDbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<Mail> Mails { get; set; }
        public DbSet<Offer> Offers { get; set; }
        public DbSet<Status> Status { get; set; }
        int SaveChanges();
    }
}
