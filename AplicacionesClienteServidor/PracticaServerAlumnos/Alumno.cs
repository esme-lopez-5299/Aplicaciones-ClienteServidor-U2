using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticaServerAlumnos
{
    public class Alumno : INotifyPropertyChanged
    {
        private string numeroControl;

        public string NumeroControl
        {
            get => numeroControl; set
            {  numeroControl=value;
                LanzarEvento("NumeroControl");
            }            
        }

        private string nombre;

        public string Nombre
        {
            get => nombre; 
           
            set { 
                nombre = value;
                LanzarEvento("Nombre");
            }
        }

        private string carrera;

        public string Carrera
        {
            get => carrera;

            set
            {
                carrera = value;
                LanzarEvento("Carrera");
            }
        }



        public event PropertyChangedEventHandler PropertyChanged;

        private void LanzarEvento(string propiedad)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propiedad));
        }
    }
}
