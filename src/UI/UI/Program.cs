using UI.Data.ApiClient;
using UI.Data.Repositories.CategoryRepository;
using UI.Data.Repositories.ClientRepository;
using UI.Data.Repositories.PurchaseOrderRepository;
using UI.Data.Repositories.SparePartRepository;
using UI.Data.UnitOfWork;
using UI.Utilities;

// Add services to the container.
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpContextAccessor();
builder.Services.AddHttpClient();
builder.Services.AddSingleton<IApiClient, ApiClient>();
builder.Services.AddSession();

// Repositories for the app
builder.Services.AddScoped<ISparePartRepository, SparePartRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IPurchaseOrderRepository, PurchaseOrderRepository>();
builder.Services.AddScoped<IClientRepository, ClientRepository>();

// Unit of work
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Authentication
builder.Services.AddScoped<IAuthenticator, Authenticator>();

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.UseSession();

app.UseRequestLocalization("en-US", "es-EC");

app.Run();
