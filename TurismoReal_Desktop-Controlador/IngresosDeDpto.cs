using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurismoReal_Desktop_DALC;

namespace TurismoReal_Desktop_Controlador
{
    public class IngresosDeDpto
    {
        public decimal idDpto { get; set; }
        public string nombreDpto { get; set; }
        public decimal totalIngresos { get; set; }

        private TurismoReal_Entities conn = new TurismoReal_Entities();

        public IngresosDeDpto() { }
        public IngresosDeDpto (decimal id, string nombre, decimal ingresos)
        {
            idDpto = id;
            nombreDpto = nombre;
            totalIngresos = ingresos;
        }

        //public List<IngresosDeDpto> ListarTodoEnFechas(List<Arriendo> arriendosEnRango, DateTime fechaInicio, DateTime fechaFin)
        public List<IngresosDeDpto> ListarTodoEnFechas(DateTime fechaInicio, DateTime fechaFin)
        {
            try
            {
                // Obtiene el resumen de ingresos por departamento para todos los departamentos en el rango dado:
                var result = from T1 in conn.ARRIENDO
                          join T2 in conn.DEPARTAMENTO on T1.ID_DPTO equals T2.ID_DPTO
                          where T1.FECHA_INICIO >= fechaInicio && T1.FECHA_INICIO <= fechaFin && T1.CHECK_IN == "1"
                          group T1 by T1.ID_DPTO into depto
                          select new
                          {
                              ID = depto.Select(x => x.ID_DPTO),
                              Nombre = (depto.Select(x => x.DEPARTAMENTO.NOMBRE)), 
                              TotalDeIngresos = depto.Sum(x => x.TOTAL_ARRIENDO + x.TOTAL_SERVICIOS)
                          };


                List <IngresosDeDpto> listIngresos = new List<IngresosDeDpto>();

                result = result.OrderByDescending(x => x.TotalDeIngresos);
                result.Take(5);


                foreach (var res in result)
                {
                    IngresosDeDpto ing = new IngresosDeDpto();

                    ing.idDpto = res.ID.First();
                    ing.nombreDpto = res.Nombre.First();
                    ing.totalIngresos = res.TotalDeIngresos;

                    listIngresos.Add(ing);
                }
                return listIngresos;
            }
            catch (Exception)
            {
                return new List<IngresosDeDpto>();
            }
        }

    }
}
