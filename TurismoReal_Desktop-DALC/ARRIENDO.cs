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
    
    public partial class ARRIENDO
    {
        public ARRIENDO()
        {
            this.MULTA = new HashSet<MULTA>();
            this.RESERVA = new HashSet<RESERVA>();
            this.SERVICIO_CONTRATADO = new HashSet<SERVICIO_CONTRATADO>();
            this.SOLICITUD_TRANSPORTE = new HashSet<SOLICITUD_TRANSPORTE>();
            this.AMIGO = new HashSet<AMIGO>();
        }
    
        public decimal ID_ARRIENDO { get; set; }
        public decimal ID_CLIENTE { get; set; }
        public decimal ID_DPTO { get; set; }
        public System.DateTime FECHA_INICIO { get; set; }
        public System.DateTime FECHA_FIN { get; set; }
        public string CHECK_IN { get; set; }
        public string CHECK_OUT { get; set; }
        public decimal TOTAL_ARRIENDO { get; set; }
        public decimal TOTAL_SERVICIOS { get; set; }
    
        public virtual DEPARTAMENTO DEPARTAMENTO { get; set; }
        public virtual USUARIO USUARIO { get; set; }
        public virtual ICollection<MULTA> MULTA { get; set; }
        public virtual ICollection<RESERVA> RESERVA { get; set; }
        public virtual ICollection<SERVICIO_CONTRATADO> SERVICIO_CONTRATADO { get; set; }
        public virtual ICollection<SOLICITUD_TRANSPORTE> SOLICITUD_TRANSPORTE { get; set; }
        public virtual ICollection<AMIGO> AMIGO { get; set; }
    }
}
