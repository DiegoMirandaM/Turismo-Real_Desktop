using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurismoReal_Desktop_DALC;

namespace TurismoReal_Desktop_Controlador
{
    public class Solicitud_transporte
    {
        public decimal ID_SOLICITUD { get; set; }
        public decimal ID_ARRIENDO { get; set; }
        public System.DateTime FECHA_INICIO { get; set; }
        public decimal PASAJEROS { get; set; }
        public string DIR_INICIO { get; set; }
        public string DIR_DESTINO { get; set; }
        public Nullable<decimal> KMS_DISTANCIA { get; set; }
        public string ACEPTADA { get; set; }
        public Nullable<decimal> COSTO { get; set; }
        public string SENTIDO_VIAJE { get; set; }
        public virtual ARRIENDO ARRIENDO { get; set; }
        public virtual ICollection<TRANSPORTE_REALIZADO> TRANSPORTE_REALIZADO { get; set; }

        public string nomCliente { get; set; }
        public string nomDepto { get; set; }

        public int IDconductorAsignado { get; set; }
        public string descripcionVehiculo { get; set; }
        public string patente { get; set; }


        private TurismoReal_Entities conn = new TurismoReal_Entities();


        public List<Solicitud_transporte> ListarTodoEnFechas(DateTime fechaInicio, DateTime fechaFin, out decimal aporteTraslados)
        {
            aporteTraslados = 0;
            try
            {
                List<Solicitud_transporte> listTransportes = new List<Solicitud_transporte>();
                // Recuperar transportes que se hayan efectuado entre las fechas especificadas: 
                var listDatos = conn.SOLICITUD_TRANSPORTE.Where(transporte =>
                transporte.FECHA_INICIO >= fechaInicio && transporte.FECHA_INICIO <= fechaFin && transporte.ACEPTADA == "1");

                foreach (SOLICITUD_TRANSPORTE dato in listDatos)
                {
                    Solicitud_transporte solicitudTransp = new Solicitud_transporte();

                    solicitudTransp.ID_SOLICITUD = dato.ID_SOLICITUD;
                    solicitudTransp.ID_ARRIENDO = dato.ID_ARRIENDO;
                    solicitudTransp.FECHA_INICIO = dato.FECHA_INICIO;
                    solicitudTransp.PASAJEROS = dato.PASAJEROS;
                    solicitudTransp.DIR_INICIO = dato.DIR_INICIO;
                    solicitudTransp.DIR_DESTINO = dato.DIR_DESTINO;
                    solicitudTransp.KMS_DISTANCIA = dato.KMS_DISTANCIA;
                    solicitudTransp.ACEPTADA = dato.ACEPTADA;
                    solicitudTransp.COSTO = dato.COSTO;
                    solicitudTransp.ARRIENDO = dato.ARRIENDO;
                    solicitudTransp.TRANSPORTE_REALIZADO = dato.TRANSPORTE_REALIZADO;

                    aporteTraslados += (decimal)dato.COSTO;


                    listTransportes.Add(solicitudTransp);
                }
                return listTransportes;
            }
            catch (Exception)
            {
                return new List<Solicitud_transporte>();
            }
        }
        
        
        public List<Solicitud_transporte> ListarSolicitudes()
        {
            try
            {
                // Obtiene datos de solicitudes. La solicitud de arriendo debe tener vigencia (no haber cancelado el arriendo), 
                var result = from T1 in conn.SOLICITUD_TRANSPORTE
                             join T2 in conn.ARRIENDO on T1.ID_ARRIENDO equals T2.ID_ARRIENDO
                             join T3 in conn.DEPARTAMENTO on T2.ID_DPTO equals T3.ID_DPTO
                             join T4 in conn.RESERVA on T2.ID_ARRIENDO equals T4.ID_ARRIENDO
                             join T5 in conn.USUARIO on T2.ID_CLIENTE equals T5.ID_USUARIO
                             join T6 in conn.TRANSPORTE_REALIZADO on T1.ID_SOLICITUD equals T6.ID_SOLICITUD
                             where T2.CHECK_OUT != "3" && T4.VIGENTE == "1" && T2.FECHA_FIN < System.DateTime.Now
                             group T2 by T2.ID_ARRIENDO into solicitudes
                             select new
                             {
                                 ID_Solic = solicitudes.Select(x => x.SOLICITUD_TRANSPORTE.Select(y => y.ID_SOLICITUD)),
                                 ID_Arrien = solicitudes.Select(x => x.ID_ARRIENDO),
                                 DirInicio = solicitudes.Select(x => x.SOLICITUD_TRANSPORTE.Select(y => y.DIR_INICIO)),
                                 DirDestino = solicitudes.Select(x => x.SOLICITUD_TRANSPORTE.Select(y => y.DIR_DESTINO)),
                                 KMSDist = solicitudes.Select(x => x.SOLICITUD_TRANSPORTE.Select(y => y.KMS_DISTANCIA)),
                                 Acepta = solicitudes.Select(x => x.SOLICITUD_TRANSPORTE.Select(y => y.ACEPTADA)),
                                 SentidoViaje = solicitudes.Select(x => x.SOLICITUD_TRANSPORTE.Select(y => y.SENTIDO_VIAJE)),
                                 Arriend = solicitudes.Select(x => x.SOLICITUD_TRANSPORTE.Select(y => y.ARRIENDO)),
                                 Cliente = solicitudes.Select(x => x.USUARIO.NOMBRE + " " + x.USUARIO.APE_PAT),
                                 DeptoSolicitado = solicitudes.Select(x => x.DEPARTAMENTO.NOMBRE),
                                 PasajerosTotales = solicitudes.Select(x => x.SOLICITUD_TRANSPORTE.Select(y => y.PASAJEROS).FirstOrDefault()),
                                 FecInicio = solicitudes.Select(x => x.FECHA_INICIO),
                                 Costo = solicitudes.Select(x => x.SOLICITUD_TRANSPORTE.Select(y => y.COSTO).FirstOrDefault()),

                                 Conductor = solicitudes.Select(x => x.SOLICITUD_TRANSPORTE.Select(y => y.TRANSPORTE_REALIZADO.Select(z => z.ID_CONDUCTOR))),
                                 Patente = solicitudes.Select(x => x.SOLICITUD_TRANSPORTE.Select(y => y.TRANSPORTE_REALIZADO.Select(z => z.PATENTE))),
                                 DescVehiculo = solicitudes.Select(x => x.SOLICITUD_TRANSPORTE.Select(y => y.TRANSPORTE_REALIZADO.Select(z => z.DESC_VEHICULO)))
                                 // FALTA INTENTAR RECUPERAR PATENTE Y CONDUCTOR EN CASO DE HABER ASIGNADOS!!!
                             };

                List<Solicitud_transporte> listSolicitudes = new List<Solicitud_transporte>();



                foreach (var res in result)
                {
                    Solicitud_transporte sol = new Solicitud_transporte();

                    sol.ID_SOLICITUD = res.ID_Solic.First().First();
                    sol.ID_ARRIENDO = res.ID_Arrien.First();
                    sol.FECHA_INICIO = res.FecInicio.First();
                    sol.PASAJEROS = res.PasajerosTotales.First();
                    sol.DIR_INICIO = res.DirInicio.First().First();
                    sol.DIR_DESTINO = res.DirDestino.First().First();
                    sol.KMS_DISTANCIA = res.KMSDist.First().First();
                    sol.ACEPTADA = res.Acepta.First().First();
                    sol.COSTO = res.Costo.First();
                    sol.SENTIDO_VIAJE = res.SentidoViaje.First().First();
                    sol.ARRIENDO = res.Arriend.First().First();
                    sol.nomCliente = res.Cliente.First();
                    sol.nomDepto = res.DeptoSolicitado.First();
                    sol.IDconductorAsignado = res.Conductor.First().First().First();
                    sol.descripcionVehiculo = res.DescVehiculo.First().First().First();
                    sol.patente = res.Patente.First().First().First();

                    listSolicitudes.Add(sol);
                }

                listSolicitudes.OrderBy(x => x.nomCliente);

                return listSolicitudes;



            }
            catch (Exception)
            {
                return new List<Solicitud_transporte>();
            }
        }
        


    }
}
