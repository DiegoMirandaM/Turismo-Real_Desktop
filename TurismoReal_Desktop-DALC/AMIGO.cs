//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
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
            this.ARRIENDO = new HashSet<ARRIENDO>();
        }
    
        public decimal ID_AMIGO { get; set; }
        public int RUT { get; set; }
        public string DV { get; set; }
        public string NOMBRE_COMPLETO { get; set; }
        public System.DateTime FEC_NAC { get; set; }
        public string TELEFONO { get; set; }
        public string EMAIL { get; set; }
    
        public virtual ICollection<ARRIENDO> ARRIENDO { get; set; }
    }
}
