
using ModeloEntrevistasMovil.Model;
using ModeloEntrevistasMovil.Services;
using ModeloEntrevistasMovil.View;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace ModeloEntrevistasMovil.ViewModel
{
    public class MainPageViewmodel : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        private ObservableCollection<Estudiantes> estudiantes;
        public ObservableCollection<Estudiantes> Estudiantes { get => estudiantes; set { estudiantes = value; RaiseOnPropertyChanged(); } }

        public ObservableCollection<Estudiantes> EstudiantesToGive;
        public ICommand SearchBarCommand { get; private set; }
        public ICommand GoToAddCommand { get; private set; }

        private Estudiantes estudianteSelected;
        public Estudiantes EstudianteSelected { get => estudianteSelected; set { estudianteSelected = value; RaiseOnPropertyChanged(); if (value != null) { ItemSelected(value); } } }

        public MainPageViewmodel()
        {
            EstudiantesToGive = new ObservableCollection<Estudiantes>();
            SearchBarCommand = new Command((searchbar) => FilterEstudiantes(searchbar?.ToString()));
            GoToAddCommand = new Command(() => PushAdd());
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

        private void PushAdd(Estudiantes estudiantes = null)
        {
            App.Current.MainPage.Navigation.PushAsync(new EstudiantesPage(estudiantes));
        }

        private async void ItemSelected(Estudiantes estudiantes)
        {
            string[] buttons = new string[] { "Eliminar", "Editar" };

            string result = await App.Current.MainPage.DisplayActionSheet("", "", null, buttons);

            switch (result)
            {
                case "Eliminar":
                    await DeleteEstudiante(estudiantes.ID);
                    GetEstudiantes();
                    break;
                case "Editar":
                    PushAdd(estudiantes);
                    break;
                default:
                    GetEstudiantes();
                    break;
            }
        }

        private async Task DeleteEstudiante(int id)
        {
            bool result = await App.Current.MainPage.DisplayAlert("Aviso", "Seguro Que Desea Eliminar Estudiante", "Aceptar", "Cancelar");

            if (result)
            {
                string deleteresult = await new ApiManager().DeleteEstudiantes(id);
                await App.Current.MainPage.DisplayAlert("Aviso", string.IsNullOrEmpty(deleteresult) ?
                    "Favor Intentar Nuevamente" : deleteresult, "Aceptar");
                await new ApiManager().DeleteEstudiantes(id);
            }
        }

        public void RaiseOnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}