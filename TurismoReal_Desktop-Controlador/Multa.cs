using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurismoReal_Desktop_DALC;

namespace TurismoReal_Desktop_Controlador
{
    public class Multa
    {
        public decimal ID_MULTA { get; set; }
        public decimal ID_ARRIENDO { get; set; }
        public decimal MONTO_MULTA { get; set; }
        public string DESCRIPCION { get; set; }

        public virtual ARRIENDO ARRIENDO { get; set; }

        private TurismoReal_Entities conn = new TurismoReal_Entities();

        public List<Multa> ListarTodoEnFechas(DateTime fechaInicio, DateTime fechaFin)
        {
            try
            {
                List<Multa> listMultas = new List<Multa>();
                // Recuperar multas que se hayan registrado entre las fechas especificadas: 
                var listDatos = conn.MULTA.Where(multa =>
                multa.ARRIENDO.FECHA_FIN >= fechaInicio &&
                multa.ARRIENDO.FECHA_FIN <= fechaFin);

                foreach (MULTA dato in listDatos)
                {
                    Multa newMulta = new Multa();

                    newMulta.ID_MULTA = dato.ID_MULTA;
                    newMulta.ID_ARRIENDO = dato.ID_ARRIENDO;
                    newMulta.MONTO_MULTA = dato.MONTO_MULTA;
                    newMulta.DESCRIPCION = dato.DESCRIPCION;
                    newMulta.ARRIENDO = dato.ARRIENDO;

                    listMultas.Add(newMulta);
                }
                return listMultas;
            }
            catch (Exception)
            {
                return new List<Multa>();
            }
        }
    }
}
