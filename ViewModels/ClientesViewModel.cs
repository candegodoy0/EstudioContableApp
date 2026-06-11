using System.Collections.ObjectModel;
using EstudioContableApp.Models;
using EstudioContableApp.Repositories;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EstudioContableApp.Validators;


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
        private string mensaje = string.Empty;

        // datos del nuevo cliente
        [ObservableProperty]
        private string nuevoNombre = string.Empty;

        [ObservableProperty]
        private string nuevoEmail = string.Empty;

        // cliente seleccionado en la lista
        // cuando cambia, cargamos sus datos en el formulario para editar
        [ObservableProperty]
        private Cliente? clienteSeleccionado;

        // recibimos el repositorio desde MauiProgram usando Dependency Injection
        public ClientesViewModel(IClienteRepository repository)
        {
            _repository = repository;
        }

        // este metodo se ejecuta automaticamente cuando cambia clienteseleccionado
        partial void OnClienteSeleccionadoChanged(Cliente? value)
        {
            if (value != null)
            {
                NuevoNombre = value.Nombre;
                NuevoEmail = value.Email;
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

        [RelayCommand]
        private async Task GuardarCliente()
        {
            try
            {
                if (!ClienteValidator.NombreValido(NuevoNombre))
                {
                    Mensaje = "Debe ingresar un nombre";
                    return;
                }

                if (!ClienteValidator.EmailValido(NuevoEmail))
                {
                    Mensaje = "Debe ingresar un email válido";
                    return;
                }

                var cliente = ClienteSeleccionado ?? new Cliente();

                cliente.Nombre = NuevoNombre;
                cliente.Email = NuevoEmail;
                cliente.Vencimiento = "IVA - 20/05";

                await _repository.GuardarClienteAsync(cliente);

                if (ClienteSeleccionado == null)
                {
                    Clientes.Add(cliente);
                }
                else
                {
                    var index = Clientes.IndexOf(ClienteSeleccionado);
                    if (index >= 0)
                        Clientes[index] = cliente;
                }
                NuevoNombre = string.Empty;
                NuevoEmail = string.Empty;
                ClienteSeleccionado = null;

                Mensaje = $"Cliente {cliente.Nombre} agregado correctamente";
            }
            catch (Exception ex)
            {
                Mensaje = $"Error al guardar cliente: {ex.Message}";
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

                ClienteSeleccionado = null;

                Mensaje = $"Se eliminó el cliente {cliente.Nombre}";
            }
            catch (Exception ex)
            {
                Mensaje = $"Error al eliminar cliente: {ex.Message}";
            }
        }
        // navega a la pantalla de detalle pasando los datos del cliente
        [RelayCommand]
        private async Task VerDetalleCliente(Cliente cliente)
        {
            if (cliente == null)
                return;

            await Shell.Current.GoToAsync(
                $"detalle?nombre={cliente.Nombre}&email={cliente.Email}&vencimiento={cliente.Vencimiento}");
        }
    }
}