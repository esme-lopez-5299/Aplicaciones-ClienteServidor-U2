using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace PracticaServerAlumnos
{
   public class CatalogoAlumnos
    {

        public ObservableCollection<Alumno> Alumnos { get; set; } = new ObservableCollection<Alumno>();

        public void Agregar(Alumno a)
        {
            Alumnos.Add(a);
            Guardar();
        }

        public void Editar(Alumno a)
        {
            var alumno = Alumnos.FirstOrDefault(x => x.NumeroControl == a.NumeroControl);//num control campo llave
            if (alumno!= null)
            {
                alumno.Nombre = a.Nombre;
                alumno.Carrera = a.Carrera;
                Guardar();
            }
        }

        public void Eliminar(Alumno a)
        {
            var alumno = Alumnos.FirstOrDefault(x => x.NumeroControl == a.NumeroControl);
            if(alumno!=null)
            {
                Alumnos.Remove(alumno);
                Guardar();
            }
        }

        private void Guardar()
        {
            string datos = JsonConvert.SerializeObject(Alumnos);
            File.WriteAllText("alumnos.json", datos);
        }

        private void Cargar()
        {
            if(File.Exists("alumnos.json"))
            {
                var nueva = JsonConvert.DeserializeObject<ObservableCollection<Alumno>>(
                   File.ReadAllText("alumnos.json"));

                foreach (var item in nueva)
                {
                    Alumnos.Add(item);
                }
            }
        }

        public CatalogoAlumnos()
        {
            Cargar();
        }



    }
}
