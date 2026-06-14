using EstudioContableApp.Models;
using EstudioContableApp.Tests.Fakes;

namespace EstudioContableApp.Tests
{
    public class ClienteRepositoryTests
    {
        [Fact]
        public async Task GuardarClienteAsync_ClienteValido_DeberiaAgregarCliente()
        {
            // Arrange
            var repository = new ClienteRepositoryFake();

            var cliente = new Cliente
            {
                Nombre = "Cliente de prueba",
                Email = "cliente@test.com",
                Vencimiento = "IVA - 20/05"
            };

            // Act
            await repository.GuardarClienteAsync(cliente);
            var clientes = await repository.ObtenerClientesAsync();

            // Assert
            Assert.Single(clientes);
            Assert.Equal("Cliente de prueba", clientes[0].Nombre);
            Assert.Equal("cliente@test.com", clientes[0].Email);
        }

        [Fact]
        public async Task EliminarClienteAsync_ClienteExistente_DeberiaEliminarCliente()
        {
            // Arrange
            var repository = new ClienteRepositoryFake();

            var cliente = new Cliente
            {
                Nombre = "Cliente de prueba",
                Email = "cliente@test.com"
            };

            await repository.GuardarClienteAsync(cliente);

            // Act
            await repository.EliminarClienteAsync(cliente);
            var clientes = await repository.ObtenerClientesAsync();

            // Assert
            Assert.Empty(clientes);
        }

        [Fact]
        public async Task ObtenerClientesAsync_ConClientesGuardados_DeberiaRetornarLista()
        {
            // Arrange
            var repository = new ClienteRepositoryFake();

            var cliente = new Cliente
            {
                Nombre = "Cliente de prueba",
                Email = "cliente@test.com"
            };

            await repository.GuardarClienteAsync(cliente);

            // Act
            var clientes = await repository.ObtenerClientesAsync();

            // Assert
            Assert.NotEmpty(clientes);
            Assert.Contains(clientes, c => c.Email == "cliente@test.com");
        }
    }
}