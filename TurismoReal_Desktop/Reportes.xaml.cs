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

using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;


namespace TurismoReal_Desktop
{
    /// <summary>
    /// Interaction logic for Reportes.xaml
    /// </summary>
    public partial class Reportes : MetroWindow
    {
        private DatosParaReporte datosReportes;

        public Reportes()
        {

            DateTime inicioAnio = DateTime.Parse("01/01/" + DateTime.Now.Year.ToString());
            DateTime fechaActual = DateTime.Now;

            datosReportes = new DatosParaReporte();

            DataContext = datosReportes;

            InitializeComponent();

            dp_fecInicio.SelectedDate = inicioAnio;
            dp_fecFin.SelectedDate = fechaActual;

            

        }

        

        private void btn_retroceder_Click(object sender, RoutedEventArgs e)
        {
            MainMenu win_mainMenu = new MainMenu();
            win_mainMenu.Show();
            this.Close();
        }

        private void btn_actualizar_Click(object sender, RoutedEventArgs e)
        {
            return;
        }

        //DateTime.Parse("01/01/")


        //var resultado = DatosParaReporte.ConsultarDatosEnRango();




    }
}
