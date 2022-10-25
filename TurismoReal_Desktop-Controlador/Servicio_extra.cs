using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurismoReal_Desktop_DALC;

namespace TurismoReal_Desktop_Controlador
{
    public class Servicio_extra
    {
        public decimal ID_SERVICIO { get; set; }
        public string DESCRIPCION { get; set; }
        public decimal COSTO_ACTUAL { get; set; }
        public virtual ICollection<DISPONIBILIDAD_SERVICIO> DISPONIBILIDAD_SERVICIO { get; set; }
        public virtual ICollection<SERVICIO_CONTRATADO> SERVICIO_CONTRATADO { get; set; }

        //private TurismoReal_Entities conn = new TurismoReal_Entities();
        private TurismoReal_Entities_Final conn = new TurismoReal_Entities_Final();

        public List<Servicio_extra> ListarTodo()
        {
            try
            {
                List<Servicio_extra> listServ = new List<Servicio_extra>();
                List<SERVICIO_EXTRA> listDato = conn.SERVICIO_EXTRA.ToList<SERVICIO_EXTRA>();

                foreach (SERVICIO_EXTRA dato in listDato)
                {
                    Servicio_extra serv = new Servicio_extra();
                    serv.ID_SERVICIO = dato.ID_SERVICIO;
                    serv.DESCRIPCION = dato.DESCRIPCION;
                    serv.COSTO_ACTUAL = dato.COSTO_ACTUAL;
                    serv.DISPONIBILIDAD_SERVICIO = dato.DISPONIBILIDAD_SERVICIO;
                    serv.SERVICIO_CONTRATADO = dato.SERVICIO_CONTRATADO;

                    listServ.Add(serv);
                }
                return listServ;
            }
            catch (Exception)
            {
                return new List<Servicio_extra>();
            }
        }

        public Boolean CrearServicio(string servNombre, decimal servCosto_dec)
        {
            try
            {
                conn.SP_CREATE_SERVICIO(servNombre, servCosto_dec);
                conn.SaveChanges();

                return true;
            }
            catch(Exception)
            {
                return false;
            }
        }

        public Boolean UpdateServicio(decimal ID_SERVICIO, string tb_nombre, decimal servCosto)
        {
            try
            {
                conn.SP_UPDATE_SERVICIO(ID_SERVICIO, tb_nombre, servCosto);
                conn.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

    }
}
