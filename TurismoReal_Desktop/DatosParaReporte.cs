using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;
using TurismoReal_Desktop_Controlador;

namespace TurismoReal_Desktop
{
    public class DatosParaReporte
    {
        public List<Arriendo> arriendosEnRango { get; set; }
        public List<Multa> multasEnRango { get; set; }
        //public List<Servicio_contratado> serviciosContratadosEnRango { get; set; }
        public List<Solicitud_transporte> transportesEnRango { get; set; }
        public List<Reserva> reservasEnRango { get; set; }
        public List<Mantencion> mantencionesEnRango { get; set; }
        public List<Inventario> inventariosEnRango { get; set; }

        public decimal cantidadArriendos { get; set; }
        public string ingresosTotales { get; set; }
        public decimal ingresosTotales_num { get; set; }
        public string egresosTotales { get; set; }
        public decimal egresosTotales_num { get; set; }
        public string gananciasTotales { get; set; }
        public decimal gananciasTotales_num { get; set; }


        public string[] categoriaIngresos { get; set; }
        public decimal[] ingresosPorCategoria { get; set; }

        public List<string> categoriaGastos { get; set; }
        public List<decimal> gastosPorCategoria { get; set; }

        public string[] ciudades { get; set; }
        public decimal[] ingresosPorCiudad { get; set; }

        public string[] deptos { get; set; }
        public decimal[] ingresosPorDpto { get; set; }

        List<decimal> ingresosCat;
        List<string> categoriasIng;

        List<Arriendo> top5Dptos;
        List<string> top5Dptos_name;
        List<decimal> top5Dptos_num;

        public IEnumerable<ISeries> serie_IngresosCategoria { get; set; }

        public IEnumerable<ISeries> serie_EgresosCategoria { get; set; }

        public IEnumerable<ISeries> serie_top5Dptos { get; set; }

        public Axis[] top5Dptos_XAxes { get; set; } 



        public DatosParaReporte()
        {
            DateTime inicioAnio = DateTime.Parse("01/01/" + DateTime.Now.Year.ToString());
            DateTime fechaActual = DateTime.Now;

            ConsultarDatosEnRango(inicioAnio, fechaActual);
            recargarGraficos();
        }
        public DatosParaReporte(DateTime inicio, DateTime fin)
        {
            ConsultarDatosEnRango(inicio, fin);
            recargarGraficos();
        }

        public Boolean ConsultarDatosEnRango(DateTime fechaInicio, DateTime fechaFin)
        {
            try
            {
                arriendosEnRango = new Arriendo().ListarTodoEnFechas(fechaInicio, fechaFin, out decimal aporte_Arriendo, out decimal aporte_Servicios);
                multasEnRango = new Multa().ListarTodoEnFechas(fechaInicio, fechaFin, out decimal aporte_Multa);
                transportesEnRango = new Solicitud_transporte().ListarTodoEnFechas(fechaInicio, fechaFin, out decimal aporte_traslados);

                // Considera como ingreso el costo de reservas canceladas que no son reembolsadas.
                reservasEnRango = new Reserva().ListarTodoEnFechas(fechaInicio, fechaFin, out decimal aporte_Reservas);

                mantencionesEnRango = new Mantencion().ListarTodoEnFechas(fechaInicio, fechaFin, out decimal costo_mantenciones);
                inventariosEnRango = new Inventario().ListarTodoEnFechas(fechaInicio, fechaFin, out decimal costo_inventario);

                // Cuenta para mostrar cantidad de arriendos en el periodo:
                cantidadArriendos = arriendosEnRango.Count;

                // Calcula ingresos totales, egresos y diferencia como ganancias del periodo. Guarda resultado tanto en decimal como en texto formateado a dinero:

                ingresosTotales_num = aporte_Arriendo + aporte_Servicios + aporte_Multa + aporte_traslados + aporte_Reservas;
                egresosTotales_num = costo_mantenciones + costo_inventario;
                gananciasTotales_num = ingresosTotales_num -egresosTotales_num;

                ingresosTotales = ingresosTotales_num.ToString("C", new System.Globalization.CultureInfo("es-CL"));
                egresosTotales = egresosTotales_num.ToString("C", new System.Globalization.CultureInfo("es-CL"));
                gananciasTotales = gananciasTotales_num.ToString("C", new System.Globalization.CultureInfo("es-CL"));

                // Ingresar las categorias de ingresos y sus valores para mostrarlo en grafico de torta:
                categoriasIng = new List<string>();
                categoriasIng.Add("Arriendos");
                categoriasIng.Add("Multas");
                categoriasIng.Add("Servicios Extra");
                categoriasIng.Add("Reservas canceladas");
                categoriasIng.Add("Traslados");

                ingresosCat = new List<decimal>();
                ingresosCat.Add(aporte_Arriendo);
                ingresosCat.Add(aporte_Multa);
                ingresosCat.Add(aporte_Servicios);
                ingresosCat.Add(aporte_Reservas);
                ingresosCat.Add(aporte_traslados);

                // Ingresar las categorias de gastos para hacer lo mismo que arriba:
                categoriaGastos = new List<string>();
                categoriaGastos.Add("Mantenciones");
                categoriaGastos.Add("Compra inventario");

                gastosPorCategoria = new List<decimal>();
                gastosPorCategoria.Add(costo_mantenciones);
                gastosPorCategoria.Add(costo_inventario);

                // Falta ciudades e ingresos por ciudades, y dptos e ingresos por dpto
                top5Dptos = new List<Arriendo>();
                top5Dptos_name = new List<string>();
                top5Dptos_num = new List<decimal>();

                top5Dptos = arriendosEnRango.OrderByDescending(arr => arr.TOTAL_ARRIENDO + arr.TOTAL_SERVICIOS).Take(5).ToList();

                foreach (Arriendo arr in top5Dptos)
                {
                    top5Dptos_name.Add(arr.DEPARTAMENTO.NOMBRE);
                    top5Dptos_num.Add(arr.TOTAL_ARRIENDO + arr.TOTAL_SERVICIOS);
                }



                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public void recargarGraficos()
        {
            int ixCatIngreso = 0;
            serie_IngresosCategoria = ingresosCat.AsLiveChartsPieSeries((value, series) =>
            {
                if(value > 0)
                {
                    // Solo poner el valor si es mayor a 1%
                    double porcentaje = (double)((value * 100) / ingresosTotales_num);
                    if (porcentaje >= 1)
                    {
                        series.Name = categoriasIng[ixCatIngreso];
                        ixCatIngreso++;
                        series.DataLabelsPaint = new SolidColorPaint(new SKColor(30, 30, 30));
                        series.DataLabelsPosition = LiveChartsCore.Measure.PolarLabelsPosition.Outer;
                        series.DataLabelsPadding = new LiveChartsCore.Drawing.Padding(10, 0);

                        series.DataLabelsFormatter = p => $"({p.StackedValue.Share:P1})";
                    }
                }
            });

            int ixGastosCategoria = 0;
            serie_EgresosCategoria = gastosPorCategoria.AsLiveChartsPieSeries((value, series) =>
            {
                if (value > 0)
                {
                    // Solo poner el valor si es mayor a 1%
                    double porcentaje = (double)((value * 100) / egresosTotales_num);
                    if (porcentaje >= 1)
                    {
                        series.Name = categoriaGastos[ixGastosCategoria];
                        ixGastosCategoria++;
                        series.DataLabelsPaint = new SolidColorPaint(new SKColor(30, 30, 30));
                        series.DataLabelsPosition = LiveChartsCore.Measure.PolarLabelsPosition.Outer;
                        series.DataLabelsPadding = new LiveChartsCore.Drawing.Padding(10, 0);

                        series.DataLabelsFormatter = p => $"({p.StackedValue.Share:P1})";
                    }
                }
            });

            int ix_top5Dptos = 0;



            /*
            serie_top5Dptos = top5Dptos_num.AsLiveChartsPieSeries((value, series) =>
            {
                if (value > 0)
                {
                    // Solo poner el valor si es mayor a 1%
                    double porcentaje = (double)((value * 100) / ingresosTotales_num);
                    if (porcentaje >= 1)
                    {
                        series.Name = top5Dptos_name[ix_top5Dptos];
                        ix_top5Dptos++;
                        series.DataLabelsPaint = new SolidColorPaint(new SKColor(30, 30, 30));
                        series.DataLabelsPosition = LiveChartsCore.Measure.PolarLabelsPosition.Outer;
                        series.DataLabelsPadding = new LiveChartsCore.Drawing.Padding(10, 0);

                        series.DataLabelsFormatter = p => $"({p.StackedValue.Share:P1})";
                    }
                }
            });
            */
            //top5Dptos_XAxes =  //new Axis(); // { Labels = new string[] { "Category 1", "Category 2", "Category 3" } };




            /*
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
           */

        }


        public ISeries[] Series { get; set; } =
        {
            new ColumnSeries<double>
            {
                Name = "Mary",
                Values = new double[] { 2, 5, 4 }
            },
            new ColumnSeries<double>
            {
                Name = "Ana",
                Values = new double[] { 3, 1, 6 }
            }
        };

        
    }
}
