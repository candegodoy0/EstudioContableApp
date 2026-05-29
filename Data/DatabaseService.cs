using SQLite;
using EstudioContableApp.Models;

namespace EstudioContableApp.Data
{
    public class DatabaseService
    {
        private SQLiteAsyncConnection _database;

        public async Task InitAsync()
        {
            if (_database != null)
                return;

            string dbPath = Path.Combine(
                FileSystem.AppDataDirectory,
                "estudiocontable.db3");

            _database = new SQLiteAsyncConnection(dbPath);

            await _database.CreateTableAsync<Cliente>();
        }

        // guarda una lista de clientes en la base local
        public async Task GuardarClientesAsync(List<Cliente> clientes)
        {
            await InitAsync();

            foreach (var cliente in clientes)
            {
                await _database.InsertOrReplaceAsync(cliente);
            }
        }

        // obtiene todos los clientes guardados
        public async Task<List<Cliente>> ObtenerClientesAsync()
        {
            await InitAsync();

            return await _database.Table<Cliente>().ToListAsync();
        }
    }
}