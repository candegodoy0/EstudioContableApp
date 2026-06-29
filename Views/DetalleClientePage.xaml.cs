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
        await Shell.Current.GoToAsync("editarcliente");
    }
}
