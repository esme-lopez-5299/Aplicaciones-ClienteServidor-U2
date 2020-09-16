using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Collections.ObjectModel;
using System.Windows.Threading;

namespace VerbosHTTP
{
   public class Servidor
    {
        Dispatcher dispatcher;

        HttpListener listener;

        public ObservableCollection<string> Nombres { get; set; } = new ObservableCollection<string>();
        public Servidor()
        {
            dispatcher = Dispatcher.CurrentDispatcher;
            listener = new HttpListener();
            listener.Prefixes.Add("http://*:80/practica2/");
            listener.Start();
            listener.BeginGetContext(OnRequest, null);
        }

        public void OnRequest(IAsyncResult ar) 
        {
            var contexto = listener.EndGetContext(ar);
            listener.BeginGetContext(OnRequest, null);
            

            if(contexto.Request.Url.LocalPath=="/practica2/"|| contexto.Request.Url.LocalPath == "/practica2")
            {
                var buffer = File.ReadAllBytes("index.html");
                contexto.Response.ContentType = "text/html";
                contexto.Response.OutputStream.Write(buffer, 0, buffer.Length);
                
                contexto.Response.StatusCode = 200; 
            }

            else if(contexto.Request.Url.LocalPath== "/practica2/recibirnombreget" && contexto.Request.HttpMethod == "GET")
            {
                if (contexto.Request.QueryString["nombre"]!= null)
                        {
                    var nombre = contexto.Request.QueryString["nombre"];
                    Agregar(nombre);

                    contexto.Response.StatusCode = 200;
                    contexto.Response.Redirect("/practica2/");
                }
                else
                {
                    contexto.Response.StatusCode = 400;
                    contexto.Response.StatusDescription = "olvidaste escribir el nombre";
                }
            }
            else
            {
                contexto.Response.StatusCode = 404;
            }
        }

        private void Agregar(string nombre)
        {
            dispatcher.BeginInvoke(new Action(() =>
            {
                Nombres.Add(nombre);
            }));
            
        }

    }
}
