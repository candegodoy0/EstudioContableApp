namespace EstudioContableApp;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        Routing.RegisterRoute("detalle", typeof(Views.DetalleClientePage));
        Routing.RegisterRoute("nuevocliente", typeof(Views.NuevoClientePage));
        Routing.RegisterRoute("editarcliente", typeof(Views.EditarClientePage));
    }
}