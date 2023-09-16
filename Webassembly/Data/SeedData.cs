using System.Formats.Asn1;
using System.Globalization;
using System.IO;
using CsvHelper;
using Microsoft.EntityFrameworkCore;
using SqliteWasmHelper;
using Webassembly.Models;
using static System.Net.WebRequestMethods;

namespace Webassembly.Data
{
	public static class SeedData
	{

		public static async Task EnsureThatDataIsSeeded(IServiceProvider services)
		{
			var scopeFactory = services.GetRequiredService<IServiceScopeFactory>();
			using var scope = scopeFactory.CreateScope();
			var ctx = await scope.ServiceProvider.GetRequiredService<ISqliteWasmDbContextFactory<AppDbContext>>().CreateDbContextAsync();
			Console.WriteLine(ctx.Parts.Count());
			if (!ctx.Parts.Any())
			{
					await SeedTheData(ctx,services);

			}
		}


		public static async Task SeedTheData(AppDbContext db, IServiceProvider services)
		{
			Console.WriteLine("Seeding Data");
			string Optionssource = "parts.csv";

			var client = services.GetRequiredService<HttpClient>();

			var st = await client.GetStreamAsync(Optionssource);

			using var reader = new StreamReader(st);

			using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
			var count = 0;
			while (csv.Read())
			{
				count++;

				db.Parts.Add(new Part
				{
					Id = count,
					Category = csv.GetField<string>(0),
					Subcategory = csv.GetField<string>(1),
					Name = csv.GetField<string>(2),
					Location = csv.GetField<string>(3),
					PriceCents = (long)(csv.GetField<double>(4) * 100),
					Stock = (int)Math.Round(50000 * Random.Shared.NextDouble() * Random.Shared.NextDouble() * Random.Shared.NextDouble()),
				});

				if (count % 1000 == 0)
				{
					Console.WriteLine($"Seeded item {count}...");
				}

				if (count == 1000)
				{
					break;
				}
			}

			await db.SaveChangesAsync();
		}

	}

}

