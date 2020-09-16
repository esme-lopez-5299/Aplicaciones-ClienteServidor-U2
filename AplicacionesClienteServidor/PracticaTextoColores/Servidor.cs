using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace PracticaTextoColores
{
   public class Servidor: INotifyPropertyChanged
    {
        Dispatcher dispatcher;
        HttpListener listener;

        public event PropertyChangedEventHandler PropertyChanged;

        public string Descripcion { get; set; }

        public string Color { get; set; }

        public Servidor()
        {
            dispatcher = Dispatcher.CurrentDispatcher;
            listener = new HttpListener();
            listener.Prefixes.Add("http://*:80/practicacolores/");
            listener.Start();
            listener.BeginGetContext(RecibirSolicitudes, null);

        }

        private void RecibirSolicitudes(IAsyncResult ar)
        {
            var contexto = listener.EndGetContext(ar);
            listener.BeginGetContext(RecibirSolicitudes, null);

            var url = contexto.Request.Url.LocalPath;
            if (url.EndsWith("/"))
            {
                url = url.Remove(url.Length - 1, 1);
            }

            if(url=="/practicacolores")
            {
                var buffer = File.ReadAllBytes("index.html");
                contexto.Response.ContentType = "text/html";
                contexto.Response.StatusCode = 200;
                contexto.Response.OutputStream.Write(buffer, 0, buffer.Length);
                contexto.Response.OutputStream.Close();                
            }

            
            else if (contexto.Request.Url.LocalPath == "/practicacolores/valoresget" && contexto.Request.HttpMethod == "GET")
            {
                if (contexto.Request.QueryString["textoDeseado"]!="" && contexto.Request.QueryString["color"] != null)
                {
                    string texto = contexto.Request.QueryString["textoDeseado"];
                    string color = contexto.Request.QueryString["color"];
                    EditarTexto(texto, color);                    
                    contexto.Response.StatusCode = 200;
                    contexto.Response.Redirect("/practicacolores/");
                }
                else
                {
                    contexto.Response.StatusCode = 400;
                    contexto.Response.StatusDescription = "No escribiste nada en el texto deseado o no elegiste algún color";
                }
            }
            else
            {
                contexto.Response.StatusCode = 404;
            }
            contexto.Response.Close();
        }


        private void OnPropertyChanged(string property)
        {
            if(PropertyChanged!=null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }

        private void EditarTexto(string texto, string color)
        {
            this.dispatcher.Invoke(() =>  Descripcion = texto );
            this.dispatcher.Invoke(() => Color = color);
            OnPropertyChanged("Descripcion");
            OnPropertyChanged("Color");

        }
              
    }
}
