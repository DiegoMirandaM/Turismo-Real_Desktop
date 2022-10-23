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

        public ServiciosExtra_Asociar()
        {
            InitializeComponent();
        }

        public ServiciosExtra_Asociar (Servicio_extra servicio)
        {
            InitializeComponent();
            selectedService = servicio;
            lb_selectedServicio.Content = armarLabel();
            recargar_listado_dptos();
        }

        private void recargar_listado_dptos()
        {
            Departamento dpto = new Departamento();
            Disponibilidad_servicio disp = new Disponibilidad_servicio();

            var listadoDptos = dpto.ListarTodo();
            var listadoDisps = disp.ListarTodoDeServicio(selectedService.ID_SERVICIO);

            /* En esta parte, tengo que iterar por cada depto buscando su correlacion en el listado de disponibilidad. 
             * Si no existe correlacion, ambos checkbox son false, y disp_createOrUpdate es Create.
             * Si existe, "Ofrecido aqui" es verdadero, disponible toma el valor desde Disponibilidad_servicio, y disp_createOrUpdate es Update. 
             * 
             * Al momento de desmarcar Ofrecido, se debiese desmarcar disponible, ya que no ofrecido = registro de disponibilidad asociado es eliminado. 
             * 
             * Copiar listado resultante de Dptos para poblar la tabla, de manera que se pueda comparar la version original del listado con la modificada,
             * realizando updates y deletes solo donde se modificaron las cosas.
             * 
             * Entonces, al presionar guardar cambios, comparar cada registro del datagrid con su version original, si se desmarco Ofrecido, delete from.
             * Si Ofrecido sigue marcado pero se altero disponible, update from.
             * 
             * Terminando ese proceso, volver a ejecutar este metodo para refrescar datagrid. 
             */



        }

        private string armarLabel()
        {
            string resultado = String.Concat("Servicio actualmente seleccionado: ", selectedService.DESCRIPCION, "(", selectedService.COSTO_ACTUAL, ")");

            return resultado;
        }


    }
}
