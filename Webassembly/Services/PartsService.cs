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
	}
}
