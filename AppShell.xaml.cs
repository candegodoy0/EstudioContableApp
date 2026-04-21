namespace EstudioContableApp;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
        Routing.RegisterRoute("detalle", typeof(Views.DetalleClientePage));
    }
}