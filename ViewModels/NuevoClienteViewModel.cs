using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EstudioContableApp.Models;
using EstudioContableApp.Repositories;
using EstudioContableApp.Validators;
using Plugin.LocalNotification;
using Plugin.LocalNotification.Core.Models;

namespace EstudioContableApp.ViewModels;

public partial class NuevoClienteViewModel : ObservableObject
{
    private readonly IClienteRepository _repository;

    public NuevoClienteViewModel(IClienteRepository repository)
    {
        _repository = repository;
    }

    [ObservableProperty]
    private string nuevoNombre = string.Empty;

    [ObservableProperty]
    private string nuevoEmail = string.Empty;

    [ObservableProperty]
    private string mensaje = string.Empty;

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

            var cliente = new Cliente
            {
                Nombre = NuevoNombre,
                Email = NuevoEmail,
                Vencimiento = "IVA - 20/05"
            };

            await _repository.GuardarClienteAsync(cliente);

            await LocalNotificationCenter.Current.Show(new NotificationRequest
            {
                NotificationId = 200,
                Title = "Cliente registrado",
                Description = $"{cliente.Nombre} fue agregado correctamente."
            });

            if (Vibration.Default.IsSupported)
                Vibration.Default.Vibrate(TimeSpan.FromMilliseconds(200));

            NuevoNombre = string.Empty;
            NuevoEmail = string.Empty;

            await Shell.Current.DisplayAlert(
                "Éxito",
                "Cliente registrado correctamente.",
                "Aceptar");

            await Shell.Current.GoToAsync("..");
        }
        catch (Exception ex)
        {
            Mensaje = $"Error: {ex.Message}";
        }
    }
}