using EstudioContableApp.ViewModels;

namespace EstudioContableApp.Views;

public partial class EditarClientePage : ContentPage
{
    public EditarClientePage(ClientesViewModel vm)
    {
        InitializeComponent();

        BindingContext = vm;

        if (vm.ClienteSeleccionado != null)
        {
            vm.NuevoNombre = vm.ClienteSeleccionado.Nombre;
            vm.NuevoEmail = vm.ClienteSeleccionado.Email;
        }
    }

    private async void OnCancelarClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("..");
    }

    private async void OnGuardarClicked(object sender, EventArgs e)
    {
        if (BindingContext is ClientesViewModel vm)
        {
            await vm.GuardarClienteCommand.ExecuteAsync(null);
        }

        await Shell.Current.GoToAsync("..");
    }
}