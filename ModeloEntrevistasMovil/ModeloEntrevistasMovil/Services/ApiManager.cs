using ModeloEntrevistasMovil.Model;
using Newtonsoft.Json;
using Plugin.Connectivity;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ModeloEntrevistasMovil.Services
{
    public class ApiManager
    {
        HttpClient client;
        public ApiManager()
        {

            var httpClientHandler = new HttpClientHandler();

            httpClientHandler.ServerCertificateCustomValidationCallback =
            (message, cert, chain, errors) => { return true; };

            client = new HttpClient(httpClientHandler) { BaseAddress = new Uri(Application.Current.Resources["Url"].ToString()) };
            client.Timeout.Add(new TimeSpan(0, 8, 0));
        }

        public async Task<List<Estudiantes>> GetEstudiantes()
        {

            if (!CrossConnectivity.Current.IsConnected)
            {
                await Application.Current.MainPage.DisplayAlert("Aviso", "No estas conectado a internet", "Aceptar");
                return null;
            }

            HttpResponseMessage request = await client.GetAsync("api/estudiantes");
            return request.IsSuccessStatusCode
                ? JsonConvert.DeserializeObject<List<Estudiantes>>(request.Content.ReadAsStringAsync().Result)
                : null;
        }

        public async Task<string> DeleteEstudiantes(int Id)
        {
            if (!CrossConnectivity.Current.IsConnected)
            {
                await Application.Current.MainPage.DisplayAlert("Aviso", "No estas conectado a internet", "Aceptar");
                return null;
            }

            HttpResponseMessage request = await client.GetAsync($"api/estudiantes/{Id}");
            return request.IsSuccessStatusCode
                ? request.Content.ReadAsStringAsync().Result.ToString()
                : null;
        }
        public async Task<string> UpdateEstudiantes(Estudiantes args)
        {
            if (!CrossConnectivity.Current.IsConnected)
            {
                await Application.Current.MainPage.DisplayAlert("Aviso", "No estas conectado a internet", "Aceptar");
                return null;
            }

            HttpResponseMessage request = await client.PutAsync("api/estudiantes",
                new StringContent(JsonConvert.SerializeObject(args), Encoding.UTF8, "application/json"));
            return request.IsSuccessStatusCode
                ? request.Content.ReadAsStringAsync().Result.ToString()
                : null;
        }

        public async Task<string> InsertEstudiantes(Estudiantes args)
        {
            if (!CrossConnectivity.Current.IsConnected)
            {
                await Application.Current.MainPage.DisplayAlert("Aviso", "No estas conectado a internet", "Aceptar");
                return null;
            }

            HttpResponseMessage request = await client.PostAsync("api/estudiantes",
                new StringContent(JsonConvert.SerializeObject(args), Encoding.UTF8, "application/json"));
            return request.IsSuccessStatusCode
                ? request.Content.ReadAsStringAsync().Result.ToString()
                : null;
        }
    }
}
