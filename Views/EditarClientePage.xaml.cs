using EstudioContableApp.ViewModels;

namespace EstudioContableApp.Views;

public partial class EditarClientePage : ContentPage
{
    public EditarClientePage(EditarClienteViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }

    private async void OnCancelarClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("..");
    }

    private async void OnGuardarClicked(object sender, EventArgs e)
    {
        if (BindingContext is EditarClienteViewModel vm)
        {
            await vm.GuardarCommand.ExecuteAsync(null);
        }
    }
}