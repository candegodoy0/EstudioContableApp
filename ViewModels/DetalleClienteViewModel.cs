using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Maui.Controls;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace EstudioContableApp.ViewModels
{
    [QueryProperty("Nombre", "nombre")]
    [QueryProperty("Email", "email")]

    // usamos ObservableObject del toolkit en lugar de INotifyPropertyChanged manual
    public partial class DetalleClienteViewModel : ObservableObject
    {
        [ObservableProperty]
        private string nombre;

        [ObservableProperty]
        private string email;
    }
}