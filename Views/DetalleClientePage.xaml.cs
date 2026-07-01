using EstudioContableApp.ViewModels;

namespace EstudioContableApp.Views;

public partial class DetalleClientePage : ContentPage
{
    public DetalleClientePage(DetalleClienteViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }

    private async void OnVolverClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("..");
    }

    private async void OnEditarClicked(object sender, EventArgs e)
    {
        if (BindingContext is DetalleClienteViewModel vm)
        {
            await Shell.Current.GoToAsync(
                $"editarcliente?id={vm.Id}" +
                $"&nombre={Uri.EscapeDataString(vm.Nombre)}" +
                $"&email={Uri.EscapeDataString(vm.Email)}");
        }
    }
}
