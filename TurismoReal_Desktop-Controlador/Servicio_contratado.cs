using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurismoReal_Desktop_DALC;

namespace TurismoReal_Desktop_Controlador
{
    public class Servicio_contratado
    {
        public decimal ID_ARRIENDO { get; set; }
        public decimal ID_SERVICIO { get; set; }
        public decimal COSTO { get; set; }
        public Nullable<System.DateTime> FECHA_REALIZACION { get; set; }
        public string REALIZADO { get; set; }
        public string POST_CHECK_IN { get; set; }
        public virtual ARRIENDO ARRIENDO { get; set; }
        public virtual SERVICIO_EXTRA SERVICIO_EXTRA { get; set; }

        private TurismoReal_Entities conn = new TurismoReal_Entities();

        public List<Servicio_contratado> ListarTodoEnFechas(DateTime fechaInicio, DateTime fechaFin)
        {
            try
            {
                List<Servicio_contratado> listServs = new List<Servicio_contratado>();
                // Recuperar Servicios contratados que hayan ocurrido entre las fechas especificadas: 
                var listDatos = conn.SERVICIO_CONTRATADO.Where(servCont =>
                servCont.FECHA_REALIZACION >= fechaInicio && servCont.FECHA_REALIZACION <= fechaFin && servCont.REALIZADO == "1");

                foreach (SERVICIO_CONTRATADO dato in listDatos)
                {
                    Servicio_contratado servCont = new Servicio_contratado();

                    servCont.ID_ARRIENDO = dato.ID_ARRIENDO;
                    servCont.ID_SERVICIO = dato.ID_SERVICIO;
                    servCont.COSTO = dato.COSTO;
                    servCont.FECHA_REALIZACION = dato.FECHA_REALIZACION;
                    servCont.REALIZADO = dato.REALIZADO;
                    servCont.POST_CHECK_IN = dato.POST_CHECK_IN;
                    servCont.ARRIENDO = dato.ARRIENDO;
                    servCont.SERVICIO_EXTRA= dato.SERVICIO_EXTRA;

                    listServs.Add(servCont);
                }
                return listServs;
            }
            catch (Exception)
            {
                return new List<Servicio_contratado>();
            }
        }


    }
}
