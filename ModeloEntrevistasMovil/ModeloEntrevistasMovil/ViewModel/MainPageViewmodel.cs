using ModeloEntrevistasMovil.Model;
using ModeloEntrevistasMovil.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
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

        public ObservableCollection<Estudiantes> EstudiantesToGive;
        public ICommand SearchBarCommand { get; private set; }
        public MainPageViewmodel()
        {
            EstudiantesToGive = new ObservableCollection<Estudiantes>();
            SearchBarCommand = new Command((searchbar) => FilterEstudiantes(searchbar?.ToString()));
        }

        public async void GetEstudiantes()
        {
            ApiManager apimanager = new ApiManager();
            Estudiantes = new ObservableCollection<Estudiantes>(await apimanager.GetEstudiantes());
            EstudiantesToGive = Estudiantes;
        }

        private void FilterEstudiantes(string searchbar)
        {
            if (searchbar == null)
            {
                return;
            }

            if (searchbar.Length < 3)
            {
                Estudiantes = EstudiantesToGive;
                return;
            }

            SetEstudiantes(searchbar);
        }

        private void SetEstudiantes(string searchbar)
        {
            Estudiantes = new ObservableCollection<Estudiantes>(EstudiantesToGive.Where(e => e.Nombre.ToUpper().Contains(searchbar.ToUpper())));
        }

        public void RaiseOnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
