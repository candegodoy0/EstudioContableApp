using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace EstudioContableApp.ViewModels
{

    public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        // metodo que avisa a la interfaz que algo cambio 
        protected void OnPropertyChanged([CallerMemberName] string? propiedad = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propiedad));
        }

        // helper que evita repetir logica en cada propiedad 
        protected bool SetProperty<T>(ref T campo, T valor, [CallerMemberName] string? propiedad = null)
        {
            if (Equals(campo, valor))
                return false;

            campo = valor;
            OnPropertyChanged(propiedad);
            return true;
        }
    }
}