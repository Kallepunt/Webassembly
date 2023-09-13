using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.EntityFrameworkCore;
using MudBlazor.Services;
using SqliteWasmHelper;
using Webassembly;
using Webassembly.Data;
using Webassembly.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddSqliteWasmDbContextFactory<AppDbContext>(opts =>
	opts.UseSqlite("Data Source=partsDb.sqlite3"));

builder.Services.AddScoped<PartsService>();

builder.Services.AddMudServices();
var host = builder.Build();

await SeedData.EnsureThatDataIsSeeded(host.Services);


await host.RunAsync();
