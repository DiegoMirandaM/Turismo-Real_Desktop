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
    
    public partial class SERVICIO_CONTRATADO
    {
        public decimal ID_ARRIENDO { get; set; }
        public decimal ID_SERVICIO { get; set; }
        public decimal COSTO { get; set; }
        public Nullable<System.DateTime> FECHA_REALIZACION { get; set; }
        public string REALIZADO { get; set; }
    
        public virtual ARRIENDO ARRIENDO { get; set; }
        public virtual SERVICIO_EXTRA SERVICIO_EXTRA { get; set; }
    }
}
