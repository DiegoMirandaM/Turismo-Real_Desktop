using System;
using System.Collections.Generic;
using System.Linq;
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
        public string nombreCompleto { get; set; }
        public int RUT { get; set; }
        public string DV { get; set; }
        public string rutCompleto { get; set; }
        public string DIRECCION { get; set; }
        public string CIUDAD { get; set; }
        public string TELEFONO { get; set; }
        public string EMAIL { get; set; }
        public string AREA_FUNCIONARIO { get; set; }
        public string USERNAME { get; set; }
        public string PASSWORD { get; set; }
        public virtual ICollection<ARRIENDO> ARRIENDO { get; set; }
        public virtual TIPO_USUARIO TIPO_USUARIO { get; set; }


        private TurismoReal_Entities conn = new TurismoReal_Entities();


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

        public List<Usuario> ListarUsuariosPorTipo(decimal tipoUsuario)
        {
            try
            {
                List<Usuario> listUsuarios = new List<Usuario>();
                var listDatos = conn.USUARIO.Where(usuario => usuario.ID_TIPOUSUARIO == tipoUsuario);

                foreach (USUARIO dato in listDatos)
                {
                    Usuario user = new Usuario();

                    user.ID_USUARIO = dato.ID_USUARIO;
                    user.ID_TIPOUSUARIO = dato.ID_TIPOUSUARIO;
                    user.NOMBRE = dato.NOMBRE;
                    user.APE_PAT = dato.APE_PAT;
                    user.APE_MAT = dato.APE_MAT;
                    user.nombreCompleto = dato.NOMBRE + " " + dato.APE_PAT + " " + dato.APE_MAT;
                    user.RUT = dato.RUT;
                    user.DV = dato.DV;
                    user.rutCompleto = dato.RUT.ToString() + "-" + dato.DV;
                    user.DIRECCION = dato.DIRECCION;
                    user.CIUDAD = dato.CIUDAD;
                    user.TELEFONO = dato.TELEFONO;
                    user.EMAIL = dato.EMAIL;
                    user.AREA_FUNCIONARIO = dato.AREA_FUNCIONARIO;
                    user.USERNAME = dato.USERNAME;
                    user.PASSWORD = dato.PASSWORD;
                    user.ARRIENDO = dato.ARRIENDO;
                    user.TIPO_USUARIO = dato.TIPO_USUARIO;

                    listUsuarios.Add(user);
                }

                return listUsuarios;

            }
            catch (Exception)
            {
                return new List<Usuario>();
            }

        }

        public bool agregarUsuario(Nullable<decimal> iD_TIPO, string nOMBRE, string pATERNO, string mATERNO, Nullable<decimal> rUT, string dV, string dIRECCION, string cIUDAD, string tELEFONO, string eMAIL, string aREA, string uSUARIO, string pASS)
        {
            try
            {
                conn.SP_CREATE_USUARIO(iD_TIPO, nOMBRE, pATERNO, mATERNO, rUT, dV, dIRECCION, cIUDAD, tELEFONO, eMAIL, aREA, uSUARIO, pASS);
                conn.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool updateUsuario(Nullable<decimal> iD, Nullable<decimal> iD_TIPO, string nOMBRE, string pATERNO, string mATERNO, Nullable<decimal> rUT, string dV, string dIRECCION, string cIUDAD, string tELEFONO, string eMAIL, string aREA, string uSUARIO, string pASS)
        {
            try
            {
                conn.SP_UPDATE_USUARIO(iD, iD_TIPO, nOMBRE, pATERNO, mATERNO, rUT, dV, dIRECCION, cIUDAD, tELEFONO, eMAIL, aREA, uSUARIO, pASS);
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
