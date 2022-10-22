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
    /// Interaction logic for Dpto_inventario.xaml
    /// </summary>
    public partial class Dpto_inventario : MetroWindow
    {
        public Departamento selectedDpto;
        private Inventario selectedInventario;
        private bool actualizando;

        public Dpto_inventario()
        {
            InitializeComponent();
            lb_selectedDpto.Content = armarLabel();
            recargar_listado_inventario();
        }

        public Dpto_inventario(Departamento dpto)
        {
            InitializeComponent();
            selectedDpto = dpto;
            lb_selectedDpto.Content = armarLabel();
            recargar_listado_inventario();
        }

        private string armarLabel()
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
            if (Decimal.TryParse(tb_valor.Text.Trim(), out decimal valor) == false)
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
                recargar_listado_inventario();
                actualizando = false;

                await this.ShowMessageAsync("Registro exitoso", "El elemento especificado se ha registrado exitosamente.");
            }
            else
            {
                await this.ShowMessageAsync("El registro falló", "Algo salió mal, por favor intentelo nuevamente.");
            }
            
        }

        private async void btn_actualizar_Click(object sender, RoutedEventArgs e)
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
                recargar_listado_inventario();
                actualizando = false;

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
        }

        private void btn_cancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void recargar_listado_inventario()
        {
            dg_inventario.ItemsSource = selectedDpto.ListarInventario();
        }

        private void dg_inventario_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (actualizando == false)
            {
                selectedInventario = (Inventario)e.AddedItems[0];

                tb_nombre.Text = selectedInventario.NOMBRE;
                tb_valor.Text = selectedInventario.VALOR.ToString();
                dt_compra.SelectedDate = selectedInventario.FECHA_COMPRA;
                ck_disponible.IsChecked = selectedInventario.DISPONIBLE == "1" ? true : false;
            }
        }


    }
}
