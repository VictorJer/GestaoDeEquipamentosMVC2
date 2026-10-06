// ASP.NET Core - Aplicação Web
// Builder de um servidor web
var builder = WebApplication.CreateBuilder(args);

// MVC
builder.Services.AddControllersWithViews();

// Criação da instância do servidor web
var app = builder.Build();

// Middlewares - Funções que executam em cada chamada que o nosso servidor vai receber
app.UseStaticFiles();
app.UseRouting();
app.MapDefaultControllerRoute();

// Inicia o loop da aplicação
app.Run();