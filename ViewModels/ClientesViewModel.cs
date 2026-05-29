using System.Collections.ObjectModel;
using EstudioContableApp.Models;
using EstudioContableApp.Repositories;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;


namespace EstudioContableApp.ViewModels
{
    // se usa partial porque el toolkit genera codigo automaticamente por detras
    public partial class ClientesViewModel : ObservableObject
    {
        private readonly IClienteRepository _repository;

        // esta coleccion es la que se muestra en pantalla
        public ObservableCollection<Cliente> Clientes { get; set; } = new();

        // mensaje para mostrarle al usuario (estado, errores, etc)
        [ObservableProperty]
        private string mensaje;

        // cliente seleccionado en la lista
        // cuando cambia, vamos a navegar al detalle amaticamente
        [ObservableProperty]
        private Cliente clienteSeleccionado;

        // recibimos el repositorio desde MauiProgram usando Dependency Injection
        public ClientesViewModel(IClienteRepository repository)
        {
            _repository = repository;
        }

        // este metodo se ejecuta automaticamente cuando cambia clienteseleccionado
        partial void OnClienteSeleccionadoChanged(Cliente value)
        {
            if (value != null)
            {
                IrADetalle(value);
            }
        }

        // este comando reemplaza el iCommand manual
        // el toolkit lo genera automaticamente como "cargarClientesCommand"
        [RelayCommand]
        private async Task CargarClientes()
        {
            try
            {
                Mensaje = "Cargando clientes...";

                var lista = await _repository.ObtenerClientesAsync();

                Clientes.Clear();

                foreach (var c in lista)
                    Clientes.Add(c);

                Mensaje = $"Se cargaron {Clientes.Count} clientes correctamente";
            }
            catch (Exception ex)
            {
                // error generico
                Mensaje = $"Error inesperado: {ex.Message}";
            }
        }

        // elimina un cliente de la base local y de la lista visible
        [RelayCommand]
        private async Task EliminarCliente(Cliente cliente)
        {
            if (cliente == null)
                return;

            try
            {
                await _repository.EliminarClienteAsync(cliente);

                Clientes.Remove(cliente);

                Mensaje = $"Se eliminó el cliente {cliente.Nombre}";
            }
            catch (Exception ex)
            {
                Mensaje = $"Error al eliminar cliente: {ex.Message}";
            }
        }

        // navegacion al detalle pasando parametros por query
        private async void IrADetalle(Cliente cliente)
        {
            await Shell.Current.GoToAsync(
                $"detalle?nombre={cliente.Nombre}&email={cliente.Email}");
        }
    }
}