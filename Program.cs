using TodoCheckerApp.Contexts;
using Microsoft.EntityFrameworkCore;
using TodoCheckerApp.Contexts.WLK;

var builder = WebApplication.CreateBuilder(args);
// �����FMVC ���g����悤�ɂ���
builder.Services.AddControllersWithViews();
// DbContext ��o�^�iappsettings.json �� ConnectionStrings:DefaultConnection ���g�p�j
builder.Services.AddDbContext<WalkmanContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


// --- �F�؂̐ݒ� ---
builder.Services.AddAuthentication("CookieAuth")  // �f�t�H���g�X�L�[��
    .AddCookie("CookieAuth", options =>
    {
        options.LoginPath = "/Login/Login";  // ���O�C����ʂ̃p�X
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

// ���[�g�� MVC �ɐݒ�
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Login}/{id?}");


app.Run();
