﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using LiveChartsCore.SkiaSharpView.Painting.Effects;
using SkiaSharp;
using System.Runtime.CompilerServices;
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

        public List<decimal> ingresosCat { get; set; }
        public List<string> categoriasIng { get; set; }

        public List<IngresosDeDpto> top5Deptos { get; set; }
        public ISeries[] serie_Top5Dptos { get; set; }

        public Axis[] XAxes_top5Dptos { get; set; }
            = new Axis[]
            {
                new Axis
                {
                    Name = "Departamentos",
                    NamePaint = new SolidColorPaint(SKColors.Black),

                    LabelsPaint = new SolidColorPaint(SKColors.Blue),
                    TextSize = 0,

                }
            };
        public Axis[] YAxes_top5Dptos { get; set; }
            = new Axis[]
            {
                new Axis
                {
                    Name = "Ingresos",
                    NamePaint = new SolidColorPaint(SKColors.Red),

                    LabelsPaint = new SolidColorPaint(SKColors.Green),
                    TextSize = 12,

                    Labeler = Labelers.Currency,

                    SeparatorsPaint = new SolidColorPaint(SKColors.LightSlateGray)
                    {
                        StrokeThickness = 1,
                        PathEffect = new DashEffect(new float[] { 3, 3 })
                    }
                }
            };

        public List<IngresosDeCiudad> top5Ciudades { get; set; }
        public ISeries[] serie_Top5Ciudades { get; set; }

        public Axis[] XAxes_top5Ciudades { get; set; }
            = new Axis[]
            {
                new Axis
                {
                    Name = "Ciudades",
                    NamePaint = new SolidColorPaint(SKColors.Black),

                    LabelsPaint = new SolidColorPaint(SKColors.Blue),
                    TextSize = 0,
                }
            };
        public Axis[] YAxes_top5Ciudades { get; set; }
            = new Axis[]
            {
                new Axis
                {
                    Name = "Ingresos",
                    NamePaint = new SolidColorPaint(SKColors.Red),

                    LabelsPaint = new SolidColorPaint(SKColors.Green),
                    TextSize = 12,

                    Labeler = Labelers.Currency,

                    SeparatorsPaint = new SolidColorPaint(SKColors.LightSlateGray)
                    {
                        StrokeThickness = 1,
                        PathEffect = new DashEffect(new float[] { 3, 3 })
                    }
                }
            };

        // De aqui para arriba estaria funcionando y usandose los datos: ---^


        public DatosParaReporte()
        {
            DateTime inicioAnio = DateTime.Parse("01/01/" + DateTime.Now.Year.ToString());
            DateTime fechaActual = DateTime.Now;

            ConsultarDatosEnRango(inicioAnio, fechaActual);
            RecargarGraficos();
        }
        public DatosParaReporte(DateTime inicio, DateTime fin)
        {
            ConsultarDatosEnRango(inicio, fin);
            RecargarGraficos();
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

                top5Ciudades = new IngresosDeCiudad().ListarTodoEnFechas(fechaInicio, fechaFin);






                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public void RecargarGraficos()
        {
            // ERROR: LAS CANTIDADES MOSTRADAS EN LOS GRAFICOS DE BARRA NO COINCIDEN CON LAS TARJETAS Y EL GRAFICO DE TORTA DE INGRESOS.

            // Genera serie de valores para el grafico ingresos por categoria: 
            int ixCatIngreso = -1;
            serie_IngresosCategoria = ingresosCat.AsLiveChartsPieSeries((value, series) =>
            {
                if(value > 0)
                {
                    ixCatIngreso++;

                    string catNameIn = categoriasIng[ixCatIngreso];

                    series.Name = catNameIn;
                    series.DataLabelsPaint = new SolidColorPaint(new SKColor(30, 30, 30));
                    series.DataLabelsPosition = LiveChartsCore.Measure.PolarLabelsPosition.Outer;
                    series.DataLabelsPadding = new LiveChartsCore.Drawing.Padding(10, 0);

                    series.DataLabelsFormatter = p => $"({p.StackedValue.Share:P1})";
                    series.TooltipLabelFormatter = p => $" {catNameIn} ({p.PrimaryValue.ToString("C", new System.Globalization.CultureInfo("es-CL"))})";
                }
            });

            // Genera valores para gastos por categoria: 
            int ixGastosCategoria = -1;
            serie_EgresosCategoria = gastosPorCategoria.AsLiveChartsPieSeries((value, series) =>
            {
                if (value > 0)
                {
                    ixGastosCategoria++;

                    string catName = categoriaGastos[ixGastosCategoria];

                    series.Name = catName;
                    series.DataLabelsPaint = new SolidColorPaint(new SKColor(30, 30, 30));
                    series.DataLabelsPosition = LiveChartsCore.Measure.PolarLabelsPosition.Outer;
                    series.DataLabelsPadding = new LiveChartsCore.Drawing.Padding(10, 0);

                    series.DataLabelsFormatter = p => $"({p.StackedValue.Share:P1})";
                    series.TooltipLabelFormatter = p => $" {catName} ({p.PrimaryValue.ToString("C", new System.Globalization.CultureInfo("es-CL"))})";
                }
            });

            // Si no hay deptos en el conjunto, omitir lo de abajo:
            if (top5Deptos.Count == 0) return;

            // Crear un array de columnSeries dependiendo de la cantidad de elementos en top5Deptos:
            ColumnSeries<decimal>[] preSerie_Top5Dptos = new ColumnSeries<decimal>[top5Deptos.Count()];

            // Variable de indice que se utiliza para agregar los elementos columnSeries al conjunto:
            int ixTop5Dpto = -1;
            foreach (IngresosDeDpto depto in top5Deptos)
            {
                ixTop5Dpto++;
                ColumnSeries<decimal> columna = new ColumnSeries<decimal>
                {
                    Name = depto.nombreDpto,
                    Values = new[] { depto.totalIngresos },
                    TooltipLabelFormatter = p => $" {depto.nombreDpto} ({p.PrimaryValue.ToString("C", new System.Globalization.CultureInfo("es-CL"))})"
                };

                preSerie_Top5Dptos[ixTop5Dpto] = columna;
            }

            // Asigna el conjunto de columnSeries al elemento que carga los datos en el grafico. Se intento cargar grafico directamente con preSerie, pero eso no funciona.
            serie_Top5Dptos = preSerie_Top5Dptos;










            // Si no hay ciudades en el conjunto, omitir lo de abajo:
            if (top5Ciudades.Count == 0) return;

            // Crear un array de columnSeries dependiendo de la cantidad de elementos en top5Ciudades:
            ColumnSeries<decimal>[] preSerie_Top5Ciudades = new ColumnSeries<decimal>[top5Ciudades.Count()];

            // Variable de indice que se utiliza para agregar los elementos columnSeries al conjunto:
            int ixTop5Ciudad = -1;
            foreach (IngresosDeCiudad ciudad in top5Ciudades)
            {
                ixTop5Ciudad++;
                ColumnSeries<decimal> columna = new ColumnSeries<decimal>
                {
                    Name = ciudad.nombreCiudad,
                    Values = new[] { ciudad.totalIngresos },
                    TooltipLabelFormatter = p => $" {ciudad.nombreCiudad} ({p.PrimaryValue.ToString("C", new System.Globalization.CultureInfo("es-CL"))})"
                };


                preSerie_Top5Ciudades[ixTop5Ciudad] = columna;
            }

            // Asigna el conjunto de columnSeries al elemento que carga los datos en el grafico. Se intento cargar grafico directamente con preSerie, pero eso no funciona.
            serie_Top5Ciudades = preSerie_Top5Ciudades;




        }

        







    }
}

