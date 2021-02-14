using LR.Avaliacao.Domain.Entities;
using System;
using Xunit;

namespace LR.Avaliacao.Tests.Domain
{
    public class ClienteTest
    {
        [Theory]
        [InlineData("Leandro Cesar Ruela", "01234567890", "1985-10-27")]
        [InlineData("Leandro Cesar", "01234567890", "1985-10-27")]
        public void ClienteDadosValidos(string nome, string cpf, string aniversario)
        {
            var cliente = new Cliente(nome, new Avaliacao.Domain.ValueObjects.Cpf(cpf), DateTime.Parse(aniversario));
            Assert.True(cliente.Valid);
        }

        [Fact]
        public void ClienteDadosIdadeMaiorIgualValidos()
        {
            DateTime dataNascimento = DateTime.Now;
            var cliente = new Cliente("Leandro Cesar Ruela", new Avaliacao.Domain.ValueObjects.Cpf("01234567890"), DateTime.Now.AddYears(-18));
            Assert.True(cliente.Valid);
        }

        [Theory]
        [InlineData("Leandro Cesar Ruela", "23987343344", "1985-10-27")]
        [InlineData("Leandro Cesar Ruela", "321654987890", "1985-10-27")]
        [InlineData("Leandro Cesar", "11111111111", "1985-10-27")]
        [InlineData("Leandro Cesar", "22222222222", "1985-10-27")]
        [InlineData("Leandro Cesar", "33333333333", "1985-10-27")]
        [InlineData("Leandro Cesar", "44444444444", "1985-10-27")]
        [InlineData("Leandro Cesar", "55555555555", "1985-10-27")]
        [InlineData("Leandro Cesar", "66666666666", "1985-10-27")]
        [InlineData("Leandro Cesar", "77777777777", "1985-10-27")]
        [InlineData("Leandro Cesar", "88888888888", "1985-10-27")]
        [InlineData("Leandro Cesar", "99999999999", "1985-10-27")]
        [InlineData("Leandro Cesar", "00000000000", "1985-10-27")]
        public void ClienteDadosCpfInValidos(string nome, string cpf, string aniversario)
        {
            var cliente = new Cliente(nome, new Avaliacao.Domain.ValueObjects.Cpf(cpf), DateTime.Parse(aniversario));
            Assert.False(cliente.Valid);
        }

        [Theory]
        [InlineData("Leandro  Cesar Ruela", "01234567890", "1985-10-27")]
        [InlineData("Leandro", "01234567890", "1985-10-27")]
        [InlineData("Leandro  ", "01234567890", "1985-10-27")]
        [InlineData("Leandro ", "01234567890", "1985-10-27")]
        public void ClienteDadosNomeInValidos(string nome, string cpf, string aniversario)
        {
            var cliente = new Cliente(nome, new Avaliacao.Domain.ValueObjects.Cpf(cpf), DateTime.Parse(aniversario));
            Assert.False(cliente.Valid);
        }

        [Fact]
        public void ClienteDadosIdadeMenor18Validos()
        {
            var cliente = new Cliente("Leandro Cesar Ruela", new Avaliacao.Domain.ValueObjects.Cpf("01234567890"), DateTime.Now.AddYears(-17));
            Assert.False(cliente.Valid);
        }
    }
}
