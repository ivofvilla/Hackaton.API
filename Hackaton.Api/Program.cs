using Hackaton.Api.Data;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Hackaton.Api.Repository;
using Hackaton.Api.Repository.Interface;
using FluentValidation;
using Hackaton.Api.Domain.Commands.Paciente.Create;
using Hackaton.Api.Domain.Commands.Paciente.Update;
using Hackaton.Api.Domain.Commands.Agenda.Update;
using Hackaton.Api.Domain.Commands.Agenda.Delete;
using Hackaton.Api.Domain.Commands.Agenda.Create;
using Hackaton.Api.Domain.Commands.Login.Create;
using Hackaton.Api.Domain.Commands.Medico.Create;
using Hackaton.Api.Domain.Commands.Medico.Delete;
using Hackaton.Api.Domain.Commands.Medico.Update;
using Hackaton.Api.Domain.Commands.Login.Update;

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

builder.Services.AddTransient<IValidator<UpdateAgendaCommand>, UpdateAgendaValidation>();
builder.Services.AddTransient<IValidator<DeleteAgendamentoCommand>, DeleteAgendamentoValidator>();
builder.Services.AddTransient<IValidator<CreateAgendaCommand>, CreateAgendaValidator>();
builder.Services.AddTransient<IValidator<CreateUsuarioCommand>, CreateUsuarioValidator>();
builder.Services.AddTransient<IValidator<CreateMedicoCommand>, CreateMedicoValidator>();
builder.Services.AddTransient<IValidator<DeleteMedicoCommand>, DeleteMedicoValidator>();
builder.Services.AddTransient<IValidator<UpdateMedicoCommand>, UpdateMedicoValidator>();
builder.Services.AddTransient<IValidator<CreatePacienteCommand>, CreatePacienteValidator>();
builder.Services.AddTransient<IValidator<UpdatePacienteCommand>, UpdatePacienteValidator>();
builder.Services.AddTransient<IValidator<UpdateLoginCommand>, UpdateLoginValidation>();


builder.Services.AddScoped<IAgendaRepository, AgendaRepository>().Reverse();
builder.Services.AddScoped<ILoginRepository, LoginRepository>().Reverse();
builder.Services.AddScoped<IMedicoRepository, MedicoRepository>().Reverse();
builder.Services.AddScoped<IPacienteRepository, PacienteRepository>().Reverse();


//builder.Services.AddCors(o => o.AddPolicy("HakatonApi", builder =>
//{
//    builder.AllowAnyOrigin()
//           .AllowAnyMethod()
//           .AllowAnyHeader();
//}));

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

app.UseCors("HakatonApi");

app.Run();
