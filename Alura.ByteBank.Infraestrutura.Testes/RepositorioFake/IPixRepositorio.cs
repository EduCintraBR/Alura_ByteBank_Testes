using Alura.ByteBank.Infraestrutura.Testes.RepositorioFake.DTO;
using System;

namespace Alura.ByteBank.Infraestrutura.Testes.RepositorioFake
{
    public interface IPixRepositorio
    {
        public PixDTO consultaPix(Guid pix);
    }
}
