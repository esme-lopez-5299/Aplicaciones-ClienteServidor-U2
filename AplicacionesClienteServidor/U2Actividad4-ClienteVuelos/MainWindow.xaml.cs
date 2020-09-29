using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace U2Actividad4_ClienteVuelos
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ClienteVuelos cliente = new ClienteVuelos();

        DatosVuelo datos = new DatosVuelo();
        DatosVuelo datos3 = new DatosVuelo();
        DatosVuelo datos2 = new DatosVuelo();
        private int time = 0;
        private DispatcherTimer Timer;
        public MainWindow()
        {
            InitializeComponent();
          //  this.DataContext = datos;
            cliente.Get();
            cliente.AlHaberMovimiento += Cliente_AlHaberMovimiento;
          //  this.DataContext = datos;
            Timer = new DispatcherTimer();
            Timer.Interval = new TimeSpan(0, 0, 5);
            Timer.Tick += Timer_Tick; ;
           Timer.Start();

            btnAgregar.IsEnabled = false;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (time >= 0)
            {

                cliente.Get();
                time++;
            }
        }

        private void Cliente_AlHaberMovimiento()
        {
            dtgListaVuelos.ItemsSource = cliente.Model;
        }

        private void btnNuevo_Click(object sender, RoutedEventArgs e)
        {

            this.DataContext = datos2;
            btnAgregar.IsEnabled = true;
        }
        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            try
            {                               
                cliente.Agregar(datos2);
                btnAgregar.IsEnabled = false;
                txtDestino.Text = txtHora.Text = txtVuelo.Text = cmbEstado.Text = "";
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void btnEditar_Click(object sender, RoutedEventArgs e)
        {
            try 
            {
                datos3.Hora = txtHora.Text;
                datos3.Vuelo = txtVuelo.Text;
                datos3.Destino = txtDestino.Text;
                datos3.Estado = cmbEstado.Text;



                cliente.Editar(datos3);
                cliente.Get();
                Timer.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            if (dtgListaVuelos.SelectedIndex != -1)
            {
                try
                {
                    DatosVuelo v = new DatosVuelo();
                    v = dtgListaVuelos.SelectedItem as DatosVuelo;
                    if (MessageBox.Show($"El vuelo {v.Vuelo} está a punto de ser eliminado. ¿Desea continuar?", "Atencion",
                        MessageBoxButton.OKCancel, MessageBoxImage.Warning) == MessageBoxResult.OK)
                    {
                        cliente.Eliminar(v);
                        txtDestino.Text = txtHora.Text = txtVuelo.Text = cmbEstado.Text = "";
                        cliente.Get();
                        Timer.Start();
                    }
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Es necesario que elijas un elemento para ser eliminado.", "Atencion", MessageBoxButton.OK);
            }

        }
                     
        
        private void dtgListaVuelos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(dtgListaVuelos.SelectedItem!=null)
            {
 Timer.Stop();            
            datos = dtgListaVuelos.SelectedItem as DatosVuelo;
                txtHora.Text = datos.Hora;
                txtDestino.Text = datos.Destino;
                txtVuelo.Text = datos.Vuelo;
                cmbEstado.Text= datos.Estado;

            }

        }

        //private void btnSeleccionar_Click(object sender, RoutedEventArgs e)
        //{
           
           
        //}
    }
}
