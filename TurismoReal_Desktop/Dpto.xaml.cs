
using System;
using System.Windows;
using MahApps.Metro.Controls;
using MahApps.Metro.Behaviors;
using MahApps.Metro.Controls.Dialogs;

using System.Collections.Generic;
using TurismoReal_Desktop_Controlador;


namespace TurismoReal_Desktop
{
    /// <summary>
    /// Interaction logic for Dpto.xaml
    /// </summary>
    public partial class Dpto : MetroWindow
    {
        private Boolean actualizando;
        private Departamento seleccionado;

        public Dpto()
        {
            InitializeComponent();
            limpiarCampos();
            recargarCiudades();
        }

        private void Alternar_habil_btns (bool estado)
        {
            btn_gestImagenes.IsEnabled = estado;
            btn_gestInventario.IsEnabled = estado;
            btn_gestMantenciones.IsEnabled = estado;
            btn_actualizarDpto.IsEnabled = estado;
            btn_disp.IsEnabled = estado;
            btn_noDisp.IsEnabled = estado;

        }

        private void btn_retroceder_Click(object sender, RoutedEventArgs e)
        {
            MainMenu win_mainMenu = new MainMenu();
            win_mainMenu.Show();
            this.Close();
        }

        private async void btn_nuevoDpto_Click(object sender, RoutedEventArgs e)
        {
            // Verifica que no falten datos antes de proceder
            if (String.IsNullOrWhiteSpace(tb_nombre.Text.Trim()) || String.IsNullOrWhiteSpace(tb_direccion.Text.Trim()) || cb_ciudad.SelectedItem == null || String.IsNullOrWhiteSpace(tb_numDpto.Text.Trim()) || String.IsNullOrWhiteSpace(tb_superficie.Text.Trim()) || String.IsNullOrWhiteSpace(tb_precio.Text.Trim()) || String.IsNullOrWhiteSpace(tb_estadoActual.Text.Trim()))
            {
                await this.ShowMessageAsync ("Registro fallido", "Por favor, complete todos los campos e intente nuevamente.");
                return;
            }

            // Toma todos los datos proporcionados en los campos de texto para comprobar que no se registre un depto duplicado, y luego lo ingresa. 
            string newDpto_NOMBRE = tb_nombre.Text.Trim();
            string newDpto_DIRECCION = tb_direccion.Text.Trim();
            Decimal.TryParse(cb_ciudad.SelectedValue.ToString(), out decimal idCiudad);
            string nroDpto = tb_numDpto.Text.Trim();
            string newDpto_SUPERFICIE_DPTO = tb_superficie.Text.Trim();
            Decimal.TryParse(tb_precio.Text.Trim(), out decimal precioDecimal);

            string newDpto_CONDICION = tb_estadoActual.Text.Trim();

            foreach (Departamento dpto in dg_listaDptos.Items)
            {
                if (newDpto_NOMBRE.ToUpper() == dpto.NOMBRE.ToUpper() || newDpto_DIRECCION.ToUpper() == dpto.DIRECCION.ToUpper() && nroDpto == dpto.NRO_DPTO)
                {
                    await this.ShowMessageAsync("Departamento duplicado", "El departamento que intenta registrar ya figura en los registros.");
                    return;
                }
            }

            actualizando = true;

            Departamento newDpto = new Departamento();
            Boolean resultado = newDpto.agregarDpto(idCiudad, newDpto_NOMBRE, newDpto_DIRECCION, newDpto_SUPERFICIE_DPTO, nroDpto, precioDecimal, "0", newDpto_CONDICION);

            
            
            if (resultado)
            {
                limpiarCampos();

                await this.ShowMessageAsync("Registro exitoso", "El nuevo departamento se ha registrado exitosamente.");
            }
            else
            {
                await this.ShowMessageAsync("El registro ha fallado", "Algo ha salido mal, por favor intentelo nuevamente.");
            }
            
            actualizando = false;
        }

        // PROBLEMA: Validar que no se puedan insertar letras en campos numericos! 
        private async void btn_actualizarDpto_Click(object sender, RoutedEventArgs e)
        {
            // Si faltan datos, detener actualizacion 
            if (String.IsNullOrWhiteSpace(tb_nombre.Text.Trim()) || 
                String.IsNullOrWhiteSpace(tb_direccion.Text.Trim()) || 
                cb_ciudad.SelectedItem == null || 
                String.IsNullOrWhiteSpace(tb_numDpto.Text.Trim()) || 
                String.IsNullOrWhiteSpace(tb_superficie.Text.Trim()) || 
                String.IsNullOrWhiteSpace(tb_precio.Text.Trim()) || 
                String.IsNullOrWhiteSpace(tb_estadoActual.Text.Trim()) )
            {
                await this.ShowMessageAsync("Datos incompletos", "Por favor, no deje campos vacíos para poder actualizar.");
                return;
            }

            // Se obtienen los datos de los campos de texto para comparar con los datos actuales, y posteriormente actualizar si hubo al menos un cambio
            string newDpto_NOMBRE = tb_nombre.Text.Trim();
            string newDpto_DIRECCION = tb_direccion.Text.Trim();
            Decimal.TryParse(cb_ciudad.SelectedValue.ToString(), out decimal idCiudad);
            string nroDpto = tb_numDpto.Text.Trim();
            var newDpto_SUPERFICIE_DPTO = tb_superficie.Text.Trim();
            Decimal.TryParse(tb_precio.Text.Trim(), out decimal precioDecimal);
            string newDpto_estado = tb_estadoActual.Text.Trim();

            // Validar que los campos numero dpto, superficie y precio por dia sean mayores a 0 y no contengan letras.

            // Si es que la superficie contiene letras o simbolos y no es casteable a numeros, detener funcion ahi. 
            if (int.TryParse(newDpto_SUPERFICIE_DPTO, out int m2) == false)
            {
                await this.ShowMessageAsync("Datos incorrectos", "Por favor, ingrese solo números en la superficie del departamento.");
                return;
            }

            // Si los campos numericos son iguales o inferiores a cero, detener ahi. 
            if (precioDecimal <= 0 || m2 <= 0)
            {
                await this.ShowMessageAsync("Datos incorrectos", "Por favor, ingrese solo números en los campos de número de departamento, precio diario y superficie del departamento.");
                return;
            }


            // No debe actualizar si es el mismo nombre, direccion, ciudad, estado, nro depto, misma superficie y mismo precio. Si cambia al menos uno, actualiza. 
            if (newDpto_NOMBRE == seleccionado.NOMBRE
                && newDpto_DIRECCION == seleccionado.DIRECCION
                && ((Ciudad)cb_ciudad.SelectedItem).NOMBRE == seleccionado.Negocio_Ciudad.NOMBRE
                && newDpto_estado == seleccionado.CONDICION 
                && nroDpto == seleccionado.NRO_DPTO 
                && newDpto_SUPERFICIE_DPTO == seleccionado.SUPERFICIE_DPTO 
                && precioDecimal == seleccionado.PRECIO_DPTO)
            {
                await this.ShowMessageAsync("Mismos datos", "Aún no se han modificado datos del registro original.");
                return;
            }
            else
            {
                // Actualización de los datos del departamento 

                actualizando = true;

                Boolean resultado = seleccionado.updateDpto(seleccionado.ID_DPTO, idCiudad, newDpto_NOMBRE, newDpto_DIRECCION, newDpto_SUPERFICIE_DPTO, nroDpto, precioDecimal, seleccionado.DISPONIBLE, newDpto_estado);

                if (resultado)
                {
                    limpiarCampos();
                    await this.ShowMessageAsync("Actualización exitosa", "Los cambios han sido guardados.");

                }
                else
                {
                    await this.ShowMessageAsync("La actualización ha fallado", "Algo ha salido mal, por favor intentelo nuevamente.");
                }

                actualizando = false;
            }
            
        }

        private void limpiarCampos(){
            tb_nombre.Clear();
            tb_direccion.Clear();
            cb_ciudad.SelectedIndex = -1;
            tb_numDpto.Clear();
            tb_superficie.Clear();
            tb_precio.Clear();
            tb_estadoActual.Clear();

            dg_inventario.ItemsSource = null;

            seleccionado = null;

            dg_listaDptos.SelectedItem = null;

            Alternar_habil_btns(false);

            RecargarListadoDpto();
        }

        private void RecargarListadoDpto(){
            Departamento dep = new Departamento();
            dg_listaDptos.ItemsSource = dep.ListarTodo();
        }

        private void recargarCiudades(){
            Ciudad city = new Ciudad();
            cb_ciudad.ItemsSource = city.listarTodo();
        }

        private void dg_listaDptos_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            // Se realiza esta comprobacion ya que el evento SelectionChanged se activa tambien cuando se actualiza la tabla, esto evita sus errores.
            if(actualizando == false) {

                if (btn_gestInventario.IsEnabled == false)
                {
                    Alternar_habil_btns(true);
                }

                seleccionado = (Departamento)e.AddedItems[0];

                // Si el departamento está disponible, solo se podrá cambiar a no disponible, y vice versa. 
                if (seleccionado.DISPONIBLE == "1")
                {
                    btn_noDisp.IsEnabled = true;
                    btn_disp.IsEnabled = false;
                }
                else
                {
                    btn_noDisp.IsEnabled = false;
                    btn_disp.IsEnabled = true;
                }

                tb_nombre.Text = seleccionado.NOMBRE;
                tb_direccion.Text = seleccionado.DIRECCION;
                cb_ciudad.Text = seleccionado.Negocio_Ciudad.NOMBRE;
                tb_numDpto.Text = seleccionado.NRO_DPTO;
                tb_superficie.Text = seleccionado.SUPERFICIE_DPTO;
                tb_precio.Text = seleccionado.PRECIO_DPTO.ToString();

                tb_estadoActual.Text = seleccionado.CONDICION;

                Inventario inv = new Inventario();

                dg_inventario.ItemsSource = inv.ListarInventarioDeDpto(seleccionado.ID_DPTO);
            }
        }

        private void btn_gestMantenciones_Click(object sender, RoutedEventArgs e)
        {
            Dpto_mantenciones win_mantencion = new Dpto_mantenciones();
            win_mantencion.ShowDialog();
        }

        private void btn_gestImagenes_Click(object sender, RoutedEventArgs e)
        {
            Dpto_imagenes win_imagenes = new Dpto_imagenes();
            win_imagenes.ShowDialog();
        }

        private void btn_gestInventario_Click(object sender, RoutedEventArgs e)
        {
            Dpto_inventario win_inventario = new Dpto_inventario(seleccionado);
            win_inventario.ShowDialog(); 

        }
        
        private void btn_disp_Click(object sender, RoutedEventArgs e)
        {
            // Dialogo de confirmacion antes de cambiar disponibilidad.
            MessageBoxResult dialogResult = MessageBox.Show("¿Estas seguro que quieres marcar el departamento como Disponible?", "Confirmación", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);

            if (dialogResult == MessageBoxResult.Yes)
            {
                actualizando = true;

                seleccionado.alternarDisponibilidad(seleccionado.ID_DPTO, "1");

                limpiarCampos();

                actualizando = false;
            }

        }

        private void btn_noDisp_Click(object sender, RoutedEventArgs e)
        {
            // Dialogo de confirmacion antes de cambiar disponibilidad.
            MessageBoxResult dialogResult = MessageBox.Show("¿Estas seguro que quieres marcar el departamento como No Disponible?", "Confirmación", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);

            if (dialogResult == MessageBoxResult.Yes)
            {
                actualizando = true;

                seleccionado.alternarDisponibilidad(seleccionado.ID_DPTO, "0");
                
                limpiarCampos();

                actualizando = false;
            }

            
        }
    }
}
