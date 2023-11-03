using MarketApi.Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Domain
{
	public class AppDbContext : IdentityDbContext<AppUser>
	{
		public AppDbContext(DbContextOptions<AppDbContext> dbContextOptions) : base(dbContextOptions)
		{

		}

		public DbSet<Product> Products { get; set; }
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{

			base.OnModelCreating(modelBuilder);
		}


	}
}
