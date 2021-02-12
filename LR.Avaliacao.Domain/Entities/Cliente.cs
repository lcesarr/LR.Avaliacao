using Flunt.Validations;
using LR.Avaliacao.Domain.Core;
using LR.Avaliacao.Domain.ValueObjects;
using LR.Avaliacao.Util.AggregateRoot;
using System;

namespace LR.Avaliacao.Domain.Entities
{
    public class Cliente : IdEntity, IAggregateRoot
    {
        public Cliente(string nome, Cpf cpf, DateTime aniversario)
        {
            Nome = nome;
            Cpf = cpf;
            Aniversario = aniversario;

            ValidarNome();
            AddNotifications(Cpf);
            ValidarIdade();
        }

        public Cliente(Guid id, string nome, Cpf cpf, DateTime aniversario)
        {
            Id = id;
            Nome = nome;
            Cpf = cpf;
            Aniversario = aniversario;

            ValidarNome();
            AddNotifications(Cpf);
            ValidarIdade();
        }

        private void ValidarIdade()
        {
            AddNotifications(new Contract()
                .Requires()
                .IsTrue(Util.Validacoes.Dados.ValidarIdadeMinima(Aniversario, 18), nameof(Aniversario), "Cliente deve ter no mínimo 18 anos"));
        }

        private void ValidarNome()
        {
            AddNotifications(new Contract()
                .Requires()
                .IsNotNullOrWhiteSpace(Nome, nameof(Nome), "Nome não pode ser nulo ou branco")
                .HasMaxLen(Nome, 100, nameof(Nome), "Nome deve conter 100 caracteres"));
        }

        public string Nome { get; set; }
        public Cpf Cpf { get; set; }
        public DateTime Aniversario { get; set; }
    }
}
