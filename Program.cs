using ApbdProject.Context;
using ApbdProject.Controllers;
using ApbdProject.Repositories.RepImplementations;
using ApbdProject.Repositories.RepInterfaces;
using ApbdProject.Services.ServImplementations;
using ApbdProject.Services.ServInterfaces;
using Microsoft.EntityFrameworkCore;
using Project.Entities;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IClientsService, ClientsService>();
builder.Services.AddScoped<ICompaniesRepository, CompaniesRepository>();
builder.Services.AddScoped<IIndividualsRepository, IndividualRepository>();
builder.Services.AddScoped<IContractsService, ContractsService>();
builder.Services.AddScoped<IContractsRepository, ContractsRepository>();
builder.Services.AddScoped<IVersionsRepository, VersionsRepository>();
builder.Services.AddScoped<IDiscountsRepository, DiscountsRepository>();
builder.Services.AddDbContext<MyContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();

