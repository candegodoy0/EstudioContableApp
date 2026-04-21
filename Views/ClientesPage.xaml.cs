using EstudioContableApp.ViewModels;

namespace EstudioContableApp.Views;

public partial class ClientesPage : ContentPage
{
    public ClientesPage()
    {
        InitializeComponent();

        BindingContext = new ClientesViewModel();
    }
}