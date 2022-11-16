using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using TurismoReal_Desktop_Controlador;

namespace TurismoReal_Desktop
{
    public class DatosParaReporte
    {
        public List<Arriendo> arriendosEnRango { get; set; }
        public List<Multa> multasEnRango { get; set; }
        public List<Servicio_contratado> serviciosContratadosEnRango { get; set; }
        public List<Solicitud_transporte> transportesEnRango { get; set; }
        public List<Mantencion> mantencionesEnRango { get; set; }
        public List<Inventario> inventariosEnRango { get; set; }

        public decimal cantidadArriendos { get; set; }
        public decimal ingresosTotales { get; set; }
        public decimal gastosTotales { get; set; }

        public string[] categoriaIngresos { get; set; }
        public decimal[] ingresosPorCategoria { get; set; }

        public string[] categoriaGastos { get; set; }
        public decimal[] gastosPorCategoria { get; set; }

        public string[] ciudades { get; set; }
        public decimal[] ingresosPorCiudad { get; set; }

        public string[] deptos { get; set; }
        public decimal[] ingresosPorDpto { get; set; }

        public DatosParaReporte() {
            DateTime inicioAnio = DateTime.Parse("01/01/" + DateTime.Now.Year.ToString());
            DateTime fechaActual = DateTime.Now;

            ConsultarDatosEnRango(inicioAnio, fechaActual);
        }
        public DatosParaReporte(DateTime inicio, DateTime fin)
        {
            ConsultarDatosEnRango(inicio, fin);
        }

        // Ejemplo de formato donde toma datos y los muestra en el grafico de puntos. 
        public ISeries[] Series { get; set; }
            = new ISeries[]
            {
                new LineSeries<double>
                {
                    Values = new double[] { 2, 1, 3, 5, 3, 4, 6 },
                    Fill = null
                }
            };
        
        public Boolean ConsultarDatosEnRango(DateTime fechaInicio, DateTime fechaFin)
        {
            try
            {
                arriendosEnRango = new Arriendo().ListarTodoEnFechas(fechaInicio, fechaFin);
                multasEnRango = new Multa().ListarTodoEnFechas(fechaInicio, fechaFin);
                serviciosContratadosEnRango = new Servicio_contratado().ListarTodoEnFechas(fechaInicio, fechaFin);
                transportesEnRango = new Solicitud_transporte().ListarTodoEnFechas(fechaInicio, fechaFin);
                mantencionesEnRango = new Mantencion().ListarTodoEnFechas(fechaInicio, fechaFin);
                inventariosEnRango = new Inventario().ListarTodoEnFechas(fechaInicio, fechaFin);

                cantidadArriendos = arriendosEnRango.Count;


                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        

    }
}
