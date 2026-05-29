using EstudioContableApp.Data;
using EstudioContableApp.Models;
using EstudioContableApp.Services;

namespace EstudioContableApp.Repositories
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly ClienteService _service;
        private readonly DatabaseService _database;

        // recibimos las dependencias desde afuera
        // esto evita crear los servicios manualmente con new

        public ClienteRepository(ClienteService service, DatabaseService database)
        {
            _service = service;
            _database = database;
        }

        public async Task<List<Cliente>> ObtenerClientesAsync()
        {
            try
            {
                // obtengo clientes desde la API
                var clientes = await _service.ObtenerClientesAsync();

                // los guardo localmente
                await _database.GuardarClientesAsync(clientes);

                return clientes;
            }
            catch
            {
                // si falla internet, uso los datos locales
                return await _database.ObtenerClientesAsync();
            }
        }

        public async Task GuardarClienteAsync(Cliente cliente)
        {
            await _database.GuardarClienteAsync(cliente);
        }

        public async Task EliminarClienteAsync(Cliente cliente)
        {
            await _database.EliminarClienteAsync(cliente);
        }
    }
}