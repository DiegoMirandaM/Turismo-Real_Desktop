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
using LiveChartsCore.VisualElements;

namespace TurismoReal_Desktop
{
    /// <summary>
    /// Interaction logic for Reportes.xaml
    /// </summary>
    public partial class Reportes : MetroWindow
    {
        public Reportes()
        {
            DateTime inicioAnio = DateTime.Parse("01/01/" + DateTime.Now.Year.ToString());
            DateTime fechaActual = DateTime.Now;

            InitializeComponent();

            dp_fecInicio.SelectedDate = inicioAnio;
            dp_fecFin.SelectedDate = fechaActual;

            lb_top5Dptos_noData.Opacity = 0;
            lb_top5Cities_noData.Opacity = 0;
            lb_egresosCat_noData.Opacity = 0;
            lb_ingresosCat_noData.Opacity = 0;
        }

        private void btn_retroceder_Click(object sender, RoutedEventArgs e)
        {
            MainMenu win_mainMenu = new MainMenu();
            win_mainMenu.Show();
            this.Close();
        }

        private async void btn_actualizar_Click(object sender, RoutedEventArgs e)
        {
            if (dp_fecInicio.SelectedDate == null ||
                dp_fecFin.SelectedDate == null)
            {
                await this.ShowMessageAsync("Datos incompletos", "Debes ingresar tanto fecha de inicio como fecha de fin.");
                return;
            }

            DateTime fecha_inicio = (DateTime)dp_fecInicio.SelectedDate;
            DateTime fecha_fin = (DateTime)dp_fecFin.SelectedDate;

            // Si las fechas estan al reves, y la fecha de fin es anterior a la fecha de inicio, invertir las fechas y consultar:
            if (fecha_fin < fecha_inicio)
            {
                DateTime temp = fecha_inicio;
                fecha_inicio = fecha_fin;
                fecha_fin = temp;

                dp_fecInicio.SelectedDate = fecha_inicio;
                dp_fecFin.SelectedDate = fecha_fin;
            }

            // Recargar los datos de los graficos, recuperando con booleanos si es que alguna categoria esta vacia:
            DatosParaReporte datosRep = new DatosParaReporte(fecha_inicio, fecha_fin, out bool top5Dptos_vacio, out bool top5Cities_vacio, out bool catIngresos_vacio, out bool catGastos_vacio);

            lb_top5Dptos_noData.Opacity = top5Dptos_vacio ? 1 : 0;
            lb_top5Cities_noData.Opacity = top5Cities_vacio ? 1 : 0;
            lb_egresosCat_noData.Opacity = catGastos_vacio ? 1 : 0;
            lb_ingresosCat_noData.Opacity = catIngresos_vacio ? 1 : 0;

            DataContext = datosRep;

        }

    }
}
