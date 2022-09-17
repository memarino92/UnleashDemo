using Unleash;
using DotNetEnv;

Env.Load();

var settings = new UnleashSettings()
{
    AppName = "dot-net-client",
    Environment = Env.GetString("ENVIRONMENT"),
    FetchTogglesInterval = TimeSpan.FromSeconds(1),
    UnleashApi = new Uri(Env.GetString("API_URL")),
    CustomHttpHeaders = new Dictionary<string, string>()
            {
            {"Authorization", Env.GetString("API_KEY") }
            }
};

IUnleash unleash = new DefaultUnleash(settings);

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSingleton<IUnleash>(unleash);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
