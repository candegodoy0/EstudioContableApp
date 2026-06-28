using EstudioContableApp.ViewModels;

namespace EstudioContableApp.Views;

public partial class NuevoClientePage : ContentPage
{
    public NuevoClientePage(ClientesViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }

    private async void OnCancelarClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("..");
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        if (BindingContext is ClientesViewModel vm)
        {
            vm.Mensaje = "";
            vm.NuevoNombre = "";
            vm.NuevoEmail = "";
        }
    }

    private async void OnGuardarClicked(object sender, EventArgs e)
    {
        if (BindingContext is ClientesViewModel vm)
        {
            await vm.GuardarClienteCommand.ExecuteAsync(null);

            if (!string.IsNullOrWhiteSpace(vm.Mensaje) &&
                vm.Mensaje.Contains("agregado correctamente"))
            {
                await Shell.Current.GoToAsync("..");
            }
        }
    }
}