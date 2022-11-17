using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurismoReal_Desktop_DALC;

namespace TurismoReal_Desktop_Controlador
{
    public class Arriendo
    {
        public decimal ID_ARRIENDO { get; set; }
        public decimal ID_CLIENTE { get; set; }
        public decimal ID_DPTO { get; set; }
        public System.DateTime FECHA_INICIO { get; set; }
        public System.DateTime FECHA_FIN { get; set; }
        public string CHECK_IN { get; set; }
        public string CHECK_OUT { get; set; }
        public decimal TOTAL_ARRIENDO { get; set; }
        public decimal TOTAL_SERVICIOS { get; set; }

        public virtual DEPARTAMENTO DEPARTAMENTO { get; set; }
        public virtual USUARIO USUARIO { get; set; }
        public virtual ICollection<MULTA> MULTA { get; set; }
        public virtual ICollection<RESERVA> RESERVA { get; set; }
        public virtual ICollection<SERVICIO_CONTRATADO> SERVICIO_CONTRATADO { get; set; }
        public virtual ICollection<SOLICITUD_TRANSPORTE> SOLICITUD_TRANSPORTE { get; set; }
        public virtual ICollection<AMIGO> AMIGO { get; set; }

        private TurismoReal_Entities conn = new TurismoReal_Entities();

        public List<Arriendo> ListarTodoEnFechas(DateTime fechaInicio, DateTime fechaFin, out decimal aporteIngresos, out decimal aporteServicios)
        {
            aporteIngresos = 0;
            aporteServicios = 0;

            try
            {
                List<Arriendo> listArr = new List<Arriendo>();
                // Recuperar arriendos que hayan empezado realmente (check_in == 1) entre las fechas especificadas: 

                var listDatos = conn.ARRIENDO.Where(arrndo =>
                    arrndo.FECHA_INICIO >= fechaInicio &&
                    arrndo.FECHA_INICIO <= fechaFin &&
                    arrndo.CHECK_IN == "1");

                var cantidad = listDatos.Count();

                foreach (ARRIENDO dato in listDatos)
                {
                    Arriendo arr = new Arriendo();

                    arr.ID_ARRIENDO = dato.ID_ARRIENDO;
                    arr.ID_CLIENTE = dato.ID_CLIENTE;
                    arr.ID_DPTO = dato.ID_DPTO;
                    arr.TOTAL_ARRIENDO = dato.TOTAL_ARRIENDO;
                    arr.FECHA_INICIO = dato.FECHA_INICIO;
                    arr.FECHA_FIN = dato.FECHA_FIN;
                    arr.CHECK_IN = dato.CHECK_IN;
                    arr.CHECK_OUT = dato.CHECK_OUT;
                    arr.TOTAL_ARRIENDO = dato.TOTAL_ARRIENDO;
                    arr.TOTAL_SERVICIOS = dato.TOTAL_SERVICIOS;
                    arr.DEPARTAMENTO = dato.DEPARTAMENTO;
                    arr.USUARIO = dato.USUARIO;
                    arr.MULTA = dato.MULTA;
                    arr.RESERVA = dato.RESERVA;
                    arr.SERVICIO_CONTRATADO = dato.SERVICIO_CONTRATADO;
                    arr.SOLICITUD_TRANSPORTE = dato.SOLICITUD_TRANSPORTE;
                    arr.AMIGO = dato.AMIGO;

                    aporteIngresos += dato.TOTAL_ARRIENDO;

                    aporteServicios += dato.TOTAL_SERVICIOS;


                    listArr.Add(arr);
                }


                return listArr;
            }
            catch (Exception)
            {
                return new List<Arriendo>();
            }
        }

    }
}
