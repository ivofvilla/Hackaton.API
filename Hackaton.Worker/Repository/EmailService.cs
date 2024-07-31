using FluentValidation;
using Hackaton.Api.Repository.Interface;
using Hackaton.Models;
using Hackaton.Worker.Repository.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Hackaton.Worker.Repository
{
    public class EmailService : IEmailService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public EmailService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async void EnviarEmailAsync(string emailPaciente, string nomePaciente, string nomeMedico, DateTime dataConsulta)
        {
            string url = _configuration.GetSection("SendGrid:Endpoint").Value;

            Aviso aviso = new Aviso
            {
                Data = dataConsulta,
                Email = emailPaciente,
                Nome = nomePaciente,
                Tipo = 0,
                MensagemCorpo = $"Olá {nomePaciente},\n\nEste é um lembrete de que você tem uma consulta marcada para o dia {dataConsulta:dd/MM/yyyy HH:mm} com o Dr(a). {nomeMedico}.\n\nAtenciosamente,\nClínica Dr. Mário"
            };

            var jsonContent = JsonConvert.SerializeObject(aviso);
            var contentString = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            contentString.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            await _httpClient.PostAsync(url, contentString);
        
        }
    }
}