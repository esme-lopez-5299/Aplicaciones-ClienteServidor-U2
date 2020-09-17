using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace U2Actividad3_TableroDeportivo
{
  public class Valores : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propiedad)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propiedad));
        } 

        private string equipo1;

        public string Equipo1
        {
            get { return equipo1; }
            set { equipo1 = value;
                OnPropertyChanged("Equipo1");
            }
        }

        private string equipo2;

        public string Equipo2
        {
            get { return equipo2; }
            set { equipo2 = value;
                OnPropertyChanged("Equipo2");
            }
        }

        private string tiempo;

        public string Tiempo
        {
            get { return tiempo; }
            set { tiempo = value;

                OnPropertyChanged("Tiempo");
            }
        }


        private int golesequipo1;

        public int Golesequipo1
        {
            get { return golesequipo1; }
            set { golesequipo1 = value;
                OnPropertyChanged("Golesequipo1");
            }
        }


        private int golesequipo2;
        
        public int Golesequipo2
        {
            get { return golesequipo2; }
            set { golesequipo2 = value;
                OnPropertyChanged("Golesequipo2");
            }
        }

        private string cronometro;

        public string Cronometro
        {
            get { return cronometro; }
            set { cronometro = value;
                OnPropertyChanged("Cronometro");
            }
        }




    }
}
