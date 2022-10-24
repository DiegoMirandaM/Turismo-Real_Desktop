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
    /// Interaction logic for ServiciosExtra_Asociar.xaml
    /// </summary>
    public partial class ServiciosExtra_Asociar : MetroWindow
    {
        public Servicio_extra selectedService;

        List<Departamento> listDptosOriginal;
        List<Departamento> listDptosModificable;

        public ServiciosExtra_Asociar()
        {
            InitializeComponent();
        }

        public ServiciosExtra_Asociar (Servicio_extra servicio)
        {
            InitializeComponent();
            selectedService = servicio;
            lb_selectedServicio.Content = ArmarLabel();
            RecargarListadoDptos();
        }

        private async void RecargarListadoDptos()
        {
            Departamento dpto = new Departamento();
            Disponibilidad_servicio disp = new Disponibilidad_servicio();

            listDptosOriginal = dpto.ListarTodo();
            List<Disponibilidad_servicio> listDisponibilidades = disp.ListarTodoDeServicio(selectedService.ID_SERVICIO);

            Boolean listDptos_Vacio = listDptosOriginal.Count == 0;
            Boolean listDisponibilidad_Vacio = listDisponibilidades.Count == 0;

            /* En esta parte, iterar por depto buscando correlacion en listado de disponibilidad. 
             * Si no existe correlacion, ambos checkbox son false, y disp_createOrUpdate es Create.
             * Si existe, "Ofrecido aqui" es verdadero, disponible toma el valor desde Disponibilidad_servicio, y disp_createOrUpdate es Update. 
             * 
             * Copiar listado resultante de Dptos para poblar la tabla, de manera que se pueda comparar la version original del listado con la modificada,
             * realizando updates y deletes solo donde se modificaron las cosas.
             * 
             * Entonces, al presionar guardar cambios, comparar cada registro del datagrid con su version original, si se desmarco Ofrecido, delete from.
             * Si Ofrecido sigue marcado pero se altero disponible, update from.
             * 
             * Terminando ese proceso, volver a ejecutar este metodo para refrescar datagrid. 
             * 
             * Si se desmarca Ofrecido, Disponible también, esto porque no ofrecido = registro de disponibilidad asociado es eliminado. 
             * Si Ofrecido está desmarcado pero se marca Disponible, Ofrecido tambien debe marcarse. 
             */


            // Si no hay departamentos en el listado, detener el proceso aqui. 
            if (listDptos_Vacio)
            {
                await this.ShowMessageAsync("No se pudo recuperar el listado", "Por favor, asegurese de tener departamentos ya registrados en el sistema, e intentelo nuevamente.");
                return;
            }

            // Si hay al menos un depto, pero no hay registros de disponibilidad, el servicio no se ofrece en ninguno, y tampoco está disponible. 
            if (listDisponibilidad_Vacio)
            {
                foreach (Departamento depto in listDptosOriginal)
                {
                    dpto.disp_createOrUpdate = "CREATE";
                    dpto.disp_asociado = false;
                    dpto.disp_habilitado = false;
                }

                listDptosModificable = listDptosOriginal;

                dg_relacionDptos.ItemsSource = listDptosModificable;

                return;
            }

            // Iterar por cada depto del listado de departamentos. 
            foreach (Departamento depto in listDptosOriginal)
            {
                

                // Iterar, considerando cada depto, por cada registro de disponibilidad, buscando relacion. 
                foreach (Disponibilidad_servicio dispServ in listDisponibilidades)
                {
                    // Si coincide el id de depto en el registro, es porque se ofrece el servicio en ese depto actualmente.
                    if (dispServ.ID_DPTO == depto.ID_DPTO)
                    {
                        depto.disp_createOrUpdate = "UPDATE";
                        depto.disp_asociado = true;
                        depto.disp_habilitado = dispServ.ACTUALMENTE_DISPONIBLE == "0" ? false: true;

                    }
                }

            }



        }

        private string ArmarLabel()
        {
            string resultado = String.Concat("Servicio actualmente seleccionado: ", selectedService.DESCRIPCION, " ($", selectedService.COSTO_ACTUAL, ")");

            return resultado;
        }

        private void btn_retroceder_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

    }
}
