using LR.Avaliacao.Domain.Entities;
using Xunit;

namespace LR.Avaliacao.Tests.Domain
{
    public class OperadorTest
    {
        [Theory]
        [InlineData("Leandro Cesar Ruela", "123456")]
        [InlineData("Leandro Cesar", "123457")]
        public void OperadorDadosValidos(string nome, string matricula)
        {
            var Operador = new Operador(nome, new Avaliacao.Domain.ValueObjects.Matricula(matricula));
            Assert.True(Operador.Valid);
        }

        [Theory]
        [InlineData("Leandro Cesar Ruela", "AAAAAA")]
        [InlineData("Leandro Cesar Ruela", "aaaaaa")]
        [InlineData("Leandro Cesar", "000000")]
        public void OperadorDadosMatriculaInValidos(string nome, string matricula)
        {
            var Operador = new Operador(nome, new Avaliacao.Domain.ValueObjects.Matricula(matricula));
            Assert.False(Operador.Valid);
        }

        [Theory]
        [InlineData("Leandro  Cesar Ruela", "123456")]
        [InlineData("Leandro", "123456")]
        [InlineData("Leandro  ", "123456")]
        [InlineData("Leandro ", "123456")]
        public void OperadorDadosNomeInValidos(string nome, string matricula)
        {
            var Operador = new Operador(nome, new Avaliacao.Domain.ValueObjects.Matricula(matricula));
            Assert.False(Operador.Valid);
        }
    }
}
