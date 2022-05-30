using ModeloEntrevistasMovil.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ModeloEntrevistasMovil.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EstudiantesPage : ContentPage
    {
        public EstudiantesPage()
        {
            BindingContext = new EstudiantesPageViewModel();
            InitializeComponent();
        }
    }
}