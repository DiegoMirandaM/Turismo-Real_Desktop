using System;
using System.Collections.Generic;
using System.Linq;
using TurismoReal_Desktop_DALC;

namespace TurismoReal_Desktop_Controlador
{
    public class IngresosDeCiudad
    {
        public decimal idCiudad { get; set; }
        public string nombreCiudad { get; set; }
        public decimal totalIngresos { get; set; }

        private TurismoReal_Entities conn = new TurismoReal_Entities();

        public IngresosDeCiudad() { }
        public IngresosDeCiudad(decimal id, string nombre, decimal ingresos)
        {
            idCiudad = id;
            nombreCiudad = nombre;
            totalIngresos = ingresos;
        }

        public List<IngresosDeCiudad> ListarTodoEnFechas(DateTime fechaInicio, DateTime fechaFin)
        {
            try
            {
                // Obtiene el resumen de ingresos por ciudad para todos los departamentos arrendados en el rango dado:
                var result = from T1 in conn.ARRIENDO
                             join T2 in conn.DEPARTAMENTO on T1.ID_DPTO equals T2.ID_DPTO
                             join T3 in conn.CIUDAD on T2.ID_CIUDAD equals T3.ID_CIUDAD
                             where T1.FECHA_INICIO >= fechaInicio && T1.FECHA_INICIO <= fechaFin && T1.CHECK_IN == "1"
                             group T1 by T3.NOMBRE into depto
                             select new
                             {
                                 ID = depto.Select(x => x.DEPARTAMENTO.ID_CIUDAD),
                                 Nombre = (depto.Select(x => x.DEPARTAMENTO.CIUDAD.NOMBRE)),
                                 TotalDeIngresos = depto.Sum(x => x.TOTAL_ARRIENDO + x.TOTAL_SERVICIOS)
                             };


                List<IngresosDeCiudad> listIngresos = new List<IngresosDeCiudad>();

                result = result.OrderByDescending(x => x.TotalDeIngresos);
                result.Take(5);


                foreach (var res in result)
                {
                    IngresosDeCiudad ing = new IngresosDeCiudad();

                    ing.idCiudad = res.ID.First();
                    ing.nombreCiudad = res.Nombre.First();
                    ing.totalIngresos = res.TotalDeIngresos;

                    listIngresos.Add(ing);
                }
                return listIngresos;
            }
            catch (Exception)
            {
                return new List<IngresosDeCiudad>();
            }
        }


    }
}
