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
    
    public partial class CONDUCTOR
    {
        public CONDUCTOR()
        {
            this.TRANSPORTE_REALIZADO = new HashSet<TRANSPORTE_REALIZADO>();
        }
    
        public int ID_CONDUCTOR { get; set; }
        public string NOMBRE { get; set; }
        public string APE_PAT { get; set; }
        public string APE_MAT { get; set; }
        public string TIPO_LICENCIA { get; set; }
        public System.DateTime FEC_NAC { get; set; }
    
        public virtual ICollection<TRANSPORTE_REALIZADO> TRANSPORTE_REALIZADO { get; set; }
    }
}
