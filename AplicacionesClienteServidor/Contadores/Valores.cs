using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contadores
{
  public class Valores: INotifyPropertyChanged
    {
        private int verde;

        public int ValorVerde
        {
            get { return verde; }
            set { verde = value;
                OnPropertyChanged("ValorVerde");
            }
        }


        private int rojo;

        public int ValorRojo
        {
            get { return rojo; }
            set
            {
                rojo = value;
                OnPropertyChanged("ValorRojo");
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propiedad)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propiedad));
        }
    }
}
