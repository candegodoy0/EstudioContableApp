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
    }
}