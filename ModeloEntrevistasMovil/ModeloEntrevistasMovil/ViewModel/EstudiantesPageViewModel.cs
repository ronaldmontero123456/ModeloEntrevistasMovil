using ModeloEntrevistasMovil.Model;
using ModeloEntrevistasMovil.Services;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Xamarin.Forms;

namespace ModeloEntrevistasMovil.ViewModel
{
    public class EstudiantesPageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public ICommand ImageButtonCommand { get; private set; }
        public ICommand ButtonCommand { get; private set; }

        private string nombre;
        public string Nombre { get => nombre; set { nombre = value; RaiseOnPropertyChanged(); } }

        private string curso;
        public string Curso { get => curso; set { curso = value; RaiseOnPropertyChanged(); } }

        private int estado;
        public int Estado { get => estado; set { estado = value; RaiseOnPropertyChanged(); } }

        private DateTime pickDate;
        public DateTime PickDate { get => pickDate; set { pickDate = value; RaiseOnPropertyChanged(); } }

        private Estudiantes _estudiantes;

        public EstudiantesPageViewModel(Estudiantes estudiantes = null)
        {
            ImageButtonCommand = new Command(GoBack);
            ButtonCommand = new Command(AddProduct);

            if(estudiantes != null)
            {
                _estudiantes = estudiantes;
                Nombre = estudiantes.Nombre;
                Curso = estudiantes.Curso;
                PickDate = estudiantes.FechaNacimiento;
                Estado = estudiantes.Estado;
            }else
                PickDate = DateTime.Now;
        }
        private async void AddProduct()
        {

            if(Math.Abs(PickDate.Year - DateTime.Now.Year) <= 16)
            {
                await App.Current.MainPage.DisplayAlert("Aviso", "El estudiante debe tener al menos  16 años de edad", "cancelar");
                return;
            }

            if (string.IsNullOrEmpty(Nombre))
            {
                await App.Current.MainPage.DisplayAlert("Aviso","Dede introducir el nombre del estudiante","cancelar");
                return;
            }

            if(string.IsNullOrEmpty(Curso))
            {
                await App.Current.MainPage.DisplayAlert("Aviso", "Dede introducir el curso del estudiante","cancelar");
                return;
            }

            if(Estado <= 0)
            {
                await App.Current.MainPage.DisplayAlert("Aviso", "Dede introducir el estado del estudiante", "cancelar");
                return;
            }

            if(_estudiantes != null)
            {
                Estudiantes estudiante = _estudiantes;
                estudiante.Nombre = Nombre;
                estudiante.FechaNacimiento = PickDate;
                estudiante.Curso = Curso;
                estudiante.Estado = Estado;

                _ = await new ApiManager().UpdateEstudiantes(estudiante);
            }
            else
            {
                Estudiantes estudiante = new Estudiantes()
                {
                    Nombre = Nombre,
                    FechaNacimiento = PickDate,
                    Curso = Curso,
                    Estado = Estado,
                };

                _ = await new ApiManager().InsertEstudiantes(estudiante);
            }

            GoBack();
        }

        private void GoBack()
        {
            App.Current.MainPage.Navigation.PopAsync(true);
        }
        public void RaiseOnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
