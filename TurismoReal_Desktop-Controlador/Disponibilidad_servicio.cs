using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurismoReal_Desktop_DALC;

namespace TurismoReal_Desktop_Controlador
{
    public class Disponibilidad_servicio
    {
        public decimal ID { get; set; }
        public decimal ID_DPTO { get; set; }
        public decimal ID_SERVICIO { get; set; }
        public string ACTUALMENTE_DISPONIBLE { get; set; }
        public virtual DEPARTAMENTO DEPARTAMENTO { get; set; }
        public virtual SERVICIO_EXTRA SERVICIO_EXTRA { get; set; }


        private TurismoReal_Entities conn = new TurismoReal_Entities();

        public List<Disponibilidad_servicio> ListarTodoDeServicio(decimal id_servicio)
        {
            try
            {
                List<Disponibilidad_servicio> listDisponibilidad = new List<Disponibilidad_servicio>();
                var listDatos = conn.DISPONIBILIDAD_SERVICIO.Where(disp => disp.ID_SERVICIO == id_servicio);

                foreach (DISPONIBILIDAD_SERVICIO dato in listDatos)
                {
                    Disponibilidad_servicio disp = new Disponibilidad_servicio();

                    disp.ID = dato.ID;
                    disp.ID_DPTO = dato.ID_DPTO;
                    disp.ID_SERVICIO = dato.ID_SERVICIO;
                    disp.ACTUALMENTE_DISPONIBLE = dato.ACTUALMENTE_DISPONIBLE;
                    disp.DEPARTAMENTO = dato.DEPARTAMENTO;
                    disp.SERVICIO_EXTRA = dato.SERVICIO_EXTRA;

                    listDisponibilidad.Add(disp);
                }
                return listDisponibilidad;
            }
            catch (Exception)
            {
                return new List<Disponibilidad_servicio>();
            }
        }

    }
}
