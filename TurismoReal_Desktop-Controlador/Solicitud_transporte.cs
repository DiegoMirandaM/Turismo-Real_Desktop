using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurismoReal_Desktop_DALC;

namespace TurismoReal_Desktop_Controlador
{
    public class Solicitud_transporte
    {
        public decimal ID_SOLICITUD { get; set; }
        public decimal ID_ARRIENDO { get; set; }
        public System.DateTime FECHA_INICIO { get; set; }
        public decimal PASAJEROS { get; set; }
        public string DIR_INICIO { get; set; }
        public string DIR_DESTINO { get; set; }
        public Nullable<decimal> KMS_DISTANCIA { get; set; }
        public string ACEPTADA { get; set; }
        public Nullable<decimal> COSTO { get; set; }

        public virtual ARRIENDO ARRIENDO { get; set; }
        public virtual ICollection<TRANSPORTE_REALIZADO> TRANSPORTE_REALIZADO { get; set; }

        private TurismoReal_Entities conn = new TurismoReal_Entities();

        public List<Solicitud_transporte> ListarTodoEnFechas(DateTime fechaInicio, DateTime fechaFin, out decimal aporteTraslados)
        {
            aporteTraslados = 0;
            try
            {
                List<Solicitud_transporte> listTransportes = new List<Solicitud_transporte>();
                // Recuperar transportes que se hayan efectuado entre las fechas especificadas: 
                var listDatos = conn.SOLICITUD_TRANSPORTE.Where(transporte =>
                transporte.FECHA_INICIO >= fechaInicio && transporte.FECHA_INICIO <= fechaFin && transporte.ACEPTADA == "1");

                foreach (SOLICITUD_TRANSPORTE dato in listDatos)
                {
                    Solicitud_transporte solicitudTransp = new Solicitud_transporte();

                    solicitudTransp.ID_SOLICITUD = dato.ID_SOLICITUD;
                    solicitudTransp.ID_ARRIENDO = dato.ID_ARRIENDO;
                    solicitudTransp.FECHA_INICIO = dato.FECHA_INICIO;
                    solicitudTransp.PASAJEROS = dato.PASAJEROS;
                    solicitudTransp.DIR_INICIO = dato.DIR_INICIO;
                    solicitudTransp.DIR_DESTINO = dato.DIR_DESTINO;
                    solicitudTransp.KMS_DISTANCIA = dato.KMS_DISTANCIA;
                    solicitudTransp.ACEPTADA = dato.ACEPTADA;
                    solicitudTransp.COSTO = dato.COSTO;
                    solicitudTransp.ARRIENDO = dato.ARRIENDO;
                    solicitudTransp.TRANSPORTE_REALIZADO = dato.TRANSPORTE_REALIZADO;

                    aporteTraslados += (decimal)dato.COSTO;


                    listTransportes.Add(solicitudTransp);
                }
                return listTransportes;
            }
            catch (Exception)
            {
                return new List<Solicitud_transporte>();
            }
        }



    }
}
