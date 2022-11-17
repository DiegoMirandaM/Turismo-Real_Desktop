﻿using System;
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

            DatosParaReporte datosRep = new DatosParaReporte(fecha_inicio, fecha_fin);

            DataContext = datosRep;
            
        }

        




    }
}