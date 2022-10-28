using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurismoReal_Desktop_DALC;

namespace TurismoReal_Desktop_Controlador
{
    public class Inventario
    {
        public decimal ID_INVENTARIO { get; set; }
        public decimal ID_DPTO { get; set; }
        public string NOMBRE { get; set; }
        public decimal VALOR { get; set; }
        public string DISPONIBLE { get; set; }
        public string DisponibleStr { get; set; }
        public System.DateTime FECHA_COMPRA { get; set; }
        public virtual DEPARTAMENTO DEPARTAMENTO { get; set; }


        private TurismoReal_Entities conn = new TurismoReal_Entities();

        public List<Inventario> ListarInventarioDeDpto(decimal id_dpto)
        {
            try
            {
                var listDato = conn.INVENTARIO.Where(inv => inv.ID_DPTO == id_dpto);
                List<Inventario> listInventario = new List<Inventario>();

                foreach (var dato in listDato)
                {
                    Inventario inv = new Inventario();

                    inv.ID_INVENTARIO = dato.ID_INVENTARIO;
                    inv.ID_DPTO = dato.ID_DPTO;
                    inv.NOMBRE = dato.NOMBRE;
                    inv.VALOR = dato.VALOR;
                    inv.DISPONIBLE = dato.DISPONIBLE;
                    inv.DisponibleStr = dato.DISPONIBLE == "0" ? "No" : "Sí";
                    inv.FECHA_COMPRA = dato.FECHA_COMPRA;
                    inv.DEPARTAMENTO = dato.DEPARTAMENTO;

                    listInventario.Add(inv);
                }
                return listInventario;
            }
            catch (Exception)
            {
                return new List<Inventario>();
            }

        }

        public Boolean CrearItem(decimal ID_DPTO, string nombre, decimal valor, string disponible, DateTime? fecCompra)
        {
            try
            {
                conn.SP_CREATE_ITEM(ID_DPTO, nombre, valor, disponible, fecCompra);
                conn.SaveChanges();
                
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public Boolean UpdateItem(decimal ID_INVENTARIO, decimal ID_DPTO, string nombre, decimal valor, string disponible, DateTime? fecCompra)
        {
            try
            {
                conn.SP_UPDATE_ITEM(ID_INVENTARIO, ID_DPTO, nombre, valor, disponible, fecCompra);
                conn.SaveChanges();

                return true;
            }
            catch(Exception)
            {
                return false;
            }
        }

    }
}
