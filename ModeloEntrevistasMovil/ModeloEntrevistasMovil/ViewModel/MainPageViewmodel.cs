using ModeloEntrevistasMovil.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace ModeloEntrevistasMovil.ViewModel
{
    public class MainPageViewmodel: INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        private ObservableCollection<Estudiantes> estudiantes;
        public ObservableCollection<Estudiantes> Estudiantes { get => estudiantes; set { estudiantes = value; RaiseOnPropertyChanged(); } }

        public ICommand SearchBarCommand { get; private set; }
        public MainPageViewmodel()
        {
            SearchBarCommand = new Command((searchbar) => FilterEstudiantes(searchbar?.ToString()));
        }

        private void FilterEstudiantes(string searchbar)
        {

        }

        public void RaiseOnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
