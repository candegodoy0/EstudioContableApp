using EstudioContableApp.Models;

namespace EstudioContableApp.Repositories
{
    /// <summary>
    /// Define las operaciones de acceso a datos para la gestión de clientes.
    /// </summary>
    public interface IClienteRepository
    {
        /// <summary>
        /// Obtiene todos los clientes almacenados.
        /// </summary>
        Task<List<Cliente>> ObtenerClientesAsync();

        /// <summary>
        /// Guarda un cliente nuevo o actualiza uno existente.
        /// </summary>
        Task GuardarClienteAsync(Cliente cliente);

        /// <summary>
        /// Elimina un cliente almacenado.
        /// </summary>
        Task EliminarClienteAsync(Cliente cliente);
    }
}