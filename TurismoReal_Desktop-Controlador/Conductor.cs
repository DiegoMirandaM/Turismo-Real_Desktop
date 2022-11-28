using System;
using System.Collections.Generic;
using System.Linq;
using TurismoReal_Desktop_DALC;

namespace TurismoReal_Desktop_Controlador
{
    public class Conductor
    {
        public int ID_CONDUCTOR { get; set; }
        public string NOMBRE { get; set; }
        public string APE_PAT { get; set; }
        public string APE_MAT { get; set; }
        public string FULL_NAME { get; set; }
        public string TIPO_LICENCIA { get; set; }
        public System.DateTime FEC_NAC { get; set; }

        private TurismoReal_Entities conn = new TurismoReal_Entities();

        public List<Conductor> ListarTodos()
        {
            try
            {
                List<Conductor> listConductores = new List<Conductor>();
                List<CONDUCTOR> listDatos = conn.CONDUCTOR.ToList<CONDUCTOR>();

                foreach (CONDUCTOR dato in listDatos)
                {
                    Conductor cond = new Conductor();

                    cond.ID_CONDUCTOR = dato.ID_CONDUCTOR;
                    cond.NOMBRE = dato.NOMBRE;
                    cond.APE_PAT = dato.APE_PAT;
                    cond.APE_MAT = dato.APE_MAT;
                    cond.FULL_NAME = dato.NOMBRE + " " + dato.APE_PAT + " " + dato.APE_MAT;
                    cond.TIPO_LICENCIA = dato.TIPO_LICENCIA;
                    cond.FEC_NAC = dato.FEC_NAC;

                    listConductores.Add(cond);
                }

                return listConductores;
            }
            catch (Exception)
            {
                return new List<Conductor>();
            }
        }
    }
}
