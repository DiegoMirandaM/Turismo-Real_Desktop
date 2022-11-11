using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using TurismoReal_Desktop_DALC;

namespace TurismoReal_Desktop_Controlador
{
    public class Imagen
    {
        public decimal ID_IMAGEN { get; set; }
        public decimal ID_DPTO { get; set; }
        public byte[] FOTO { get; set; }

        public ImageSource fotoImg { get; set; }

        public virtual DEPARTAMENTO DEPARTAMENTO { get; set; }

        private TurismoReal_Entities conn = new TurismoReal_Entities();

        public List<Imagen> ObtenerImgsDeUnDpto(decimal p_id_dpto)
        {
            try
            {
                var listDato = conn.IMAGEN.Where(img => img.ID_DPTO == p_id_dpto);
                List<Imagen> listImagenes = new List<Imagen>();

                foreach (var dato in listDato)
                {
                    Imagen img = new Imagen();

                    img.ID_IMAGEN = dato.ID_IMAGEN;
                    img.ID_DPTO = dato.ID_DPTO;
                    img.FOTO = dato.FOTO;
                    img.DEPARTAMENTO = dato.DEPARTAMENTO;
                    img.fotoImg = TR_Recursos.ByteToImage(dato.FOTO);

                    listImagenes.Add(img);
                }
                return listImagenes;

            }
            catch (Exception)
            {
                return new List<Imagen>();
            }
        }

        public Boolean CreateImagenDpto(decimal p_id_dpto, byte[] p_foto)
        {
            try
            {
                conn.SP_CREATE_IMAGEN(p_id_dpto, p_foto);
                conn.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public Boolean DeleteImagenDpto(decimal p_id_imagen)
        {
            try
            {
                conn.SP_DELETE_IMAGEN(p_id_imagen);
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
