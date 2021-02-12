using LR.Avaliacao.Domain.Entities;
using System;
using Xunit;

namespace LR.Avaliacao.Tests.Domain
{
    public class ClienteTest
    {
        [Theory]
        [InlineData("Leandro Cesar Ruela", "01234567890", "1985-10-27")]
        public void ClienteDadosValidos(string nome, string cpf, string aniversario)
        {
            var cliente = new Cliente(nome, new Avaliacao.Domain.ValueObjects.Cpf(cpf), DateTime.Parse(aniversario));
            Assert.True(cliente.Valid);
        }
    }
}
