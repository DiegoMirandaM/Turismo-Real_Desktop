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
            dpto.ListarTodo();
        }

        private string armarLabel()
        {
            string resultado = String.Concat("Servicio actualmente seleccionado: ", selectedService.DESCRIPCION, "(", selectedService.COSTO_ACTUAL, ")");

            return resultado;
        }


    }
}
