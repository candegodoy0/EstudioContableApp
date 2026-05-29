using System.Collections.ObjectModel;
using EstudioContableApp.Models;
using EstudioContableApp.Services;
using EstudioContableApp.Data;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;


namespace EstudioContableApp.ViewModels
{
    // se usa partial porque el toolkit genera codigo automaticamente por detras
    public partial class ClientesViewModel : ObservableObject
    {
        private readonly ClienteService _service = new();
        private readonly DatabaseService _database = new();

        // esta coleccion es la que se muestra en pantalla
        public ObservableCollection<Cliente> Clientes { get; set; } = new();

        // mensaje para mostrarle al usuario (estado, errores, etc)
        [ObservableProperty]
        private string mensaje;

        // cliente seleccionado en la lista
        // cuando cambia, vamos a navegar al detalle amaticamente
        [ObservableProperty]
        private Cliente clienteSeleccionado;

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
                Mensaje = "Cargando clientes desde la API...";

                var lista = await _service.ObtenerClientesAsync();

                // guardo los datos en SQLite para que queden disponibles localmente
                await _database.GuardarClientesAsync(lista);

                Clientes.Clear();

                foreach (var c in lista)
                    Clientes.Add(c);

                Mensaje = $"Se cargaron y guardaron {Clientes.Count} clientes correctamente";
            }
            catch (HttpRequestException)
            {
                // error: sin internet
                Mensaje = "No se pudo conectar a internet";

                // si no hay internet, intento mostrar los clientes guardados localmente
                var clientesLocales = await _database.ObtenerClientesAsync();

                Clientes.Clear();

                foreach (var c in clientesLocales)
                    Clientes.Add(c);

                if (Clientes.Count > 0)
                    Mensaje = $"Sin conexión. Se muestran {Clientes.Count} clientes guardados localmente";

            }
            catch (TaskCanceledException)
            {
                // timeout
                Mensaje = "La solicitud tardó demasiado";
            }
            catch (Exception ex)
            {
                // error generico
                Mensaje = $"Error inesperado: {ex.Message}";
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