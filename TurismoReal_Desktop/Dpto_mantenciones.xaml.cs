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
    /// Interaction logic for Dpto_mantenciones.xaml
    /// </summary>
    public partial class Dpto_mantenciones : MetroWindow
    {
        public Departamento selectedDpto;
        public Mantencion selectedMantencion;
        private bool actualizando;

        public string costoManten;

        public Dpto_mantenciones()
        {
            InitializeComponent();
        }

        public Dpto_mantenciones (Departamento dpto)
        {
            InitializeComponent();
            selectedDpto = dpto;

            lb_selectedDpto.Content = ArmarLabel();
            
            Recargar_listado_mantenciones();
            Alternar_habil_btns(false);
        }

        private void Alternar_habil_btns(bool estado)
        {
            btn_actualizar.IsEnabled = estado;
            btn_eliminar.IsEnabled = estado;
        }

        private void Recargar_listado_mantenciones()
        {
            Mantencion mant = new Mantencion();

            List<Mantencion> listadoMantenciones = mant.ListarTodoDeDpto(selectedDpto.ID_DPTO);

            // Si no hay mantenciones para el departamento, mostrar un primer registro vacio
            if(listadoMantenciones.Count < 1)
            {
                dg_mantenciones.SelectedItem = new List<Mantencion>();
            }
            else
            {
                dg_mantenciones.ItemsSource = listadoMantenciones;
            }

        }

        private string ArmarLabel()
        {
            string resultado = String.Concat("Departamento actualmente seleccionado: ", selectedDpto.NOMBRE, ", ", selectedDpto.DIRECCION, ", ", "n°", selectedDpto.NRO_DPTO, ", ", selectedDpto.Negocio_Ciudad.NOMBRE, ".");

            return resultado;
        }

        private void btn_retroceder_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private async void btn_guardar_Click(object sender, RoutedEventArgs e)
        {
            // No deja proceder si faltan datos. 
            if (String.IsNullOrWhiteSpace(tb_descripcion.Text.Trim()) || String.IsNullOrWhiteSpace(tb_costo.Text.Trim()) || dp_fecInicio.SelectedDate == null || dp_fecFin.SelectedDate == null)
            {
                await this.ShowMessageAsync("Registro fallido", "Por favor, complete todos los campos e intente nuevamente.");
                return;
            }

            // Intenta extraer y convertir valor, y valida que valor solo contenga numeros! 
            if (Decimal.TryParse(tb_costo.Text.Trim(), out decimal valor) == false)
            {
                await this.ShowMessageAsync("Datos incorrectos", "Por favor, ingrese solo números en el costo del mantenimiento.");
                return;
            }

            // Extrae datos de los campos.
            string descripcion = tb_descripcion.Text.Trim();
            DateTime? fec_inicio = dp_fecInicio.SelectedDate;
            DateTime? fec_fin = dp_fecFin.SelectedDate;


            // No deja proceder si la fecha de fin es mayor a la fecha de inicio. 
            if (dp_fecInicio.SelectedDate > dp_fecFin.SelectedDate)
            {
                await this.ShowMessageAsync("Fechas no coherentes", "Por favor, corrobore que haya seleccionado las fechas correctas.");
                return;
            }


            // Iterar por listado de mantenciones actual para verificar que no se registren mantenciones ya existentes.
            foreach (Mantencion manten in dg_mantenciones.Items)
            {
                if (descripcion.Replace(" ", "").ToUpper() == manten.DESCRIPCION.Replace(" ", "").ToUpper() && 
                    fec_inicio == manten.FECHA_INICIO && 
                    fec_fin == manten.FECHA_FIN)
                {
                    await this.ShowMessageAsync("Elemento duplicado", "La mantención ya se encuentra en los registros.");
                    return;
                }
            }

            // Crear nuevo objeto de mantencion para llamar el metodo de registrar una nueva. 
            Mantencion mantencion = new Mantencion();

            Boolean resultado = mantencion.CrearMantencion(selectedDpto.ID_DPTO, fec_inicio, fec_fin, descripcion, valor);

            if (resultado)
            {
                actualizando = true;

                limpiarCampos();
                actualizando = false;

                await this.ShowMessageAsync("Registro exitoso", "La mantención especificada se ha registrado exitosamente.");
            }
            else
            {
                await this.ShowMessageAsync("El registro falló", "Algo salió mal, por favor intentelo nuevamente.");
            }

        }

        private async void btn_actualizar_Click(object sender, RoutedEventArgs e)
        {
            // Si por algun motivo el boton de actualizar esta activado sin tener seleccionado una mantencion, que no deberia, retornar de inmediato en vez de crashear.
            if (selectedMantencion == null)
            {
                return;
            }

            // No deja proceder si faltan datos. 
            if (String.IsNullOrWhiteSpace(tb_descripcion.Text.Trim()) || String.IsNullOrWhiteSpace(tb_costo.Text.Trim()) || dp_fecInicio.SelectedDate == null || dp_fecFin.SelectedDate == null)
            {
                await this.ShowMessageAsync("Actualización fallida", "Por favor, complete todos los campos e intente nuevamente.");
                return;
            }

            // Intenta extraer y convertir valor, y valida que valor solo contenga numeros! 
            if (Decimal.TryParse(tb_costo.Text.Trim(), out decimal valor) == false)
            {
                await this.ShowMessageAsync("Datos incorrectos", "Por favor, ingrese solo números en el costo del mantenimiento.");
                return;
            }

            // Extrae datos de los campos.
            string descripcion = tb_descripcion.Text.Trim();
            DateTime fec_inicio = (DateTime)dp_fecInicio.SelectedDate;
            DateTime fec_fin = (DateTime)dp_fecFin.SelectedDate;


            // No deja proceder si la fecha de fin es mayor a la fecha de inicio. 
            if (dp_fecInicio.SelectedDate > dp_fecFin.SelectedDate)
            {
                await this.ShowMessageAsync("Fecha no coherentes", "Por favor, corrobore que haya seleccionado las fechas correctas.");
                return;
            }

            // No actualizar si todos los datos estan iguales. Si cambia al menos uno, actualiza. 
            if (descripcion.Replace(" ", "").ToUpper() == selectedMantencion.DESCRIPCION.Replace(" ", "").ToUpper() &&
                fec_inicio == selectedMantencion.FECHA_INICIO &&
                fec_fin == selectedMantencion.FECHA_FIN &&
                valor == selectedMantencion.COSTO)
            {
                await this.ShowMessageAsync("Mismos datos", "Aún no se han modificado datos del registro original.");
                return;
            }


            Mantencion mant = new Mantencion();
            Boolean resultado = mant.UpdateMantencion(selectedMantencion.ID_MANTENCION, selectedMantencion.ID_DPTO, fec_inicio, fec_fin, descripcion, valor);

            if (resultado)
            {
                actualizando = true;

                limpiarCampos();

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
            tb_descripcion.Clear();
            dp_fecInicio.SelectedDate = null;
            dp_fecFin.SelectedDate = null;
            tb_costo.Clear();


            selectedMantencion = null;

            Alternar_habil_btns(false);

            Recargar_listado_mantenciones();
        }

        private void dg_mantenciones_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (actualizando == false)
            {
                selectedMantencion = dg_mantenciones.SelectedItem as Mantencion;

                //Recien aqui habilita boton de actualizar
                Alternar_habil_btns(true);

                dp_fecInicio.SelectedDate = selectedMantencion.FECHA_INICIO;
                dp_fecFin.SelectedDate = selectedMantencion.FECHA_FIN;
                tb_descripcion.Text = selectedMantencion.DESCRIPCION;
                tb_costo.Text = selectedMantencion.COSTO.ToString();
            }
        }

        private async void btn_eliminar_Click(object sender, RoutedEventArgs e)
        {
            if (selectedMantencion == null)
            {
                return;
            }

            MessageBoxResult res = MessageBox.Show("¿Estás seguro que quieres eliminar PERMANENTEMENTE la mantención seleccionada?", "Confirmación", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);

            if (res == MessageBoxResult.Yes)
            {
                // Borrar mantencion
                Boolean resultado = selectedMantencion.DeleteMantencion(selectedMantencion.ID_MANTENCION);

                if (resultado)
                {
                    btn_eliminar.IsEnabled = false;
                    selectedMantencion = null;
                    limpiarCampos();
                    await this.ShowMessageAsync("Mantenimiento eliminado", "El mantenimiento seleccionado ha sido eliminado exitosamente.");
                }
                else
                {
                    await this.ShowMessageAsync("Eliminación fallida", "Algo salió mal, verifique conexión a la base de datos e intente nuevamente.");
                }
            }

            



        }
    }
}
