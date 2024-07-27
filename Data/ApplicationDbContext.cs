using Microsoft.EntityFrameworkCore;
using Dashboard.Models;

namespace Dashboard.Data
{
	public class ApplicationDbContext:DbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
		{


		}

		public DbSet<Products> products { get; set; }
		public DbSet<ProductsDetails> productDetails { get; set; }
        public DbSet<DamagedProducts> damagedProducts { get; set; }

    }
}
