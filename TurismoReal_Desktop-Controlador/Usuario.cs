using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurismoReal_Desktop_DALC;

namespace TurismoReal_Desktop_Controlador
{
    public class Usuario
    {
        public decimal ID_USUARIO { get; set; }
        public short ID_TIPOUSUARIO { get; set; }
        public string NOMBRE { get; set; }
        public string APE_PAT { get; set; }
        public string APE_MAT { get; set; }
        public int RUT { get; set; }
        public string DV { get; set; }
        public string DIRECCION { get; set; }
        public string CIUDAD { get; set; }
        public string TELEFONO { get; set; }
        public string EMAIL { get; set; }
        public string AREA_FUNCIONARIO { get; set; }
        public string USERNAME { get; set; }
        public string PASSWORD { get; set; }
        public virtual ICollection<ARRIENDO> ARRIENDO { get; set; }
        public virtual TIPO_USUARIO TIPO_USUARIO { get; set; }


        //private TurismoReal_Entities conn = new TurismoReal_Entities();
        private TurismoReal_Entities_Final conn = new TurismoReal_Entities_Final();

        public void Login(string user, 
                        string pass, 
                        out System.Data.Objects.ObjectParameter p_nombre,
                        out System.Data.Objects.ObjectParameter p_ape_pat,
                        out System.Data.Objects.ObjectParameter p_id_usuario,
                        out System.Data.Objects.ObjectParameter p_tipo_usuario)
        {
            try
            {
                p_nombre = new System.Data.Objects.ObjectParameter("P_NOMBRE", typeof(string));
                p_ape_pat = new System.Data.Objects.ObjectParameter("P_APE_PAT", typeof(string));
                p_id_usuario = new System.Data.Objects.ObjectParameter("P_ID_USUARIO", typeof(int));
                p_tipo_usuario = new System.Data.Objects.ObjectParameter("P_TIPO_USUARIO", typeof(string));

                conn.SP_LOGIN(user, pass, p_nombre, p_ape_pat, p_id_usuario, p_tipo_usuario);
            }
            catch (Exception)
            {
                p_nombre = null;
                p_ape_pat = null;
                p_id_usuario = null;
                p_tipo_usuario = null;
                return;
            }
            

        }
    }
}
