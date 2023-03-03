using Alura.ByteBank.Dominio.Entidades;
using System.Collections.Generic;

namespace Alura.ByteBank.Infraestrutura.Testes.RepositorioFake
{
    public interface IByteBankRepositorio
    {
        public List<Cliente> BuscarClientes();
        public List<Agencia> BuscarAgencias();
        public List<ContaCorrente> BuscarContasCorrentes();
        public bool AdicionarConta(ContaCorrente conta);
        public bool AdicionarAgencia(Agencia agencia);
        public bool AdicionarCliente(Cliente cliente);
    }
}
