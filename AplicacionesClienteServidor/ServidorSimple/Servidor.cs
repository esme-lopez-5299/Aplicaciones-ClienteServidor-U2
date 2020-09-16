using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ServidorSimple
{
   public class Servidor
    {
        //Este es un proceso que escucha peticiones http.
        HttpListener server;

        public Servidor()
        {
            server = new HttpListener();
            //puerto y direccion. como es local será en localhost.
            //Todos los prefijos deben terminar con una '/'
            server.Prefixes.Add("http://localhost/practica1/");
            //Contexto es union entre request y response.
            
            
        }


        //Es un método call back . pq se eejecuta hasta que llega petición
        private void ResponderSolicitudes(IAsyncResult ar)
        {
            var context = server.EndGetContext(ar);

            server.BeginGetContext(ResponderSolicitudes, null);

            //Context tiene 3 propiedades. Request, response, user contiene informacion
            //de autenticacion del usuario. Se usa en usuario y contraseña.
            var peticion = context.Request;

            if(context.Request.Url.LocalPath=="/practica1")
            {
            context.Response.StatusCode = 200;
            string respuesta = "<h1>Servidor Web de pruebas</h1><p>iniciado</p>";

            byte[] buffer = Encoding.UTF8.GetBytes(respuesta);

                context.Response.ContentType = "text/html"; //MIME-formato de correo
            context.Response.OutputStream.Write(buffer, 0, buffer.Length);
            context.Response.OutputStream.Close();
            context.Response.Close();
            }

            else if(context.Request.Url.LocalPath=="/practica1/saludame" &&
                context.Request.QueryString["nombre"]!=null)
            {
                context.Response.StatusCode = 200;

                var nombre = context.Request.QueryString["nombre"];

                string respuesta = $"Hola {nombre} como estas";

                byte[] buffer = Encoding.UTF8.GetBytes(respuesta);
                context.Response.OutputStream.Write(buffer, 0, buffer.Length);
                context.Response.OutputStream.Close();
                context.Response.Close();
            }

            else
            {
                context.Response.StatusCode = 404;
                context.Response.Close();

            }


            
        }

        public void Iniciar()
        {
            //Empieza a escuchar las peticiones.
            server.Start();

            server.BeginGetContext(ResponderSolicitudes, null);
        }

        public void Detener()
        {
            //Deja de recibir peticiones
            server.Stop();
        }
    }
}
