﻿using Alura.ByteBank.Dados.Contexto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Alura.ByteBank.Infraestrutura.Testes
{
    public class ByteBankContextoTestes
    {
        [Fact(DisplayName = "Verifica se é possivel conectar ao banco de dados")]
        public void TestaConexaoComOBancoDeDadosMySql()
        {
            //Arrange
            var contexto = new ByteBankContexto();
            bool conectado;
            //Act
            try
            {
                conectado = contexto.Database.CanConnect();
            }
            catch (Exception e)
            {
                throw new Exception("Não foi possível conectar a base de dados.");
            }
            //Assert
            Assert.True(conectado);
        }


    }
}
