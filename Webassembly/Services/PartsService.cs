using SqliteWasmHelper;
using Webassembly.Data;
using Webassembly.Models;

namespace Webassembly.Services
{
	public class PartsService
	{
		private readonly ISqliteWasmDbContextFactory<AppDbContext> _dbFactory;
		public PartsService(ISqliteWasmDbContextFactory<AppDbContext> dbFactory)
		{
			_dbFactory = dbFactory;
		}

		public async Task<List<Part>> GetAllPartsAsync()
		{
			using var ctx = await _dbFactory.CreateDbContextAsync();
			return ctx.Parts.ToList();

		}

		public async Task DeletePart(Part part)
		{
			try
			{
				using var ctx = await _dbFactory.CreateDbContextAsync();

				var partInDb = ctx.Parts.Where(x => x.Id == part.Id).FirstOrDefault();

				if (partInDb != null)
				{
					ctx.Parts.Remove(partInDb);
					await ctx.SaveChangesAsync();
				}
			}
			catch (Exception)
			{

				throw;
			}
			

		}

		public async Task EditPart(Part part)
		{
			try
			{
				using var ctx = await _dbFactory.CreateDbContextAsync();

				var partInDb = ctx.Parts.Where(x => x.Id == part.Id).FirstOrDefault();

				if (partInDb != null)
				{
					partInDb.Category = part.Category;
					partInDb.Subcategory = part.Subcategory;
					partInDb.Name = part.Name;
					partInDb.Location = part.Location;
					partInDb.Stock = part.Stock;
					partInDb.PriceCents = part.PriceCents;

					await ctx.SaveChangesAsync();
				}
			}
			catch (Exception)
			{

				throw;
			}
			
		}

		public async Task CreatePart(Part part)
		{
			try
			{
				using var ctx = await _dbFactory.CreateDbContextAsync();
				ctx.Parts.Add(part);
				await ctx.SaveChangesAsync();


			}
			catch (Exception)
			{

				throw;
			}
		}

	}
}
