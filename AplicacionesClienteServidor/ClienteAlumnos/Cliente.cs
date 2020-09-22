using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ClienteAlumnos
{
     public class Cliente
    {
        HttpClient cliente = new HttpClient();

        public Cliente()
        {
            cliente.BaseAddress = new Uri("http://localhost:8080/");
        }


        public async void Agregar(DatosAlumnos a)
        {

            var json = JsonConvert.SerializeObject(a);
          var result= await  cliente.PostAsync("practica4/Alumnos", new StringContent(json, Encoding.UTF8, "application/json"));
            result.EnsureSuccessStatusCode();
        }

        public async void Editar(DatosAlumnos a)
        {

            var json = JsonConvert.SerializeObject(a);
            var result = await cliente.PutAsync("practica4/Alumnos", new StringContent(json, Encoding.UTF8, "application/json"));
            result.EnsureSuccessStatusCode();
        }

        public async void Eliminar(DatosAlumnos a)
        {

            var json = JsonConvert.SerializeObject(a);
            HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Delete, "practica4/Alumnos");
            message.Content = new StringContent(json, Encoding.UTF8, "application/json");
            var result = await cliente.SendAsync(message);
            result.EnsureSuccessStatusCode();
        }
    }
}
