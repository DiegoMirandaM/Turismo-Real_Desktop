//------------------------------------------------------------------------------
// <auto-generated>
//    Este código se generó a partir de una plantilla.
//
//    Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//    Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TurismoReal_Desktop_DALC
{
    using System;
    using System.Collections.Generic;
    
    public partial class AMIGO
    {
        public AMIGO()
        {
            this.ARRIENDO_AMIGO = new HashSet<ARRIENDO_AMIGO>();
        }
    
        public decimal ID_AMIGO { get; set; }
        public int RUT { get; set; }
        public string DV { get; set; }
        public string NOMBRE_COMPLETO { get; set; }
        public System.DateTime FEC_NAC { get; set; }
        public string TELEFONO { get; set; }
        public string EMAIL { get; set; }
    
        public virtual ICollection<ARRIENDO_AMIGO> ARRIENDO_AMIGO { get; set; }
    }
}