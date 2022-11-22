using System;
using System.Collections.Generic;
using System.Linq;

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
        public List<string> categoriaGastos { get; set; }
        public List<decimal> gastosPorCategoria { get; set; }
        public IEnumerable<ISeries> serie_IngresosCategoria { get; set; }
        public IEnumerable<ISeries> serie_EgresosCategoria { get; set; }
        // De aqui para arriba estaria funcionando y usandose los datos: ---^


        public string[] categoriaIngresos { get; set; }
        public decimal[] ingresosPorCategoria { get; set; }
        public string[] ciudades { get; set; }
        public decimal[] ingresosPorCiudad { get; set; }

        public string[] deptos { get; set; }
        public decimal[] ingresosPorDpto { get; set; }

        public List<decimal> ingresosCat { get; set; }
        public List<string> categoriasIng { get; set; }


        public List<IngresosDeDpto> top5Deptos { get; set; }
        public ISeries[] serie_Top5Dptos { get; set; }

        public ISeries[] SeriesCollection { get; set; }




        public Axis XAxes { get; set; }
        public Axis YAxes { get; set; }



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
                gananciasTotales_num = ingresosTotales_num - egresosTotales_num;

                ingresosTotales = ingresosTotales_num.ToString("C", new System.Globalization.CultureInfo("es-CL"));
                egresosTotales = egresosTotales_num.ToString("C", new System.Globalization.CultureInfo("es-CL"));
                gananciasTotales = gananciasTotales_num.ToString("C", new System.Globalization.CultureInfo("es-CL"));

                // Ingresar las categorias de ingresos y sus valores para mostrarlo en grafico de torta:
                categoriasIng = new List<string>
                {
                    "Arriendos",
                    "Multas",
                    "Servicios Extra",
                    "Reservas canceladas",
                    "Traslados"
                };

                ingresosCat = new List<decimal>
                {
                    aporte_Arriendo,
                    aporte_Multa,
                    aporte_Servicios,
                    aporte_Reservas,
                    aporte_traslados
                };

                // Ingresar las categorias de gastos para hacer lo mismo que arriba:
                categoriaGastos = new List<string>
                {
                    "Mantenciones",
                    "Compra inventario"
                };

                gastosPorCategoria = new List<decimal>
                {
                    costo_mantenciones,
                    costo_inventario
                };

                // Esto consigue el top 5 de departamentos por ingresos totales en el periodo:
                top5Deptos = new IngresosDeDpto().ListarTodoEnFechas(fechaInicio, fechaFin);


                // ERROR: Falta ciudades e ingresos por ciudades, y dptos e ingresos por dpto






                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public void recargarGraficos()
        {
            // Genera serie de valores para el grafico ingresos por categoria. Se intento filtrar por porcentaje
            // para mostrar solo los mayores a 1%, pero no funciono. Solo se salto los pasos de especificarle
            // nombre y detalles, pero aparece detodas formas en el piechart.
            int ixCatIngreso = -1;
            serie_IngresosCategoria = ingresosCat.AsLiveChartsPieSeries((value, series) =>
            {
                ixCatIngreso++;
                if (value > 0)
                {
                    series.Name = categoriasIng[ixCatIngreso];
                    series.DataLabelsPaint = new SolidColorPaint(new SKColor(30, 30, 30));
                    series.DataLabelsPosition = LiveChartsCore.Measure.PolarLabelsPosition.Outer;
                    series.DataLabelsPadding = new LiveChartsCore.Drawing.Padding(10, 0);
                    series.TooltipLabelFormatter =

                    series.DataLabelsFormatter = p => $"({p.StackedValue.Share:P1})";
                }
            });

            // Genera valores para gastos por categoria: 
            int ixGastosCategoria = 0;
            serie_EgresosCategoria = gastosPorCategoria.AsLiveChartsPieSeries((value, series) =>
            {
                if (value > 0)
                {
                    series.Name = categoriaGastos[ixGastosCategoria];
                    ixGastosCategoria++;
                    series.DataLabelsPaint = new SolidColorPaint(new SKColor(30, 30, 30));
                    series.DataLabelsPosition = LiveChartsCore.Measure.PolarLabelsPosition.Outer;
                    series.DataLabelsPadding = new LiveChartsCore.Drawing.Padding(10, 0);

                    series.DataLabelsFormatter = p => $"({p.StackedValue.Share:P1})";
                }
            });

            // Funciona, acomoda las cosas de manera algo mejorable, pero funciona al menos:
            //serie_Top5Dptos = new ISeries[]
            //{
            //    new ColumnSeries<decimal>
            //    {
            //        Name = top5Deptos[0].nombreDpto,
            //        Values = new []{top5Deptos[0].totalIngresos}
            //        //Values = new []{ 2, 5, 4, 2, 6 }
            //    },
            //    new ColumnSeries<decimal>
            //    {
            //        Name = top5Deptos[1].nombreDpto,
            //        Values = new []{top5Deptos[1].totalIngresos}
            //        //Values = new []{ 2, 5, 4, 2, 6 }
            //    },
            //    new ColumnSeries<decimal>
            //    {
            //        Name = top5Deptos[2].nombreDpto,
            //        Values = new []{top5Deptos[2].totalIngresos}
            //        //Values = new []{ 2, 5, 4, 2, 6 }
            //    },
            //    new ColumnSeries<decimal>
            //    {
            //        Name = top5Deptos[3].nombreDpto,
            //        Values = new []{top5Deptos[3].totalIngresos}
            //        //Values = new []{ 2, 5, 4, 2, 6 }
            //    },
            //    new ColumnSeries<decimal>
            //    {
            //        Name = top5Deptos[4].nombreDpto,
            //        Values = new []{top5Deptos[4].totalIngresos}
            //        //Values = new []{ 2, 5, 4, 2, 6 }
            //    }
            //};



            // ERROR: Distancia las barras, si, pero no puede asignar mas de un nombre a los elementos de la serie, imposibilitando diferenciar los deptos:
            serie_Top5Dptos = new ISeries[]
            {
                new ColumnSeries<decimal>
                {
                    //Name = new [] {top5Deptos[0].nombreDpto, top5Deptos[1].nombreDpto, },
                    Values = new []{top5Deptos[0].totalIngresos, top5Deptos[1].totalIngresos, top5Deptos[2].totalIngresos, top5Deptos[3].totalIngresos, top5Deptos[4].totalIngresos, }
                    //Values = new []{ 2, 5, 4, 2, 6 }
                },
                
            };

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

