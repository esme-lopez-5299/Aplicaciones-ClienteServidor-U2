using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using System.Web;

namespace Contadores
{
   public class ServidorContadores
    {
        HttpListener listener;
        public Valores Valores { get; set; } = new Valores();
        Dispatcher dispatcher;

        public ServidorContadores()
        {
            listener = new HttpListener();
            listener.Prefixes.Add("http://*:8080/practica3/");
            listener.Start();

            dispatcher = Dispatcher.CurrentDispatcher; //El dispatcher es el objeto que puede
            //ejecutar cosas en un hilo-es un delegado- Este es el dispatcher del hilo principal

            listener.BeginGetContext(Recibir, null);
        }

        private void Recibir(IAsyncResult ar)
        {
            var context = listener.EndGetContext(ar);
            listener.BeginGetContext(Recibir, null);

            var url = context.Request.Url.LocalPath;
            if(url.EndsWith("/"))
            {
                url = url.Remove(url.Length - 1, 1);
            }


            if(context.Request.HttpMethod=="GET" && url=="/practica3")
            {
                var index = System.IO.File.ReadAllBytes("index.html");
                context.Response.ContentType = "text/html";
                context.Response.OutputStream.Write(index, 0, index.Length);
                context.Response.StatusCode = 200;
                context.Response.OutputStream.Close();
            }
            else if (context.Request.HttpMethod == "POST" && url == "/practica3")
            {
                //VAriable color
                StreamReader stream = new StreamReader(context.Request.InputStream);
                string variables = stream.ReadToEnd(); //color=verde&numero=5...etc
                var datos = HttpUtility.ParseQueryString(variables);
                Incrementar(datos["color"]);
                context.Response.StatusCode = 200;
                context.Response.Redirect("/practica3");
            }
            else
            {
                context.Response.StatusCode = 404;
            }
            context.Response.Close();
        }

        public void Incrementar(string color)
        {
            dispatcher.BeginInvoke(new Action(() => {
            if(color=="Rojo")
            {
                Valores.ValorRojo++;
            }
            else
            {
                Valores.ValorVerde++;
            } 
            }));
        }
    }
}
