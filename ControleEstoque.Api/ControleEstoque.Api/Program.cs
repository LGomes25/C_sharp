using ControleEstoque.Infrastructure.Context;
using ControleEstoque.Infrastructure.Repositories;
using ControleEstoque.Infrastructure.Repositories.Interfaces;
using ControleEstoque.Application.Services;
using ControleEstoque.Application.Services.Interfaces;
using ControleEstoque.Api.Filters;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IProdutoService, ProdutoService>();
builder.Services.AddScoped<IProdutoRepository, ProdutoRepository>();

builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();

builder.Services.AddScoped<ILoginService, LoginService>();


// Add services to the container.

builder.Services.AddControllers(options =>
{
    options.Filters.Add<HttpResponseExceptionFilter>();
});

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<DapperContext>();

//habilitar o cors para react
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy => policy
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod());
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//habilitar o cors para react
app.UseCors("AllowAll");


app.UseAuthorization();

app.MapControllers();

app.Run();
