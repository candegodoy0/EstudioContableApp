namespace EstudioContableApp.Views;

public partial class DetalleClientePage : ContentPage
{
    public DetalleClientePage()
    {
        InitializeComponent();
        BindingContext = new ViewModels.DetalleClienteViewModel();
    }

    private async void OnVolverClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("..");
    }
}