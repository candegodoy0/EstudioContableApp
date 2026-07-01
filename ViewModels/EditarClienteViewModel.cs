using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EstudioContableApp.Models;
using EstudioContableApp.Repositories;
using EstudioContableApp.Validators;

namespace EstudioContableApp.ViewModels;

[QueryProperty(nameof(Id), "id")]
[QueryProperty(nameof(Nombre), "nombre")]
[QueryProperty(nameof(Email), "email")]
public partial class EditarClienteViewModel : ObservableObject
{
    private readonly IClienteRepository _repository;

    public EditarClienteViewModel(IClienteRepository repository)
    {
        _repository = repository;
    }

    [ObservableProperty]
    private int id;

    [ObservableProperty]
    private string nombre = "";

    [ObservableProperty]
    private string email = "";

    [RelayCommand]
    private async Task Guardar()
    {
        if (!ClienteValidator.NombreValido(Nombre))
        {
            await Application.Current.MainPage.DisplayAlert(
                "Error",
                "Ingrese un nombre válido.",
                "Aceptar");
            return;
        }

        if (!ClienteValidator.EmailValido(Email))
        {
            await Application.Current.MainPage.DisplayAlert(
                "Error",
                "Ingrese un email válido.",
                "Aceptar");
            return;
        }

        var cliente = new Cliente
        {
            Id = Id,
            Nombre = Nombre,
            Email = Email,
            Vencimiento = "IVA - 20/05"
        };

        await _repository.GuardarClienteAsync(cliente);

        await Shell.Current.GoToAsync("..");
    }
}