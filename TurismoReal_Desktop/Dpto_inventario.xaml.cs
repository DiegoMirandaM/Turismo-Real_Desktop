using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using TurismoReal_Desktop_Controlador;

namespace TurismoReal_Desktop
{
    /// <summary>
    /// Interaction logic for Dpto_inventario.xaml
    /// </summary>
    public partial class Dpto_inventario : MetroWindow
    {
        public Departamento selectedDpto;
        private Inventario selectedInventario;
        private bool actualizando;

        private Dpto win_Dpto;

        public Dpto_inventario()
        {
            InitializeComponent();
            lb_selectedDpto.Content = ArmarLabel();
            Recargar_listado_inventario();
            Alternar_habil_btns(false);
        }

        public Dpto_inventario(Departamento dpto, Dpto ventana_Dpto)
        {
            InitializeComponent();
            selectedDpto = dpto;
            lb_selectedDpto.Content = ArmarLabel();
            Recargar_listado_inventario();
            Alternar_habil_btns(false);

            win_Dpto = ventana_Dpto;
        }

        private string ArmarLabel()
        {
            string resultado = String.Concat("Departamento actualmente seleccionado: ", selectedDpto.NOMBRE, ", ", selectedDpto.DIRECCION, ", ", "n°", selectedDpto.NRO_DPTO, ", ", selectedDpto.Negocio_Ciudad.NOMBRE, ".");

            return resultado;
        }

        private async void btn_nuevoInventario_Click(object sender, RoutedEventArgs e)
        {
            // No deja proceder si faltan datos. 
            if (String.IsNullOrEmpty(tb_nombre.Text.Trim()) || String.IsNullOrEmpty(tb_valor.Text.Trim()) || dt_compra.SelectedDate == null)
            {
                await this.ShowMessageAsync("Registro fallido", "Por favor, complete todos los campos e intente nuevamente.");
                return;
            }

            // Extrae datos de los campos.
            var nombre = tb_nombre.Text.Trim();
            string disponible = (bool)ck_disponible.IsChecked ? "1" : "0";
            var fecCompra = dt_compra.SelectedDate;

            // Intenta extraer y convertir valor, y valida que valor solo contenga numeros! 
            if (Decimal.TryParse(tb_valor.Text.Replace("$", "").Replace(".", "").Trim(), out decimal valor) == false)
            {
                await this.ShowMessageAsync("Datos incorrectos", "Por favor, ingrese solo números en el valor del objeto de inventario.");
                return;
            }

            // No deja proceder si la fecha de compra es mayor a la fecha actual. 
            if (dt_compra.SelectedDate > DateTime.Now)
            {
                await this.ShowMessageAsync("Fecha futura", "Por favor, corrobore que haya seleccionado la fecha correcta.");
                return;
            }


            // Iterar por inventario actual para verificar que no se duplique inventario.
            foreach (Inventario inventario in dg_inventario.Items)
            {
                if (nombre.Replace(" ", "").ToUpper() == inventario.NOMBRE.Replace(" ", "").ToUpper())
                {
                    await this.ShowMessageAsync("Elemento duplicado", "El objeto de inventario ya se encuentra en los registros.");
                    return;
                }
            }

            Inventario inv = new Inventario();

            Boolean resultado = inv.CrearItem(selectedDpto.ID_DPTO, nombre, valor, disponible, fecCompra);

            if (resultado)
            {
                actualizando = true;

                limpiarCampos();
                actualizando = false;

                win_Dpto.RecargarInventario();

                await this.ShowMessageAsync("Registro exitoso", "El elemento especificado se ha registrado exitosamente.");
            }
            else
            {
                await this.ShowMessageAsync("El registro falló", "Algo salió mal, por favor intentelo nuevamente.");
            }

        }

        private async void btn_actualizar_Click(object sender, RoutedEventArgs e)
        {
            // Si por algun motivo el boton de actualizar esta activado sin tener seleccionado un objeto de inventario, que no deberia, retornar de inmediato en vez de crashear.
            if (selectedInventario == null)
            {
                return;
            }

            // No deja proceder si faltan datos. 
            if (String.IsNullOrEmpty(tb_nombre.Text.Trim()) || String.IsNullOrEmpty(tb_valor.Text.Trim()) || dt_compra.SelectedDate == null)
            {
                await this.ShowMessageAsync("Registro fallido", "Por favor, complete todos los campos e intente nuevamente.");
                return;
            }

            // Extrae datos de los campos.
            var nombre = tb_nombre.Text.Trim();
            string disponible = (bool)ck_disponible.IsChecked ? "1" : "0";
            var fecCompra = dt_compra.SelectedDate;

            // Intenta extraer y convertir valor, y valida que valor solo contenga numeros! 
            if (Decimal.TryParse(tb_valor.Text.Trim(), out decimal valor) == false)
            {
                await this.ShowMessageAsync("Datos incorrectos", "Por favor, ingrese solo números en el valor del objeto de inventario.");
                return;
            }

            // No deja proceder si la fecha de compra es mayor a la fecha actual. 
            if (dt_compra.SelectedDate > DateTime.Now)
            {
                await this.ShowMessageAsync("Fecha futura", "Por favor, corrobore que haya seleccionado la fecha de compra correcta.");
                return;
            }

            // No actualizar si todos los datos estan iguales. Si cambia al menos uno, actualiza. 
            if (nombre.Replace(" ", "").ToUpper() == selectedInventario.NOMBRE.Replace(" ", "").ToUpper() &&
                valor == selectedInventario.VALOR &&
                disponible == selectedInventario.DISPONIBLE &&
                fecCompra == selectedInventario.FECHA_COMPRA)
            {
                await this.ShowMessageAsync("Mismos datos", "Aún no se han modificado datos del registro original.");
                return;
            }

            Inventario inv = new Inventario();
            Boolean resultado = inv.UpdateItem(selectedInventario.ID_INVENTARIO, selectedInventario.ID_DPTO, nombre, valor, disponible, fecCompra);

            if (resultado)
            {
                actualizando = true;

                limpiarCampos();

                actualizando = false;

                win_Dpto.RecargarInventario();

                await this.ShowMessageAsync("Actualización exitosa", "El elemento especificado se ha actualizado exitosamente.");
            }
            else
            {
                await this.ShowMessageAsync("Actualización fallida", "Algo salió mal, por favor intentelo nuevamente.");
            }


        }

        private void limpiarCampos()
        {
            tb_nombre.Clear();
            tb_valor.Clear();
            ck_disponible.IsChecked = true;
            dt_compra.SelectedDate = null;

            selectedInventario = null;

            Alternar_habil_btns(false);

            Recargar_listado_inventario();
        }

        private void Alternar_habil_btns(bool estado)
        {
            btn_actualizar.IsEnabled = estado;
        }

        private void btn_cancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Recargar_listado_inventario()
        {
            Inventario inv = new Inventario();
            dg_inventario.ItemsSource = inv.ListarInventarioDeDpto(selectedDpto.ID_DPTO);
        }

        private void dg_inventario_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            selectedInventario = dg_inventario.SelectedItem as Inventario;

            if (actualizando == true || selectedInventario == null) return;

            //Recien aqui habilita boton de actualizar
            Alternar_habil_btns(true);

            tb_nombre.Text = selectedInventario.NOMBRE;
            tb_valor.Text = selectedInventario.VALOR.ToString();
            dt_compra.SelectedDate = selectedInventario.FECHA_COMPRA;
            ck_disponible.IsChecked = selectedInventario.DISPONIBLE == "1" ? true : false;
            
        }

        private void tb_valor_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^0-9]+").IsMatch(e.Text);
        }

        private void tb_valor_LostFocus(object sender, RoutedEventArgs e)
        {
            Double value;
            if (Double.TryParse(tb_valor.Text, out value))
                tb_valor.Text = value.ToString("C", new System.Globalization.CultureInfo("es-CL"));
            else
                tb_valor.Text = String.Empty;
        }
    }
}
