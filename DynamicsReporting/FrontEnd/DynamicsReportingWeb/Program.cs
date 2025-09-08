 
using DynamicsReportingWeb.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllersWithViews();

// Register HttpClient + ApiService (mock service)
builder.Services.AddHttpClient<IApiService, ApiService>();

// Add Session
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSession();
app.UseAuthorization();

 
//// Default route รองรับทุก Controller/Action
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Authen}/{action=Index}/{id?}");

 
app.Run();
