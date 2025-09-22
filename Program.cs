using TodoCheckerApp.Contexts;
using Microsoft.EntityFrameworkCore;
using TodoCheckerApp.Contexts.WLK;

var builder = WebApplication.CreateBuilder(args);
// メモ：MVC を使えるようにする
builder.Services.AddControllersWithViews();
// DbContext を登録（appsettings.json の ConnectionStrings:DefaultConnection を使用）
builder.Services.AddDbContext<WalkmanContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


// --- 認証の設定 ---
builder.Services.AddAuthentication("CookieAuth")  // デフォルトスキーム
    .AddCookie("CookieAuth", options =>
    {
        options.LoginPath = "/Login/Login";  // ログイン画面のパス
    });

builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// ルートを MVC に設定
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Login}/{id?}");


app.Run();
