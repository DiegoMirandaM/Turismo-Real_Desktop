using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurismoReal_Desktop_DALC;

namespace TurismoReal_Desktop_Controlador
{
    public class Ciudad
    {
        public decimal ID_CIUDAD { get; set; }
        public string NOMBRE { get; set; }
        public virtual ICollection<DEPARTAMENTO> DEPARTAMENTO { get; set; }


        //private TurismoReal_Entities conn = new TurismoReal_Entities();
        private TurismoReal_Entities_Final conn = new TurismoReal_Entities_Final();

        public List<Ciudad> listarTodo()
        {
            try
            {
                List<Ciudad> listCity = new List<Ciudad>();
                List<CIUDAD> listDatos = conn.CIUDAD.ToList<CIUDAD>();

                foreach (CIUDAD dato in listDatos)
                {
                    Ciudad city = new Ciudad();

                    city.ID_CIUDAD = dato.ID_CIUDAD;
                    city.NOMBRE = dato.NOMBRE;
                    city.DEPARTAMENTO = dato.DEPARTAMENTO;

                    listCity.Add(city);
                }
                return listCity;
            }
            catch (Exception)
            {
                return new List<Ciudad>();
            }

        }
    }
}
