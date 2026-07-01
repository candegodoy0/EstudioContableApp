using EstudioContableApp.ViewModels;

namespace EstudioContableApp.Views;

public partial class ClientesPage : ContentPage
{
    public ClientesPage(ClientesViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        if (BindingContext is ClientesViewModel vm)
        {
            await vm.CargarClientesCommand.ExecuteAsync(null);
        }
    }
}