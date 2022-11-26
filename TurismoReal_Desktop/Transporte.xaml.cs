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
            btn_rechazarTraslado.IsEnabled = false;
            btn_aceptarTraslado.IsEnabled = false;
        }

        private void btn_retroceder_Click(object sender, RoutedEventArgs e)
        {
            MainMenu win_mainMenu = new MainMenu();
            win_mainMenu.Show();
            this.Close();
        }
        private void dg_listaSolicitudes_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // Se realiza esta comprobacion ya que el evento SelectionChanged se activa tambien cuando se actualiza la tabla, esto evita sus errores.
            seleccionado = dg_listaSolicitudes.SelectedItem as Solicitud_transporte;

            // Tomar los valores del objeto seleccionado, y ponerlos en las cajas de texto CORESPONDIENTE A VIAJE DE IDA. ELSE = VUELTA:
            if (seleccionado.SENTIDO_VIAJE == "IDA")
            {
                // Asignar valores de la solicitud escogida:
                tb_dirOrigen_ida.Text = seleccionado.DIR_INICIO;
                tb_dirDest_ida.Text = seleccionado.DIR_DESTINO;
                tb_fecIda.Text = seleccionado.FECHA_INICIO_VIAJE.ToString().Substring(0, 10);
                tb_pasajIda.Text = seleccionado.PASAJEROS.ToString();
                tb_descVehIda.Text = seleccionado.descripcionVehiculo;
                tb_patenteIda.Text = seleccionado.patente;

                // Si tiene un conductor asignado, tomar y mostrar eso.
                if (seleccionado.IDconductorAsignado > -1)
                {
                    //int.TryParse(cb_conductorIda.SelectedValue.ToString(), out idConductor);
                    cb_conductorIda.SelectedValue = seleccionado.IDconductorAsignado;
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

                // Activar campos del sentido actual:
                tb_descVehIda.IsEnabled = true;
                tb_descVehIda.Background = Brushes.Transparent;
                cb_conductorIda.IsEnabled = true;
                cb_conductorIda.Background = Brushes.Transparent;
                tb_patenteIda.IsEnabled = true;
                tb_patenteIda.Background = Brushes.Transparent;

                // Y temporalmente desactivar campos del sentido opuesto:
                tb_descVehVuelta.IsEnabled = false;
                tb_descVehVuelta.Background = Brushes.LightGray;
                cb_conductorVuelta.IsEnabled = false;
                cb_conductorVuelta.Background = Brushes.LightGray;
                tb_patenteVuelta.IsEnabled = false;
                tb_patenteVuelta.Background = Brushes.LightGray;

            }
            else
            {
                // Asignar valores de la solicitud escogida:
                tb_dirOrigen_vuelta.Text = seleccionado.DIR_INICIO;
                tb_dirDest_vuelta.Text = seleccionado.DIR_DESTINO;
                tb_fecVuelta.Text = seleccionado.FECHA_INICIO_VIAJE.ToString().Substring(0, 10);
                tb_pasajVuelta.Text = seleccionado.PASAJEROS.ToString();
                tb_descVehVuelta.Text = seleccionado.descripcionVehiculo;
                tb_patenteVuelta.Text = seleccionado.patente;

                // Si tiene un conductor asignado, tomar y mostrar eso.
                if (seleccionado.IDconductorAsignado > -1)
                {
                    cb_conductorVuelta.SelectedValue = seleccionado.IDconductorAsignado;
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


                // Activar campos del sentido actual:
                tb_descVehVuelta.IsEnabled = true;
                tb_descVehVuelta.Background = Brushes.Transparent;
                cb_conductorVuelta.IsEnabled = true;
                cb_conductorVuelta.Background = Brushes.Transparent;
                tb_patenteVuelta.IsEnabled = true;
                tb_patenteVuelta.Background = Brushes.Transparent;

                // Y temporalmente desactivar campos del sentido opuesto:
                tb_descVehIda.IsEnabled = false;
                tb_descVehIda.Background = Brushes.LightGray;
                cb_conductorIda.IsEnabled = false;
                cb_conductorIda.Background = Brushes.LightGray;
                tb_patenteIda.IsEnabled = false;
                tb_patenteIda.Background = Brushes.LightGray;
            }

            // Si la solicitud seleccionada ya esta aceptada, dar la opcion de actualizar sus datos, o rechazarla.
            if (seleccionado.bool_aceptada)
            {
                btn_actualizarTraslado.IsEnabled = true;
                btn_rechazarTraslado.IsEnabled = true;
                btn_aceptarTraslado.IsEnabled = false;
            }
            // Si no esta aceptada, ofrecer aceptarla solamente
            else
            {
                btn_actualizarTraslado.IsEnabled = false;
                btn_rechazarTraslado.IsEnabled = false;
                btn_aceptarTraslado.IsEnabled = true;
            }

        }

        private async void btn_aceptarTraslado_Click(object sender, RoutedEventArgs e)
        {
            // Primero, revisar el sentido del viaje para saber de que campos extraer la informacion,
            // y declarar variables a utilizar:
            string sentido = seleccionado.SENTIDO_VIAJE;

            decimal idSolicitud = seleccionado.ID_SOLICITUD;
            string desc_vehiculo;
            string patente;
            int idConductor;

            // Tomar valores de los campos dependiendo del sentido del viaje:
            if (sentido == "IDA")
            {
                if (cb_conductorIda.SelectedIndex == -1) {
                    await this.ShowMessageAsync("Faltan datos del viaje de ida", "Por favor, complete todos los datos e intente nuevamente.");
                    return;
                }

                int.TryParse(cb_conductorIda.SelectedValue.ToString(), out idConductor);

                desc_vehiculo = tb_descVehIda.Text.Trim();
                patente = tb_patenteIda.Text.Trim();
            }
            else
            {
                if (cb_conductorVuelta.SelectedIndex == -1)
                {
                    await this.ShowMessageAsync("Faltan datos del viaje de vuelta", "Por favor, complete todos los datos e intente nuevamente.");
                    return;
                }

                int.TryParse(cb_conductorVuelta.SelectedValue.ToString(), out idConductor);

                desc_vehiculo = tb_descVehVuelta.Text.Trim();
                patente = tb_patenteVuelta.Text.Trim();
            }

            // Revisar que no hayan quedado campos vacios:
            if (String.IsNullOrWhiteSpace(desc_vehiculo) ||
                String.IsNullOrWhiteSpace(patente) ||
                cb_conductorIda.SelectedIndex == -1 ||
                idConductor == -1)
            {
                await this.ShowMessageAsync("Faltan datos del viaje de Ida", "Por favor, complete todos los datos e intente nuevamente.");
                return;
            }

            // Aceptar traslado y registrar datos en Transporte_realizado (que en este sentido seria Transportes realizados o por realizar):
            bool resultado = seleccionado.AceptarTraslado(idSolicitud, idConductor, desc_vehiculo, patente);

            if (resultado)
            {
                RecargarSolicitudes();
                await this.ShowMessageAsync("Solicitud aceptada", "La solicitud se ha aceptado y se han registrado los datos especificados.");
            }
            else
            {
                await this.ShowMessageAsync("Algo salió mal...", "Por favor, revise su conexión a la base de datos e intentelo nuevamente.");
            }
        }

        private async void btn_actualizarTraslado_Click(object sender, RoutedEventArgs e)
        {
            // Primero, revisar el sentido del viaje para saber de que campos extraer la informacion,
            // y declarar variables a utilizar:
            string sentido = seleccionado.SENTIDO_VIAJE;

            decimal idSolicitud = seleccionado.ID_SOLICITUD;
            string desc_vehiculo;
            string patente;
            int idConductor;

            // Tomar valores de los campos dependiendo del sentido del viaje:
            if (sentido == "IDA")
            {
                int.TryParse(cb_conductorIda.SelectedValue.ToString(), out idConductor);
                desc_vehiculo = tb_descVehIda.Text.Trim();
                patente = tb_patenteIda.Text.Trim();
            }
            else
            {
                int.TryParse(cb_conductorVuelta.SelectedValue.ToString(), out idConductor);
                desc_vehiculo = tb_descVehVuelta.Text.Trim();
                patente = tb_patenteVuelta.Text.Trim();
            }

            // Revisar que no hayan quedado campos vacios:
            if (String.IsNullOrWhiteSpace(desc_vehiculo) ||
                String.IsNullOrWhiteSpace(patente) ||
                 idConductor == -1)
            {
                await this.ShowMessageAsync("Faltan datos del viaje", "Por favor, complete todos los datos e intente nuevamente.");
                return;
            }

            // Aceptar traslado y registrar datos en Transporte_realizado (que en este sentido seria Transportes realizados o por realizar):
            bool resultado = seleccionado.ActualizarTraslado(idSolicitud, idConductor, desc_vehiculo, patente);

            if (resultado)
            {
                RecargarSolicitudes();
                await this.ShowMessageAsync("Datos actualizados", "Los datos del traslado han sido actalizados exitosamente.");
            }
            else
            {
                await this.ShowMessageAsync("Algo salió mal...", "Por favor, revise su conexión a la base de datos e intentelo nuevamente.");
            }



        }

        private async void btn_rechazarTraslado_Click(object sender, RoutedEventArgs e)
        {
            // Detectar si ya habia sido aceptado, para pasar ese bool como parametro a la funcion de rechazarlo: 
            bool estabaAceptado = seleccionado.ACEPTADA == "1" ? true : false;

            bool resultado = seleccionado.RechazarTraslado(seleccionado.ID_SOLICITUD, estabaAceptado);

            if (resultado)
            {
                RecargarSolicitudes();
                await this.ShowMessageAsync("Solicitud rechazada", "La solicitud seleccionada ha sido rechazada exitosamente.");
            }
            else
            {
                await this.ShowMessageAsync("Algo salió mal...", "Por favor, revise su conexión a la base de datos e intentelo nuevamente.");
            }
        }

        private void RecargarSolicitudes()
        {
            // Recargar el datagrid de solicitudes:
            dg_listaSolicitudes.ItemsSource = new Solicitud_transporte().ListarSolicitudes();

            // Limpiar todos los campos dependiendo del sentido de la solicitud, y por ende de los campos que tenian datos:
            if (seleccionado.SENTIDO_VIAJE == "IDA")
            {
                tb_dirOrigen_ida.Clear();
                tb_dirDest_ida.Clear();
                tb_fecIda.Clear();
                tb_pasajIda.Clear();
                tb_descVehIda.Clear();
                cb_conductorIda.SelectedIndex = -1;
                tb_precioIda.Clear();
                tb_patenteIda.Clear();
            }
            else
            {
                tb_dirOrigen_vuelta.Clear();
                tb_dirDest_vuelta.Clear();
                tb_fecVuelta.Clear();
                tb_pasajVuelta.Clear();
                tb_descVehVuelta.Clear();
                cb_conductorVuelta.SelectedIndex = -1;
                tb_precioVuelta.Clear();
                tb_patenteVuelta.Clear();
            }

            seleccionado = null;

            btn_aceptarTraslado.IsEnabled = false;
            btn_actualizarTraslado.IsEnabled = false;
            btn_rechazarTraslado.IsEnabled = false;
        }

    }
}
