using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace EstudioContableApp.ViewModels
{
    [QueryProperty("Nombre", "nombre")]
    [QueryProperty("Email", "email")]
    [QueryProperty("Vencimiento", "vencimiento")]

    // usamos ObservableObject del toolkit en lugar de INotifyPropertyChanged manual
    public partial class DetalleClienteViewModel : ObservableObject
    {
        [ObservableProperty]
        private string nombre = string.Empty;

        public string Iniciales
        {
            get
            {
                if (string.IsNullOrWhiteSpace(Nombre))
                    return "?";

                var partes = Nombre.Split(' ', StringSplitOptions.RemoveEmptyEntries);

                if (partes.Length == 1)
                    return partes[0][0].ToString().ToUpper();

                return $"{partes[0][0]}{partes[1][0]}".ToUpper();
            }
        }

        [ObservableProperty]
        private string email = string.Empty;

        [ObservableProperty]
        private string vencimiento = string.Empty;

        [ObservableProperty]
        private ImageSource? imagenComprobante;

        [ObservableProperty]
        private string mensaje = string.Empty;

        [ObservableProperty]
        private string ubicacion = "Ubicación no registrada";

        [RelayCommand]
        private async Task SeleccionarImagen()
        {
            try
            {
                var imagen = await MediaPicker.PickPhotoAsync(
                    new MediaPickerOptions
                    {
                        Title = "Seleccionar comprobante"
                    });

                if (imagen == null)
                {
                    Mensaje = "No se seleccionó ninguna imagen";
                    return;
                }

                var stream = await imagen.OpenReadAsync();

                ImagenComprobante = ImageSource.FromStream(() => stream);
                Mensaje = "Comprobante seleccionado correctamente";
            }
            catch (FeatureNotSupportedException)
            {
                Mensaje = "La galería no está disponible en este dispositivo";
            }
            catch (PermissionException)
            {
                Mensaje = "No se otorgó permiso para acceder a la galería";
            }
            catch (Exception ex)
            {
                Mensaje = $"Error al seleccionar imagen: {ex.Message}";
            }
        }

        [RelayCommand]
        private async Task TomarFoto()
        {
            try
            {
                var foto = await MediaPicker.CapturePhotoAsync();

                if (foto == null)
                {
                    Mensaje = "No se tomó ninguna foto";
                    return;
                }

                var stream = await foto.OpenReadAsync();

                ImagenComprobante = ImageSource.FromStream(() => stream);
                Mensaje = "Foto cargada correctamente";
            }
            catch (FeatureNotSupportedException)
            {
                Mensaje = "La cámara no está disponible en este dispositivo";
            }
            catch (PermissionException)
            {
                Mensaje = "No se otorgó permiso para usar la cámara";
            }
            catch (Exception ex)
            {
                Mensaje = $"Error al tomar foto: {ex.Message}";
            }
        }
        [RelayCommand]
        private async Task ObtenerUbicacion()
        {
            try
            {
                var status = await Permissions.CheckStatusAsync<Permissions.LocationWhenInUse>();

                if (status != PermissionStatus.Granted)
                    status = await Permissions.RequestAsync<Permissions.LocationWhenInUse>();

                if (status != PermissionStatus.Granted)
                {
                    Mensaje = "No se otorgó permiso para acceder a la ubicación";
                    return;
                }

                var request = new GeolocationRequest(
                    GeolocationAccuracy.Medium,
                    TimeSpan.FromSeconds(10));

                var location = await Geolocation.GetLocationAsync(request);

                if (location == null)
                {
                    Mensaje = "No se pudo obtener la ubicación actual";
                    return;
                }

                Ubicacion = $"Latitud: {location.Latitude:F6} - Longitud: {location.Longitude:F6}";
                Mensaje = "Ubicación obtenida correctamente";
            }
            catch (FeatureNotSupportedException)
            {
                Mensaje = "La geolocalización no está disponible en este dispositivo";
            }
            catch (PermissionException)
            {
                Mensaje = "Permiso de ubicación denegado";
            }
            catch (Exception ex)
            {
                Mensaje = $"Error al obtener ubicación: {ex.Message}";
            }
        }
    }
}
    