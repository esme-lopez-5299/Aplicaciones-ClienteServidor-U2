using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace PracticaServerAlumnos
{
  public  class ServidorAlumnos
    {
        public CatalogoAlumnos Alumnos { get; set; } = new CatalogoAlumnos();

        HttpListener server;
        Dispatcher current;

        public ServidorAlumnos()
        {
            current = Dispatcher.CurrentDispatcher; //Referencia al hilo actual

            server = new HttpListener();
            server.Prefixes.Add("http://*:8080/practica4/");
            server.Start();
            server.BeginGetContext(onContext, null);
        }

        private void onContext(IAsyncResult ar)
        {
            var contexto = server.EndGetContext(ar);
            server.BeginGetContext(onContext, null);
            if(contexto.Request.Url.LocalPath=="/practica4/Alumnos")
            {
                if(contexto.Request.HttpMethod=="GET")
                {
                    var datos = JsonConvert.SerializeObject(Alumnos.Alumnos);
                    byte[] buffer = Encoding.UTF8.GetBytes(datos);
                    contexto.Response.OutputStream.Write(buffer, 0, buffer.Length);
                    
                    contexto.Response.StatusCode = 200;
                }

                if(contexto.Request.ContentType.StartsWith("application/json") && contexto.Request.ContentLength64>0)
                {

               

                StreamReader reader = new StreamReader(contexto.Request.InputStream);//Lee el input stream
                string datos = reader.ReadToEnd();//leeo toda la cadena que esté en stream. Alumno en json

                var alumno = JsonConvert.DeserializeObject<Alumno>(datos);


                current.Invoke(new Action(
                    () =>
                    {
                        if (contexto.Request.HttpMethod == "POST")//Insertar
                        {

                            {
                                Alumnos.Agregar(alumno);

                            }


                        }
                        else if (contexto.Request.HttpMethod == "PUT")//Editar
                        {
                            Alumnos.Editar(alumno);
                        }
                        else if (contexto.Request.HttpMethod == "DELETE")//Editar
                        {
                            Alumnos.Eliminar(alumno);
                        }
                    }
                    ));
                    
               
                contexto.Response.StatusCode = 200;
                }
                else
                {
                    contexto.Response.StatusCode = 400;
                }
            }
            else
            {
                contexto.Response.StatusCode = 404;
            }
            contexto.Response.Close();
        }
    }
}
