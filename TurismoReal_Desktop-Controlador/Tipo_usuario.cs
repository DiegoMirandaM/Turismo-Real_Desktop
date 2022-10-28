using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurismoReal_Desktop_DALC;

namespace TurismoReal_Desktop_Controlador
{
    public class Tipo_usuario
    {
        public short ID_TIPOUSUARIO { get; set; }
        public string DESCRIPCION { get; set; }
        public virtual ICollection<USUARIO> USUARIO { get; set; }

        private TurismoReal_Entities conn = new TurismoReal_Entities();

        public List<Tipo_usuario> ListarTodo()
        {
            try
            {
                List<Tipo_usuario> listTipoUsuarios = new List<Tipo_usuario>();
                List<TIPO_USUARIO> listDatos = conn.TIPO_USUARIO.ToList();

                foreach (TIPO_USUARIO dato in listDatos)
                {
                    Tipo_usuario tipo = new Tipo_usuario();

                    tipo.ID_TIPOUSUARIO = dato.ID_TIPOUSUARIO;
                    tipo.DESCRIPCION = dato.DESCRIPCION;
                    tipo.USUARIO = dato.USUARIO;

                    listTipoUsuarios.Add(tipo);
                }

                return listTipoUsuarios;
            }
            catch (Exception)
            {
                return new List<Tipo_usuario>();
            }
        }

    }
}
