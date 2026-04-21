namespace EstudioContableApp.Views;

public partial class DetalleClientePage : ContentPage
{
    public DetalleClientePage()
    {
        InitializeComponent();
        BindingContext = new ViewModels.DetalleClienteViewModel();
    }
}