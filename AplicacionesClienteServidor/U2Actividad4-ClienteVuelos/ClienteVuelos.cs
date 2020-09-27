using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace U2Actividad4_ClienteVuelos
{
    public class ClienteVuelos:INotifyPropertyChanged
    {

        private void LanzarEvento(string propiedad)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propiedad));
        }

        HttpClient cliente = new HttpClient();

        public ClienteVuelos()
        {
            cliente.BaseAddress = new Uri("http://vuelos.itesrc.net/");
            

        }

        public async void Agregar(DatosVuelo v)
        {
            var json = JsonConvert.SerializeObject(v);
            var result = await cliente.PostAsync("/Tablero", new StringContent(json, Encoding.UTF8, "application/json"));
            result.EnsureSuccessStatusCode();
        }


        public async void Editar(DatosVuelo v)
        {
            var json = JsonConvert.SerializeObject(v);
            var result = await cliente.PutAsync("/Tablero", new StringContent(json, Encoding.UTF8, "application/json"));
            result.EnsureSuccessStatusCode();
        }

        public async void Eliminar(DatosVuelo v)
        {
            var json = JsonConvert.SerializeObject(v);
            HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Delete, "/Tablero");
            message.Content = new StringContent(json, Encoding.UTF8, "application/json");
            var result = await cliente.SendAsync(message);
            result.EnsureSuccessStatusCode();
        }


        

        public event PropertyChangedEventHandler PropertyChanged;

      

        public List<DatosVuelo> Model { get; set; }


        public async void Get()
        {
            //List<DatosVuelo> model = null;
            var client = new HttpClient();



            var response = await client.GetAsync("http://vuelos.itesrc.net/Tablero");
            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                Model = JsonConvert.DeserializeObject<List<DatosVuelo>>(jsonString);

                LanzarEvento("Model");                
            }            
        }
    }
}
