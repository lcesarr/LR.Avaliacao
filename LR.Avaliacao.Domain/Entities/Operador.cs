using Flunt.Validations;
using LR.Avaliacao.Domain.Core;
using LR.Avaliacao.Domain.ValueObjects;
using LR.Avaliacao.Util.AggregateRoot;
using System;

namespace LR.Avaliacao.Domain.Entities
{
    public class Operador : IdEntity, IAggregateRoot
    {
        public Operador(string nome, Matricula matricula)
        {
            Nome = nome;
            Matricula = matricula;

            ValidarNome();
            AddNotifications(Matricula);
        }

        public Operador(Guid id, string nome, Matricula matricula)
        {
            Id = id;
            Nome = nome;
            Matricula = matricula;

            ValidarNome();
            AddNotifications(Matricula);
        }

        public void AlterarId(Guid id)
        {
            Id = id;
        }

        private void ValidarNome()
        {
            AddNotifications(new Contract()
                .Requires()
                .IsNotNullOrWhiteSpace(Nome, nameof(Nome), "Nome não pode ser nulo ou branco")
                .HasMaxLen(Nome, 100, nameof(Nome), "Nome deve conter 100 caracteres")
                .Matchs(Nome, @"^[aA-zZ]+((\s[aA-zZ]+)+)?$", nameof(Nome), "Nome inválido")
                .IsTrue(Nome.Contains(" "), nameof(Nome), "Nome inválido"));
        }

        public string Nome { get; set; }
        public Matricula Matricula { get; set; }
    }
}
