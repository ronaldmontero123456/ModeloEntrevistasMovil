using ModeloEntrevistasMovil.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ModeloEntrevistasMovil
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            BindingContext = new MainPageViewmodel();
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            (BindingContext as MainPageViewmodel).GetEstudiantes();
            base.OnAppearing();
        }
    }
}
