using Flunt.Validations;
using LR.Avaliacao.Domain.Core;
using LR.Avaliacao.Util.AggregateRoot;
using System;

namespace LR.Avaliacao.Domain.Entities
{
    public class Modelo : IdEntity, IAggregateRoot
    {
        public Modelo(string descricao)
        {
            Descricao = descricao;

            ValidarDescricao();
        }
        public Modelo(Guid id, string descricao)
        {
            Id = id;
            Descricao = descricao;

            ValidarDescricao();
        }

        public void AlterarId(Guid id)
        {
            Id = id;
        }

        private void ValidarDescricao()
        {
            AddNotifications(new Contract()
                .Requires()
                .IsNotNullOrWhiteSpace(Descricao, nameof(Descricao), "Descrição não pode ser nulo ou branco")
                .HasMinLen(Descricao, 3, nameof(Descricao), "A Descrição deve conter no minimo 8 caracteres")
                .HasMaxLen(Descricao, 100, nameof(Descricao), "A Descrição deve conter no máximo 8 caracteres"));
        }

        public string Descricao { get; set; }
    }
}