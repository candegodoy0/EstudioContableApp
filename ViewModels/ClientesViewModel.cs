using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EstudioContableApp.Models;
using EstudioContableApp.Repositories;
using EstudioContableApp.Validators;
using Plugin.LocalNotification;
using Plugin.LocalNotification.Core.Models;
using System.Collections.ObjectModel;


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

        [ObservableProperty]
        private string textoBusqueda = string.Empty;

        // cliente seleccionado en la lista
        // cuando cambia, cargamos sus datos en el formulario para editar
        [ObservableProperty]
        private Cliente? clienteSeleccionado;

        public int CantidadClientes => Clientes.Count;

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

                OnPropertyChanged(nameof(CantidadClientes));

                System.Diagnostics.Debug.WriteLine($"Se cargaron {Clientes.Count} clientes");
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
                    OnPropertyChanged(nameof(CantidadClientes));
                }
                else
                {
                    ClienteSeleccionado.Nombre = NuevoNombre;
                    ClienteSeleccionado.Email = NuevoEmail;

                    var index = Clientes.IndexOf(ClienteSeleccionado);

                    if (index >= 0)
                    {
                        Clientes.RemoveAt(index);
                        Clientes.Insert(index, ClienteSeleccionado);
                    }
                }
                NuevoNombre = string.Empty;
                NuevoEmail = string.Empty;
                ClienteSeleccionado = null;

                VibrarConfirmacion();

                var notification = new NotificationRequest
                {
                    NotificationId = 100,
                    Title = "Cliente guardado",
                    Description = $"Se registró correctamente {cliente.Nombre}"
                };

                await LocalNotificationCenter.Current.Show(notification);

                System.Diagnostics.Debug.WriteLine($"Cliente guardado: {cliente.Nombre}");
                Mensaje = $"Cliente {cliente.Nombre} agregado correctamente";
                await Shell.Current.GoToAsync("..");
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

            bool confirmar = await Shell.Current.DisplayAlert(
                "Confirmar eliminación",
                $"¿Desea eliminar al cliente {cliente.Nombre}?",
                "Sí",
                "Cancelar");

            if (!confirmar)
                return;

            try
            {
                await _repository.EliminarClienteAsync(cliente);

                Clientes.Remove(cliente);


                ClienteSeleccionado = null;

                VibrarConfirmacion();
                System.Diagnostics.Debug.WriteLine
                    ($"Cliente eliminado: {cliente.Nombre}");
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
                $"detalle?id={cliente.Id}" +
                $"&nombre={Uri.EscapeDataString(cliente.Nombre)}" +
                $"&email={Uri.EscapeDataString(cliente.Email)}" +
                $"&vencimiento={Uri.EscapeDataString(cliente.Vencimiento)}");
        }

        [RelayCommand]
        private async Task BuscarCliente()
        {
            try
            {
                var lista = await _repository.ObtenerClientesAsync();

                if (string.IsNullOrWhiteSpace(TextoBusqueda))
                {
                    Clientes.Clear();

                    foreach (var cliente in lista)
                        Clientes.Add(cliente);

                    Mensaje = "Mostrando todos los clientes";
                    return;
                }

                var resultados = lista
                    .Where(c => c.Nombre.Contains(TextoBusqueda,
                        StringComparison.OrdinalIgnoreCase))
                    .ToList();

                Clientes.Clear();

                foreach (var cliente in resultados)
                    Clientes.Add(cliente);

                if (resultados.Count == 0)
                    Mensaje = "No se encontró ningún cliente";
                else
                    Mensaje = $"Se encontraron {resultados.Count} cliente(s)";
            }
            catch (Exception ex)
            {
                Mensaje = $"Error en la búsqueda: {ex.Message}";
            }
        }

        private void VibrarConfirmacion()
        {
            if (Vibration.Default.IsSupported)
            {
                Vibration.Default.Vibrate(TimeSpan.FromMilliseconds(200));
            }



        }

        [RelayCommand]
        private async Task IrANuevoCliente()
        {
            await Shell.Current.GoToAsync("nuevocliente");
        }
        [RelayCommand]
        private async Task EditarCliente(Cliente cliente)
        {
            if (cliente == null)
                return;

            ClienteSeleccionado = cliente;

            NuevoNombre = cliente.Nombre;
            NuevoEmail = cliente.Email;

            cliente.EsEdicion = true;

            await Shell.Current.GoToAsync("nuevocliente");
        }
    }
}
