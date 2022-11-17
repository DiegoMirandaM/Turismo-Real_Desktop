using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurismoReal_Desktop_DALC;

namespace TurismoReal_Desktop_Controlador
{
    public class Reserva
    {
        public decimal ID_RESERVA { get; set; }
        public decimal ID_ARRIENDO { get; set; }
        public string NOM_PERSONA { get; set; }
        public System.DateTime FECHA_RESERVA { get; set; }
        public decimal ACOMPANANTES { get; set; }
        public decimal COSTO_RESERVA { get; set; }
        public string VIGENTE { get; set; }

        public virtual ARRIENDO ARRIENDO { get; set; }

        private TurismoReal_Entities conn = new TurismoReal_Entities();

        public List<Reserva> ListarTodoEnFechas(DateTime fechaInicio, DateTime fechaFin, out decimal aporteReservas)
        {
            aporteReservas = 0;
            try
            {
                List<Reserva> listReservas = new List<Reserva>();
                // Recuperar reservas efectuadas en el rango, pero canceladas por el vigente == 0, ya que esto no estaría considerado
                // en resumen de arriendos, pero porque no hay reembolso de reserva, seria ingreso de dinero de todas formas: 
                var listDatos = conn.RESERVA.Where(reserv =>
                reserv.FECHA_RESERVA >= fechaInicio &&
                reserv.FECHA_RESERVA <= fechaFin &&
                reserv.VIGENTE == "0");

                foreach (RESERVA dato in listDatos)
                {
                    Reserva newReserva = new Reserva();

                    newReserva.ID_RESERVA = dato.ID_RESERVA;
                    newReserva.ID_ARRIENDO = dato.ID_ARRIENDO;
                    newReserva.NOM_PERSONA = dato.NOM_PERSONA;
                    newReserva.FECHA_RESERVA = dato.FECHA_RESERVA;
                    newReserva.ACOMPANANTES = dato.ACOMPANANTES;
                    newReserva.COSTO_RESERVA = dato.COSTO_RESERVA;
                    newReserva.VIGENTE = dato.VIGENTE;

                    aporteReservas += dato.COSTO_RESERVA;

                    listReservas.Add(newReserva);
                }
                return listReservas;
            }
            catch (Exception)
            {
                return new List<Reserva>();
            }
        }


    }
}
