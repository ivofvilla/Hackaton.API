using Hackaton.Api.Data;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Hackaton.Api.Repository;
using Hackaton.Api.Repository.Interface;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
builder.Services.AddDbContext<DbContextClass>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("HakatonApi"))
);

builder.Services.AddScoped<IAgendaRepository, AgendaRepository>().Reverse();
builder.Services.AddScoped<ILoginRepository, LoginRepository>().Reverse();
builder.Services.AddScoped<IMedicoRepository, MedicoRepository>().Reverse();
builder.Services.AddScoped<IPacienteRepository, PacienteRepository>().Reverse();

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
