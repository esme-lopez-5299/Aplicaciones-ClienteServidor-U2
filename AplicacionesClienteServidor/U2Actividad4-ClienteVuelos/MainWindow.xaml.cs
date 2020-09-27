using System;
using System.Collections.Generic;
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

        private int time = 0;
        private DispatcherTimer Timer;
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = datos;
            cliente.Get();

            dtgListaVuelos.ItemsSource = cliente.Model;

            //Instanciaciones para un timespan
            Timer = new DispatcherTimer();
            Timer.Interval = new TimeSpan(0, 0, 1);
            Timer.Tick += Timer_Tick;
            Timer.Start();

        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if(time>=0)
            {
                
                dtgListaVuelos.ItemsSource = cliente.Model;
                time++;
            }
        }

        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                
                cliente.Agregar(datos);
                cliente.Get();
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
                
                cliente.Editar(datos);
                cliente.Get();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
                
            }
        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                
                cliente.Eliminar(datos);
                cliente.Get();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void btnActualizar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                dtgListaVuelos.ItemsSource = cliente.Model;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }
    }
}
