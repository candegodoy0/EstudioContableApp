using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Controls;
using System.ComponentModel;
using System.Runtime.CompilerServices;

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

        [ObservableProperty]
        private string email = string.Empty;

        [ObservableProperty]
        private string vencimiento = string.Empty;

        [ObservableProperty]
        private ImageSource? imagenComprobante;

        [ObservableProperty]
        private string mensaje = string.Empty;

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
    }
}