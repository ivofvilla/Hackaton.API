create database Consultorio
go 
use Consultorio

create table Agenda
(
	Id int primary key identity(1,1),
	IdPaciente int not null,
	IdMedico int not null,
	DataAgendamento datetime not null,
	Ativo bit not null
)

create table Medico
(
	Id int primary key identity(1,1),
	Nome varchar(100) not null,
	Email varchar(100) not null,
	Senha varchar(100) not null,
	CRM varchar(100) not null,
	Especialidade varchar(100) not null,
	DataNascimento datetime not null,
	Ativo bit not null
)


create table Paciente
(
	Id int primary key identity(1,1),
	Nome varchar(100) not null,
	Email varchar(100) not null,
	Senha varchar(100) not null,
	DataNascimento datetime not null,
	Ativo bit 
)
alter table Paciente add constraint p_ativo default 1 for ativo
GO
