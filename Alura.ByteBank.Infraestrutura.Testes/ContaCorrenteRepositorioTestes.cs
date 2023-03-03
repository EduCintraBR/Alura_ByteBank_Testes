using Alura.ByteBank.Dados.Repositorio;
using Alura.ByteBank.Dominio.Entidades;
using Alura.ByteBank.Dominio.Interfaces.Repositorios;
using Alura.ByteBank.Infraestrutura.Testes.RepositorioFake;
using Alura.ByteBank.Infraestrutura.Testes.RepositorioFake.DTO;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace Alura.ByteBank.Infraestrutura.Testes
{
    public class ContaCorrenteRepositorioTestes
    {
        private readonly IContaCorrenteRepositorio _repositorio;
        public ContaCorrenteRepositorioTestes()
        {
            var servico = new ServiceCollection();
            servico.AddScoped<IContaCorrenteRepositorio, ContaCorrenteRepositorio>();
            var provedor = servico.BuildServiceProvider();
            _repositorio = provedor.GetService<IContaCorrenteRepositorio>();
        }

        [Fact]
        public void TestaAListagemDeTodasAsContasCorrentes()
        {
            //Arrange
            //Act
            var lista = _repositorio.ObterTodos();
            //Assert
            Assert.NotNull(lista);
            Assert.IsType<List<ContaCorrente>>(lista);
        }

        [Fact]
        public void TestaObterContaCorrentePorId()
        {
            //Arrange
            //Act
            var CC = _repositorio.ObterPorId(1);
            //Assert
            Assert.NotNull(CC);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void TestaObterContasCorrentesPorVariosIds(int id)
        {
            //Arrange
            //Act
            var CCs = _repositorio.ObterPorId(id);
            //Assert
            Assert.NotNull(CCs);
        }

        [Fact]
        public void TestaAtualizaSaldoDeterminadaConta()
        {
            //Arrange
            var conta = _repositorio.ObterPorId(1);
            double valorAdicionado = 50;
            conta.Saldo = valorAdicionado;
            //Act
            var result = _repositorio.Atualizar(1, conta);
            //Assert
            Assert.True(result);
        }

        [Fact]
        public void TestaInsercaoDeNovaContaCorrenteNoBancoDeDados()
        {
            //Arrange
            var conta = new ContaCorrente()
            {
                Saldo = 10,
                Identificador = Guid.NewGuid(),
                Cliente = new Cliente()
                {
                    Nome = "Eduardo Cintra",
                    CPF = "825.058.166-03",
                    Identificador = Guid.NewGuid(),
                    Profissao = "Desenvolvedor",
                    Id = 1
                },
                Agencia = new Agencia()
                {
                    Nome = "Agencia Central Coast City",
                    Identificador = Guid.NewGuid(),
                    Id = 1,
                    Endereco = "Rua das Flores, 25",
                    Numero = 174
                }
            };
            //Act
            var result = _repositorio.Adicionar(conta);
            //Assert
            Assert.True(result);
        }

        [Fact]
        public void TestaRemocaoDeContaCorrenteExistente()
        {
            //Arrange
            //Act
            var result = _repositorio.Excluir(3);
            //Assert
            Assert.True(result);
        }

        //Stub é o conceito onde voce testa o comportamento (igual ao Mock) mas tambem testa se o retorno esta correto
        [Fact]
        public void TestaConsultaPix()
        {

            //Arrange
            var chaveGuid = new Guid("a0b80d53-c0dd-4897-ab90-c0615ad80d5a");
            var pix = new PixDTO() { Chave = chaveGuid, Saldo = 10 };

            var pixRepositorioMock = new Mock<IPixRepositorio>();
            pixRepositorioMock.Setup(x => x.ConsultaPix(It.IsAny<Guid>())).Returns(pix);
            var mock = pixRepositorioMock.Object;
            //Act
            var saldo = mock.ConsultaPix(chaveGuid).Saldo;
            //Assert
            Assert.Equal(10, saldo);

        }
    }
}
