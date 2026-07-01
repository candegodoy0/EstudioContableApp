using EstudioContableApp.Data;
using EstudioContableApp.Models;
using EstudioContableApp.Services;

namespace EstudioContableApp.Repositories
{
    /// <summary>
    /// Implementación del repositorio de clientes.
    /// Se encarga de obtener y persistir información utilizando
    /// la API y la base de datos local.
    /// </summary>
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
        /// <summary>
        /// Obtiene los clientes desde la API.
        /// Si la API falla, recupera los datos almacenados localmente.
        /// </summary>
        public async Task<List<Cliente>> ObtenerClientesAsync()
        {
            // primero veo si ya existen clientes en la base local
            var clientesLocales = await _database.ObtenerClientesAsync();

            if (clientesLocales.Any())
                return clientesLocales;

            try
            {
                // si la base esta vacia, los descargo desde la API
                var clientesApi = await _service.ObtenerClientesAsync();

                await _database.GuardarClientesAsync(clientesApi);

                return await _database.ObtenerClientesAsync();
            }
            catch
            {
                return clientesLocales;
            }
        }
        /// <summary>
        /// Guarda un cliente en la base de datos local.
        /// </summary>
        public async Task GuardarClienteAsync(Cliente cliente)
        {
            await _database.GuardarClienteAsync(cliente);
        }
        /// <summary>
        /// Elimina un cliente de la base de datos local.
        /// </summary>
        public async Task EliminarClienteAsync(Cliente cliente)
        {
            await _database.EliminarClienteAsync(cliente);
        }
    }
}