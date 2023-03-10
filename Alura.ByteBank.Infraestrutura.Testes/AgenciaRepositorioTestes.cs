using Alura.ByteBank.Dados.Repositorio;
using Alura.ByteBank.Dominio.Entidades;
using Alura.ByteBank.Dominio.Interfaces.Repositorios;
using Alura.ByteBank.Infraestrutura.Testes.RepositorioFake;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace Alura.ByteBank.Infraestrutura.Testes
{
    public class AgenciaRepositorioTestes
    {
        private readonly IAgenciaRepositorio _repositorio;
        public AgenciaRepositorioTestes()
        {
            var servico = new ServiceCollection();
            servico.AddScoped<IAgenciaRepositorio, AgenciaRepositorio>();
            var provedor = servico.BuildServiceProvider();
            _repositorio = provedor.GetService<IAgenciaRepositorio>();
        }

        [Fact]
        public void TestaAListagemDeTodasAsAgencias()
        {
            //Arrange
            //Act
            var lista = _repositorio.ObterTodos();
            //Assert
            Assert.NotNull(lista);
            Assert.IsType<List<Agencia>>(lista);
        }

        [Fact]
        public void TestaObterAgenciaPorId()
        {
            //Arrange
            //Act
            var agencia = _repositorio.ObterPorId(1);
            //Assert
            Assert.NotNull(agencia);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void TestaObterAgenciaPorVariosIds(int id)
        {
            //Arrange
            //Act
            var agencia = _repositorio.ObterPorId(id);
            //Assert
            Assert.NotNull(agencia);
        }

        [Fact]
        public void TestaCriacaoDeNovaAgencia()
        {
            //Arrange
            var agencia = new Agencia()
            {
                Id = 3,
                Endereco = "Rua dos ruais, 24",
                Identificador = Guid.NewGuid(),
                Nome = "Agencia Braba"
            };
            //Act
            var result = _repositorio.Adicionar(agencia);
            //Assert
            Assert.True(result);
        }

        [Fact]
        public void TestaExclusaoDeAgenciaExistente()
        {
            //Arrange
            //Act
            var result = _repositorio.Excluir(3);
            //Assert
            Assert.True(result);
        }

        [Fact]
        public void TestaAdicionarAgenciaMock()
        {
            //Arrange
            var agencia = new Agencia()
            {
                Nome = "Agencia Amaral",
                Identificador = Guid.NewGuid(),
                Id = 4,
                Endereco = "Rua Arthur Costa",
                Numero = 6497
            };

            var repositorioMock = new ByteBankRepositorio();
            //Act
            var adicionado = repositorioMock.AdicionarAgencia(agencia);
            //Assert
            Assert.True(adicionado);
        }
        // Aqui é utilizado a biblioteca Moq para testar o COMPORTAMENTO do método e não seu retoro, isso é um MOCK
        [Fact]
        public void TestaObterAgenciasMock()
        {

            //Arrange
            var byteBankRepositorioMock = new Mock<IByteBankRepositorio>();
            var mock = byteBankRepositorioMock.Object;
            //Act
            var lista = mock.BuscarAgencias();
            //Assert
            byteBankRepositorioMock.Verify(a => a.BuscarAgencias());

        }
    }
}
