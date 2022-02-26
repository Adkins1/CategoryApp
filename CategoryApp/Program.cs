using Microsoft.EntityFrameworkCore;
using TodoApp.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// dodany db context do aplikacji,
// nugetem dorzucamy obs³ugê sqlserver dla entity
// i przekazujemy propsy z appsetings.json
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("DefaultConnection")
    ));
// w nugecie doda³em razorruntimecoompilation, linijka jest aby hotreload dzia³a³ i ¿eby nie restartowaæ serwera co chwile jak coœ zmienie
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();
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
