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
    /// Interaction logic for ServiciosExtra.xaml
    /// </summary>
    public partial class ServiciosExtra : MetroWindow
    {
        private Boolean actualizando;
        private Servicio_extra seleccionado;

        public ServiciosExtra()
        {
            InitializeComponent();
            Alternar_habil_btns(false);
            recargarListadoServicios();
        }

        private void btn_retroceder_Click(object sender, RoutedEventArgs e)
        {
            MainMenu win_mainMenu = new MainMenu();
            win_mainMenu.Show();
            this.Close();
        }

        private void recargarListadoServicios()
        {
            Servicio_extra serv = new Servicio_extra();
            dg_servicios.ItemsSource = serv.ListarTodo();
        }

        private async void btn_guardarServicio_Click(object sender, RoutedEventArgs e)
        {
            string servNombre = tb_nombre.Text.Trim();
            string servCosto_str = tb_costo.Text.Trim();

            decimal servCosto_dec;

            //Primero verifica que se haya ingresado un nombre de servicio, si esto no esta, no tiene sentido el resto de la ejecucion
            if (string.IsNullOrWhiteSpace(servNombre))
            {
                await this.ShowMessageAsync("Datos incompletos", "Por favor, ingrese el nombre del servicio e intente nuevamente.");
                return;
            }

            // Verifica que el servicio que se intenta agregar, por nombre, no se encuentre actualmente registrado.  
            foreach (Servicio_extra servicio in dg_servicios.Items)
            {
                if (servNombre == servicio.DESCRIPCION)
                {
                    await this.ShowMessageAsync("Servicio ya existe", "El servicio que intentas registrar ya figura en los registros.");
                    return;
                }
            }

            // Comprobación para evitar que falte el costo diario asociado
            if (string.IsNullOrWhiteSpace(servCosto_str))
            {
                await this.ShowMessageAsync("Datos incompletos", "Por favor, especifique un costo diario e intentelo nuevamente.");
                return;
            }

            // Comprobación para evitar letras o simbolos en el campo de costo ... 
            if (decimal.TryParse(servCosto_str, out servCosto_dec) == false)
            {
                await this.ShowMessageAsync("Registro fallido", "Ingrese solo números en el campo de costo, por favor.");
                return;
            }

            actualizando = true;

            Servicio_extra serv = new Servicio_extra();
            Boolean resultado = serv.CrearServicio(servNombre, servCosto_dec);

            if (resultado)
            {
                await this.ShowMessageAsync("Servicio registrado", "El servicio extra ha sido registrado exitosamente.");
                limpiarCampos();
                recargarListadoServicios();
            }
            else
            {
                await this.ShowMessageAsync("Algo ha salido mal", "No ha sido posible registrar el servicio extra, por favor intentelo nuevamente.");
            }

            actualizando = false;
        }

        private void limpiarCampos()
        {
            tb_nombre.Clear();
            tb_costo.Clear();

            seleccionado = null;
            Alternar_habil_btns(false);
        }

        private async void btn_actualizarServicio_Click(object sender, RoutedEventArgs e)
        {
            // Comprobacion para evitar campos vacios y datos faltantes. 
            if (String.IsNullOrWhiteSpace(tb_nombre.Text.Trim()) || String.IsNullOrWhiteSpace(tb_costo.Text.Trim()))
            {
                await this.ShowMessageAsync("Actualización fallida", "Por favor, complete todos los campos e intente nuevamente.");
                return;
            }

            string servNombre = tb_nombre.Text.Trim();

            // Comprobacion para evitar letras o simbolos en el campo de costo. 
            if (decimal.TryParse(tb_costo.Text.Trim(), out decimal servCosto) == false)
            {
                await this.ShowMessageAsync("Actualización fallida", "Ingrese solo números en el campo de costo, por favor.");
                return;
            }

            // Comprobacion para no actualizar si los datos son exactamente los mismos. 
            if (seleccionado.DESCRIPCION == tb_nombre.Text.Trim() && seleccionado.COSTO_ACTUAL.ToString() == tb_costo.Text.Trim())
            {
                await this.ShowMessageAsync("Actualización fallida", "Todavía no se han modificado los datos.");
                return;
            }

            actualizando = true;

            Boolean resultado = seleccionado.UpdateServicio(seleccionado.ID_SERVICIO, tb_nombre.Text.Trim(), servCosto);

            if (resultado)
            {
                limpiarCampos();
                recargarListadoServicios();
                await this.ShowMessageAsync("Actualización exitosa", "Los cambios han sido guardados.");
            }
            else
            {
                await this.ShowMessageAsync("Actualización fallida", "Algo salió mal, por favor intentelo nuevamente.");
            }

            actualizando = false;
        }

        private void btn_relacionarServicio_Click(object sender, RoutedEventArgs e)
        {
            ServiciosExtra_Asociar win_asociacionServicio = new ServiciosExtra_Asociar(seleccionado);
            win_asociacionServicio.ShowDialog();
        }
        
        private void Alternar_habil_btns(bool estado)
        {
            btn_actualizarServicio.IsEnabled = estado;
            btn_relacionarServicio.IsEnabled = estado;
        }

        private void tb_costo_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Aquí podría ir una manera de permitir solo caracteres numéricos, para facilitar que solo se coloquen numeros en precio. 
        }

        private void dg_servicios_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // Se realiza esta comprobacion ya que el evento SelectionChanged se activa tambien cuando se actualiza la tabla, esto evita sus errores.
            if (actualizando == false)
            {
                if (btn_actualizarServicio.IsEnabled == false)
                {
                    Alternar_habil_btns(true);
                }

                seleccionado = dg_servicios.SelectedItem as Servicio_extra;

                tb_nombre.Text = seleccionado.DESCRIPCION;
                tb_costo.Text = seleccionado.COSTO_ACTUAL.ToString();
            }
        }
    }
}
