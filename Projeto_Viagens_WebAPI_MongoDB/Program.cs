using Microsoft.Extensions.Options;
using Projeto_Viagens_WebAPI_MongoDB.Config;
using Projeto_Viagens_WebAPI_MongoDB.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configuration Singleton and AppSettings parameters
builder.Services.Configure<ProjetoViagensWebAPIMongoDBSettings>(builder.Configuration.GetSection("ProjetoViagensWebAPIMongoDBSettings"));
builder.Services.AddSingleton<IProjetoViagensWebAPIMongoDBSettings>(x => x.GetRequiredService<IOptions<ProjetoViagensWebAPIMongoDBSettings>>().Value);
builder.Services.AddSingleton<CityService>();
builder.Services.AddSingleton<AddressService>();
builder.Services.AddSingleton<CustomerService>();

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
