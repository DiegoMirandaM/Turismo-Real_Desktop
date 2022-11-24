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
using System.Windows.Shapes;
using MahApps.Metro.Controls;
using MahApps.Metro.Behaviors;
using MahApps.Metro.Controls.Dialogs;

using TurismoReal_Desktop_Controlador;

namespace TurismoReal_Desktop
{
    /// <summary>
    /// Interaction logic for Transporte.xaml
    /// </summary>
    public partial class Transporte : MetroWindow
    {
        private Boolean actualizando;
        private Solicitud_transporte seleccionado;

        public Transporte()
        {
            InitializeComponent();
            dg_listaSolicitudes.ItemsSource = new Solicitud_transporte().ListarSolicitudes();
            cb_conductorIda.ItemsSource = cb_conductorVuelta.ItemsSource = new Conductor().ListarTodos();
            btn_actualizarTraslado.IsEnabled = false;
        }

        private void btn_retroceder_Click(object sender, RoutedEventArgs e)
        {
            MainMenu win_mainMenu = new MainMenu();
            win_mainMenu.Show();
            this.Close();
        }

        private void btn_registrarTraslado_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_actualizarTraslado_Click(object sender, RoutedEventArgs e)
        {

        }

        private void dg_listaSolicitudes_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // Se realiza esta comprobacion ya que el evento SelectionChanged se activa tambien cuando se actualiza la tabla, esto evita sus errores.
            if (actualizando == false)
            {

                if (btn_actualizarTraslado.IsEnabled == false)
                {
                    btn_actualizarTraslado.IsEnabled = true;
                }

                seleccionado = dg_listaSolicitudes.SelectedItem as Solicitud_transporte;

                // Tomar los valores del objeto seleccionado, y ponerlos en las cajas de texto:
                if (seleccionado.SENTIDO_VIAJE == "IDA")
                {
                    // Asignar valores de la solicitud escogida:
                    tb_dirOrigen_ida.Text = seleccionado.DIR_INICIO;
                    tb_dirDest_ida.Text = seleccionado.DIR_DESTINO;
                    tb_fecIda.Text = seleccionado.FECHA_INICIO.ToString().Substring(0,10);
                    tb_pasajIda.Text = seleccionado.PASAJEROS.ToString();
                    tb_descVehIda.Text = seleccionado.descripcionVehiculo;
                    tb_patenteIda.Text = seleccionado.patente;

                    // Si tiene un conductor asignado, tomar y mostrar eso.
                    if (seleccionado.IDconductorAsignado >= 0)
                    {
                        cb_conductorIda.SelectedIndex = seleccionado.IDconductorAsignado;
                    }
                    
                    tb_precioIda.Text = seleccionado.COSTO.ToString();


                    // Limpiar campos que no estan en uso:
                    tb_dirOrigen_vuelta.Clear();
                    tb_dirDest_vuelta.Clear();
                    tb_fecVuelta.Clear();
                    tb_pasajVuelta.Clear();
                    tb_descVehVuelta.Clear();
                    cb_conductorVuelta.SelectedIndex = -1;
                    tb_precioVuelta.Clear();
                    tb_patenteVuelta.Clear();
                }
                else
                {
                    // Asignar valores de la solicitud escogida:
                    tb_dirOrigen_vuelta.Text = seleccionado.DIR_INICIO;
                    tb_dirDest_vuelta.Text = seleccionado.DIR_DESTINO;
                    tb_fecVuelta.Text = seleccionado.FECHA_INICIO.ToString().Substring(0, 10);
                    tb_pasajVuelta.Text = seleccionado.PASAJEROS.ToString();
                    tb_descVehVuelta.Text = seleccionado.descripcionVehiculo;
                    tb_patenteVuelta.Text = seleccionado.patente;

                    // Si tiene un conductor asignado, tomar y mostrar eso.
                    if (seleccionado.IDconductorAsignado >= 0)
                    {
                        cb_conductorVuelta.SelectedIndex = seleccionado.IDconductorAsignado;
                    }
                    tb_precioVuelta.Text = seleccionado.COSTO.ToString();


                    // Limpiar campos que no estan en uso:
                    tb_dirOrigen_ida.Clear();
                    tb_dirDest_ida.Clear();
                    tb_fecIda.Clear();
                    tb_pasajIda.Clear();
                    tb_descVehIda.Clear();
                    cb_conductorIda.SelectedIndex = -1;
                    tb_precioIda.Clear();
                    tb_patenteIda.Clear();
                }


            }
        }
    }
}
