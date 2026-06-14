using EstudioContableApp.Models;
using EstudioContableApp.Repositories;

namespace EstudioContableApp.Tests.Fakes
{
    // repositorio falso para pruebas unitarias
    // trabaja en memoria y no usa SQLite ni API
    public class ClienteRepositoryFake : IClienteRepository
    {
        private readonly List<Cliente> _clientes = new();

        // devuelve los clientes almacenados en memoria
        public Task<List<Cliente>> ObtenerClientesAsync()
        {
            return Task.FromResult(_clientes);
        }

        // agrega un cliente a la lista local
        public Task GuardarClienteAsync(Cliente cliente)
        {
            _clientes.Add(cliente);

            return Task.CompletedTask;
        }

        // elimina un cliente de la lista local
        public Task EliminarClienteAsync(Cliente cliente)
        {
            _clientes.Remove(cliente);

            return Task.CompletedTask;
        }
    }
}