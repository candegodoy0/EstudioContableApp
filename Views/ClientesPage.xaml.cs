using EstudioContableApp.ViewModels;

namespace EstudioContableApp.Views;

public partial class ClientesPage : ContentPage
{
    public ClientesPage(ClientesViewModel viewModel)
    {
        InitializeComponent();

        BindingContext = viewModel;
    }
}