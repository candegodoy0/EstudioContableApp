using EstudioContableApp.Data;
using EstudioContableApp.Models;
using EstudioContableApp.Services;

namespace EstudioContableApp.Repositories
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly ClienteService _service;
        private readonly DatabaseService _database;

        public ClienteRepository()
        {
            _service = new ClienteService();
            _database = new DatabaseService();
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
    }
}