using Alura.ByteBank.Dados.Repositorio;
using Alura.ByteBank.Dominio.Entidades;
using Alura.ByteBank.Dominio.Interfaces.Repositorios;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using Xunit;

namespace Alura.ByteBank.Infraestrutura.Testes
{
    public class ClienteRepositorioTestes
    {
        private readonly IClienteRepositorio _repositorio;
        public ClienteRepositorioTestes()
        {
            var servico = new ServiceCollection();
            servico.AddScoped<IClienteRepositorio, ClienteRepositorio>();
            var provedor = servico.BuildServiceProvider();
            _repositorio = provedor.GetService<IClienteRepositorio>();
        }

        [Fact]
        public void TestaAListagemDeTodosOsClientes()
        {
            //Arrange
            //Act
            var lista = _repositorio.ObterTodos();
            //Assert
            Assert.NotNull(lista);
            Assert.IsType<List<Cliente>>(lista);
        }

        [Fact]
        public void TestaObterClientePorId()
        {
            //Arrange
            //Act
            var cliente = _repositorio.ObterPorId(1);
            //Assert
            Assert.NotNull(cliente);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void TestaObterClientePorVariosIds(int id)
        {
            //Arrange
            //Act
            var cliente = _repositorio.ObterPorId(id);
            //Assert
            Assert.NotNull(cliente);
        }

        [Fact]
        public void TestaExcecaoConsultaClientePorId()
        {
            //Arrange
            //Act
            //Assert
            Assert.Throws<Exception>(
                () => _repositorio.ObterPorId(43)
            );
        }

        [Fact]
        public void TestaCriacaoDeNovoCliente()
        {
            //Arrange
            var cliente = new Cliente()
            {
                Id = 4,
                Nome = "Tomas Tejano",
                Profissao = "Arquiteto",
                CPF = "911.777.954-50",
                Identificador = Guid.NewGuid()
            };
            //Act
            var result = _repositorio.Adicionar(cliente);
            //Assert
            Assert.True(result);
        }

        [Fact]
        public void TestaExclusaoDeClienteExistente()
        {
            //Arrange
            //Act
            var result = _repositorio.Excluir(4);
            //Assert
            Assert.True(result);
        }
    }
}
