using FilmesAPI.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//CONECTANDO AO BANCO DE DADOS - "STRING DE CONEXÃO ESTÁ NO APPSETINGS.JSON"

builder.Services.AddDbContext<FilmeContext>(opts =>  opts.UseSqlServer(builder.Configuration.GetConnectionString("FilmeConnection")));

//Instalar Packages Mapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Add services to the container.

builder.Services.AddControllers().AddNewtonsoftJson(); //Foi colocado o AddNewtonSoftJson para poder utilizar o verbo Patch
                                                       //Precisa baixar o package em uma versão compative com o .NET6
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
