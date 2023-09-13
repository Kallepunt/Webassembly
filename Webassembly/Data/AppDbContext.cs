using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Webassembly.Models;

namespace Webassembly.Data
{
	public class AppDbContext : DbContext
	{
        public AppDbContext(DbContextOptions options) : base(options) { }


        protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);
			builder.Entity<Part>().HasIndex(nameof(Part.Id));
		}

		// Inventory
		public DbSet<Part> Parts { get; set; } = default!;

	}
}
