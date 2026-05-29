using EstudioContableApp.Models;

namespace EstudioContableApp.Repositories
{
    public interface IClienteRepository
    {
        Task<List<Cliente>> ObtenerClientesAsync();

        Task GuardarClienteAsync(Cliente cliente);

        Task EliminarClienteAsync(Cliente cliente);
    }
}