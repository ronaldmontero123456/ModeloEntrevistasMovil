using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using ModeloEntrevistasMovil.Droid.Renderers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: Xamarin.Forms.ExportRenderer(handler: typeof(Xamarin.Forms.SearchBar), target: typeof(CustomSearchBarRenderer))]
namespace ModeloEntrevistasMovil.Droid.Renderers
{
    public class CustomSearchBarRenderer : SearchBarRenderer
    {
        public CustomSearchBarRenderer(Context context) : base(context)
        {
        }
        protected override void OnElementChanged(ElementChangedEventArgs<SearchBar> e)
        {
            base.OnElementChanged(e);
            if (Control != null)
                (Control.FindViewById(Resources.GetIdentifier("android:id/search_plate", null, null))).
                    SetBackgroundColor(Android.Graphics.Color.Transparent);
        }
    }
}