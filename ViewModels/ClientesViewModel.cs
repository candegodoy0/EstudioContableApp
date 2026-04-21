using System.Collections.ObjectModel;
using System.Windows.Input;
using EstudioContableApp.Models;
using EstudioContableApp.Services;

namespace EstudioContableApp.ViewModels
{
    public class ClientesViewModel : BaseViewModel
    {
        private readonly ClienteService _service = new();

        // esta coleccion es la que se muestra en la pantalla
        public ObservableCollection<Cliente> Clientes { get; set; } = new();

        private string _mensaje = string.Empty;

        // este mensaje se usa para darle feedback al usuario (cargando, error, etc.)
        public string Mensaje
        {
            get => _mensaje;
            set => SetProperty(ref _mensaje, value);
        }

        // comando que se va a usar en el boton
        public ICommand CargarClientesCommand { get; }

        public ClientesViewModel()
        {
            // se vincula el boton con el metodo
            CargarClientesCommand = new Command(async () => await CargarClientes());
        }

        // metodo que hace todo el proceso de traer los datos
        private async Task CargarClientes()
        {
            try
            {
                Mensaje = "Cargando clientes...";

                var lista = await _service.ObtenerClientesAsync();

                // se limpia la lista antes de volver a cargar
                Clientes.Clear();

                foreach (var c in lista)
                    Clientes.Add(c);

                Mensaje = $"Se cargaron {Clientes.Count} clientes correctamente";
            }
            catch (HttpRequestException)
            {
                // error tipico de conexion (sin internet)
                Mensaje = "No se pudo conectar a internet";
            }
            catch (TaskCanceledException)
            {
                // timeout o cancelacion
                Mensaje = "La solicitud tardó demasiado, intentá nuevamente";
            }
            catch (Exception)
            {
                // error generico
                Mensaje = "Ocurrió un error inesperado";
            }
        }
    }
}