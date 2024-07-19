﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;

namespace Hackaton.Models
{
    [Table(name: "Medico", Schema = "dbo")]
    public class Medico
    {
        [Key, Column("Id")]
        public int Id { get; set; }
        [Column("Nome")]
        public string Nome { get; set; }
        [Column("Email")]
        public string Email { get; set; }
        [Column("DataNascimento")]
        public DateTime DataNascimento { get; set; }
        [Column("CRM")]
        public string CRM { get; set; }
        [Column("Especialidade")]
        public string Especialidade { get; set; }
        [Column("Ativo")]
        public bool Ativo { get; set; }
        public IEnumerable<Agenda> Agendas { get; set; } = new List<Agenda>();

        public Medico()
        { 
        }

        public Medico(string nome, string email, string senha, DateTime dataNascimento, string CRM, string especialidade)
        {
            this.Nome = nome;
            this.Email = email;
            this.DataNascimento = dataNascimento;
            this.CRM = CRM;
            this.Especialidade = especialidade;
            this.Ativo = true;
        }

        public void AdicionarAgenda(IEnumerable<Agenda> Agendas) => this.Agendas = Agendas;
    }
}