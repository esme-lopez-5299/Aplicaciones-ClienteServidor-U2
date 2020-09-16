using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;

namespace ClienteHTTP
{
   public class Cliente
    {

        HttpClient cliente = new HttpClient();
        public Cliente()
        {
         cliente.BaseAddress = new Uri("http://ce210feec558.ngrok.io:8080/");
        }
        public async void IncrementarRojo()
        {
           var result=await cliente.PostAsync("/practica3", new StringContent("color=Rojo"));
            if (result.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception("No se realizó la operación");
            }
        }

        public async void IncrementarVerde()
        {
           var result= await cliente.PostAsync("/practica3", new StringContent("color=Verde"));
            if(result.StatusCode!=HttpStatusCode.OK)
            {
                throw new Exception("No se realizó la operación");
            }
        }
    }
}
