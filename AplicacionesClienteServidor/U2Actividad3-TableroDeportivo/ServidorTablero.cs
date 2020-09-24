using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Windows;
using System.Windows.Threading;

namespace U2Actividad3_TableroDeportivo
{
   public class ServidorTablero
    {
        //Variables para el cronometro:
        private int time = 0;
        private DispatcherTimer Timer;
        
        


        public Valores ValoresTablero { get; set; } = new Valores();
        HttpListener listener;
        Dispatcher dispatcher;

        public ServidorTablero()
        {
            listener = new HttpListener();
            listener.Prefixes.Add("http://*:8080/actividad3/");
            listener.Start();
            dispatcher = Dispatcher.CurrentDispatcher;
            listener.BeginGetContext(recibirSolicitudes, null);

            //Instanciaciones para el cronómetro
            Timer = new DispatcherTimer();
            Timer.Interval = new TimeSpan(0, 0, 1);
            Timer.Tick += Timer_Tick;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (time >= 0)
            {



                if (time < 599)
                {
                    if (time % 60 == 59)
                    {
                        time++;
                        ValoresTablero.Cronometro = string.Format("0{0}:0{0}", time / 60, time % 60);
                    }

                    else if (time % 60 > 8)
                    {
                        time++;
                        ValoresTablero.Cronometro = string.Format("0{0}:{1}", time / 60, time % 60);
                    }
                    else if (time % 60 <= 8)
                    {
                        time++;
                        ValoresTablero.Cronometro = string.Format("0{0}:0{1}", time / 60, time % 60);
                    }
                }
                else
                {
                    if (time % 60 == 59)
                    {
                        time++;
                        ValoresTablero.Cronometro = string.Format("{0}:0{0}", time / 60, time % 60);
                    }

                    else if (time % 60 > 8)
                    {
                        time++;
                        ValoresTablero.Cronometro = string.Format("{0}:{1}", time / 60, time % 60);
                    }
                    else if (time % 60 <= 8)
                    {
                        time++;
                        ValoresTablero.Cronometro = string.Format("{0}:0{1}", time / 60, time % 60);
                    }
                }




            }
        }

        private void recibirSolicitudes(IAsyncResult ar)
        {
            var contexto = listener.EndGetContext(ar);
            listener.BeginGetContext(recibirSolicitudes, null);

            var url = contexto.Request.Url.LocalPath;
            if(url.EndsWith("/"))
            {
              url=  url.Remove(url.Length - 1, 1);
            }


            if(url=="/actividad3" && contexto.Request.HttpMethod=="GET")
            {
                var index = System.IO.File.ReadAllBytes("index.html");
                contexto.Response.ContentType = "text/html";
                contexto.Response.OutputStream.Write(index, 0, index.Length);
                contexto.Response.StatusCode = 200;
                contexto.Response.OutputStream.Close();
            }
            else if(url == "/actividad3" && contexto.Request.HttpMethod == "POST")
            {
                StreamReader stream = new StreamReader(contexto.Request.InputStream);
                string variables = stream.ReadToEnd();
                var datos = HttpUtility.ParseQueryString(variables);
                if(datos["equipo1"]=="" && datos["equipo2"]=="" && datos["tiempo"]==null && datos["golesequipo1"]==null && datos["golesequipo2"] == null && datos["iniciar"] == null && datos["detener"] == null)
                {
                    contexto.Response.StatusCode = 400;
                }
                else
                {
       ModificarE1(datos["equipo1"]);
                ModificarE2(datos["equipo2"]);
                ModificarTiempo(datos["tiempo"]);
                ModificarGoles(datos["golesequipo1"], datos["golesequipo2"]);
                ModificarCronometro(datos["iniciar"], datos["detener"]);
                contexto.Response.StatusCode = 200;
                contexto.Response.Redirect("/actividad3");
                }
               
            }            
            else
            {
                contexto.Response.StatusCode = 404;
            }

            contexto.Response.Close();
            
        }

        public void ModificarE1(string equipo1)
        {
            dispatcher.BeginInvoke(new Action(() =>{ 
            if(equipo1!="")
                {
                    ValoresTablero.Equipo1 = equipo1;
                }                        
            }));
        }


        public void ModificarE2(string equipo2)
        {
            dispatcher.BeginInvoke(new Action(() => {
                if (equipo2 != "")
                {
                    ValoresTablero.Equipo2 = equipo2;
                }
            }));
        }

        public void ModificarTiempo(string tiempo)
        {
            dispatcher.BeginInvoke(new Action(() => {
                if (tiempo != null)
                {
                    ValoresTablero.Tiempo = tiempo;
                }
            }));
        }

       public void  ModificarGoles(string golesequipo1, string golesequipo2)
        {
            dispatcher.BeginInvoke(new Action(() => {
                if (golesequipo1!= null)
                {
                   if(golesequipo1=="sumar")
                    {
                        ValoresTablero.Golesequipo1++;
                    }
                    else if(golesequipo1=="restar")
                    {
                        if(ValoresTablero.Golesequipo1>0)
                        {
                             ValoresTablero.Golesequipo1--;
                        }
                       
                    }
                }
                else if(golesequipo2!=null)
                {
                    if (golesequipo2 == "sumar")
                    {
                        ValoresTablero.Golesequipo2++;
                    }
                    else if(golesequipo2=="restar")
                    {
                        if(ValoresTablero.Golesequipo2 > 0)
                        ValoresTablero.Golesequipo2--;
                    }
                }
            }));
        }

        public void ModificarCronometro(string iniciar, string detener)
        {
            dispatcher.BeginInvoke(new Action(() => {
                if (iniciar != null)
                {
                    Timer.Start();
                }
                if(detener!=null)
                {
                    Timer.Stop();
                }
            }));

        }


 
        

    }
}
