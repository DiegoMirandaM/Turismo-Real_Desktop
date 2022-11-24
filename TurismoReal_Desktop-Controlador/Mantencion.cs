using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurismoReal_Desktop_DALC;

namespace TurismoReal_Desktop_Controlador
{
    public class Mantencion
    {
        public decimal ID_MANTENCION { get; set; }
        public decimal ID_DPTO { get; set; }
        public System.DateTime FECHA_INICIO { get; set; }
        public System.DateTime FECHA_FIN { get; set; }
        public string DESCRIPCION { get; set; }
        public decimal COSTO { get; set; }

        public virtual DEPARTAMENTO DEPARTAMENTO { get; set; }

        private TurismoReal_Entities conn = new TurismoReal_Entities();

        public List<Mantencion> ListarTodoDeDpto(decimal ID_DPTO)
        {
            try
            {
                List<Mantencion> listMantencion = new List<Mantencion>();
                var listDatos = conn.MANTENCION.Where(mantencion => mantencion.ID_DPTO == ID_DPTO);

                foreach (MANTENCION dato in listDatos)
                {
                    Mantencion mant = new Mantencion();

                    mant.ID_MANTENCION = dato.ID_MANTENCION;
                    mant.ID_DPTO = dato.ID_DPTO;
                    mant.FECHA_INICIO = dato.FECHA_INICIO;
                    mant.FECHA_FIN = dato.FECHA_FIN;
                    mant.DESCRIPCION = dato.DESCRIPCION;
                    mant.COSTO = dato.COSTO;
                    mant.DEPARTAMENTO = dato.DEPARTAMENTO;

                    listMantencion.Add(mant);
                }
                return listMantencion;
            }
            catch
            {
                return new List<Mantencion>();
            }
        }

        public Boolean CrearMantencion(decimal ID_DPTO, DateTime? fec_inicio, DateTime? fec_fin, string desc, decimal valor)
        {
            try
            {
                conn.SP_CREATE_MANTEN(ID_DPTO, fec_inicio, fec_fin, desc, valor);
                conn.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public Boolean UpdateMantencion(decimal iD, decimal p_ID_DPTO, DateTime fEC_INI, DateTime fEC_TERM, string p_DESCRIPCION, decimal p_COSTO)
        {
            try
            {
                conn.SP_UPDATE_MANTEN(iD, p_ID_DPTO, fEC_INI, fEC_TERM, p_DESCRIPCION, p_COSTO);
                conn.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public Boolean DeleteMantencion(decimal ID)
        {
            try
            {
                conn.SP_DELETE_MANTEN(ID);
                conn.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }


        public List<Mantencion> ListarTodoEnFechas(DateTime fechaInicio, DateTime fechaFin, out decimal costoMantenciones)
        {
            costoMantenciones = 0;
            try
            {
                List<Mantencion> listMantenciones = new List<Mantencion>();
                // Recuperar mantenciones que se hayan efectuado entre las fechas especificadas: 
                var listDatos = conn.MANTENCION.Where(manten =>
                manten.FECHA_INICIO >= fechaInicio &&
                manten.FECHA_INICIO <= fechaFin);

                foreach (MANTENCION dato in listDatos)
                {
                    Mantencion newManten = new Mantencion();

                    newManten.ID_MANTENCION = dato.ID_MANTENCION;
                    newManten.ID_DPTO = dato.ID_DPTO;
                    newManten.FECHA_INICIO = dato.FECHA_INICIO;
                    newManten.FECHA_FIN = dato.FECHA_FIN;
                    newManten.DESCRIPCION = dato.DESCRIPCION;
                    newManten.COSTO = dato.COSTO;
                    newManten.DEPARTAMENTO = dato.DEPARTAMENTO;

                    costoMantenciones += dato.COSTO;

                    listMantenciones.Add(newManten);
                }
                return listMantenciones;
            }
            catch (Exception)
            {
                return new List<Mantencion>();
            }
        }

    }
}
