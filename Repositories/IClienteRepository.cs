using EstudioContableApp.Models;

namespace EstudioContableApp.Repositories
{
    public interface IClienteRepository
    {
        Task<List<Cliente>> ObtenerClientesAsync();
    }
}