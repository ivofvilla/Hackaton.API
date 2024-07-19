﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Numerics;
using System;
using System.Collections.Generic;

namespace Hackaton.Models
{
    [Table(name: "Paciente", Schema = "dbo")]
    public class Paciente
    {
        [Key, Column("Id")]
        public int Id { get; set; }
        [Column("Nome")]
        public string Nome { get; set; }
        [Column("Email")]
        public string Email { get; set; }
        [Column("DataNascimento")]
        public DateTime DataNascimento { get; set; }
        public bool Ativo { get; set; }
        public virtual IEnumerable<Agenda> Agendas { get; set; } = new List<Agenda>();

        public Paciente(string nome, string email, DateTime dataNascimento)
        {
            this.Nome = nome;
            this.Email = email;
            this.DataNascimento = dataNascimento;
            this.Ativo = true;
        }

        public void AdicionarAgenda(IEnumerable<Agenda> Agendas) => this.Agendas = Agendas;  
    }
}