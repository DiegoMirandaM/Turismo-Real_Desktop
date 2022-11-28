using System;
using System.Collections.Generic;
using System.Linq;
using TurismoReal_Desktop_DALC;

namespace TurismoReal_Desktop_Controlador
{
    public class Solicitud_transporte
    {
        public decimal ID_SOLICITUD { get; set; }
        public decimal ID_ARRIENDO { get; set; }
        public System.DateTime FECHA_INICIO_VIAJE { get; set; }
        public System.DateTime FECHA_INICIO_ARRIENDO { get; set; }
        public decimal PASAJEROS { get; set; }
        public string DIR_INICIO { get; set; }
        public string DIR_DESTINO { get; set; }
        public string SENTIDO_VIAJE { get; set; }
        public Nullable<decimal> KMS_DISTANCIA { get; set; }

        public string ACEPTADA { get; set; }
        public bool bool_aceptada { get; set; }
        public bool bool_cancelada { get; set; }

        public decimal COSTO { get; set; }
        public virtual ARRIENDO ARRIENDO { get; set; }

        public string nomCliente { get; set; }
        public string nomDepto { get; set; }

        public int IDconductorAsignado { get; set; }
        public string descripcionVehiculo { get; set; }
        public string patente { get; set; }

        public virtual TRANSPORTE_REALIZADO TRANSPORTE_REALIZADO { get; set; }


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
                    solicitudTransp.FECHA_INICIO_VIAJE = dato.FECHA_INICIO;
                    solicitudTransp.PASAJEROS = dato.PASAJEROS;
                    solicitudTransp.DIR_INICIO = dato.DIR_INICIO;
                    solicitudTransp.DIR_DESTINO = dato.DIR_DESTINO;
                    solicitudTransp.KMS_DISTANCIA = dato.KMS_DISTANCIA;
                    solicitudTransp.ACEPTADA = dato.ACEPTADA;
                    solicitudTransp.COSTO = dato.COSTO;
                    solicitudTransp.ARRIENDO = dato.ARRIENDO;

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
                // Obtiene datos de solicitudes. La solicitud de arriendo debe tener vigencia (no haber cancelado el arriendo)
                // y el arriendo no debe haber concluido. 
                var result = from T1 in conn.SOLICITUD_TRANSPORTE
                             join T2 in conn.ARRIENDO on T1.ID_ARRIENDO equals T2.ID_ARRIENDO
                             join T3 in conn.DEPARTAMENTO on T2.ID_DPTO equals T3.ID_DPTO
                             join T4 in conn.RESERVA on T2.ID_ARRIENDO equals T4.ID_ARRIENDO
                             join T5 in conn.USUARIO on T2.ID_CLIENTE equals T5.ID_USUARIO
                             join T6 in conn.TRANSPORTE_REALIZADO on T1.ID_SOLICITUD equals T6.ID_SOLICITUD into allData
                             where T2.CHECK_OUT != "3" && T4.VIGENTE == "1" && T2.FECHA_FIN < System.DateTime.Now

                             from cruce in allData.DefaultIfEmpty()

                             select new
                             {
                                 ID_Solic = T1.ID_SOLICITUD,
                                 ID_Arrien = T1.ID_ARRIENDO,
                                 DirInicio = T1.DIR_INICIO,
                                 DirDestino = T1.DIR_DESTINO,
                                 KMSDist = T1.KMS_DISTANCIA,
                                 Acepta = T1.ACEPTADA,
                                 SentidoViaje = T1.SENTIDO_VIAJE,
                                 Arriend = T1.ARRIENDO,
                                 Cliente = T5.NOMBRE + " " + T5.APE_PAT,
                                 DeptoSolicitado = T3.NOMBRE,
                                 PasajerosTotales = T1.PASAJEROS,
                                 FecInicioViaje = T1.FECHA_INICIO,
                                 FecInicioArriendo = T2.FECHA_INICIO,
                                 Costo = T1.COSTO,
                                 // Si no hay relacion con transporte_realizado, estos datos vendran nulos, por lo que idConductor
                                 // se deja como -1 para usar de indice en el comboBox, y los demas quedan de null. 
                                 Conductor = cruce == null ? -1 : (cruce.ID_CONDUCTOR),
                                 Patente = cruce == null ? null : (cruce.PATENTE),
                                 DescVehiculo = cruce == null ? null : (cruce.DESC_VEHICULO),
                             };

                List<Solicitud_transporte> listSolicitudes = new List<Solicitud_transporte>();

                foreach (var res in result)
                {
                    Solicitud_transporte sol = new Solicitud_transporte();

                    sol.ID_SOLICITUD = res.ID_Solic;
                    sol.ID_ARRIENDO = res.ID_Arrien;
                    sol.FECHA_INICIO_VIAJE = res.FecInicioViaje;
                    sol.FECHA_INICIO_ARRIENDO = res.FecInicioArriendo;
                    sol.PASAJEROS = res.PasajerosTotales;
                    sol.DIR_INICIO = res.DirInicio;
                    sol.DIR_DESTINO = res.DirDestino;
                    sol.KMS_DISTANCIA = res.KMSDist;
                    sol.ACEPTADA = res.Acepta;
                    sol.COSTO = res.Costo;
                    sol.SENTIDO_VIAJE = res.SentidoViaje;
                    sol.ARRIENDO = res.Arriend;
                    sol.nomCliente = res.Cliente;
                    sol.nomDepto = res.DeptoSolicitado;

                    // La aceptacion de la solicitud de transporte puede ser:
                    // 0 = Ni aceptado ni rechazado;
                    // 1 = Aceptado;
                    // 2 = Rechazado;
                    if (res.Acepta == "0")
                    {
                        sol.bool_aceptada = false;
                        sol.bool_cancelada = false;
                    }
                    else if (res.Acepta == "1")
                    {
                        sol.bool_aceptada = true;
                        sol.bool_cancelada = false;
                    }
                    else if (res.Acepta == "2")
                    {
                        sol.bool_aceptada = false;
                        sol.bool_cancelada = true;
                    }

                    sol.IDconductorAsignado = res.Conductor;
                    sol.descripcionVehiculo = res.DescVehiculo;
                    sol.patente = res.Patente;


                    listSolicitudes.Add(sol);
                }

                return listSolicitudes;

            }
            catch (Exception)
            {
                return new List<Solicitud_transporte>();
            }
        }


        public bool AceptarTraslado(decimal id_solicitud, int id_conductor, string desc_vehiculo, string patente)
        {
            // Tomar datos recibidos, que debiesen ser id_solicitud, ID_conductor, desc_vehiculo, y patente para crear un
            // registro en Transporte_Realizado que inserte estos valores en un nuevo registro de Transporte_realizado.

            // Cambiar estado de aceptacion de la solicitud.
            try
            {
                conn.SP_CREATE_TRANSP_REALIZADO(id_solicitud, id_conductor, desc_vehiculo, patente);
                conn.SP_UPDATE_ESTADO_SOL_TRANSP(id_solicitud, "1");
                conn.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public bool ActualizarTraslado(decimal id_solicitud, int id_conductor, string desc_vehiculo, string patente)
        {
            try
            {
                conn.SP_UPDATE_TRANSP_REALIZADO(id_solicitud, id_conductor, desc_vehiculo, patente);
                conn.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }


        public bool RechazarTraslado(decimal id_solicitud, bool estabaAceptado)
        {
            // Al rechazar el traslado, si habia sido aceptada, tendrá que eliminarse el registro de Traslado_realizado y
            // marcar aceptado en la solicitud con un 0.
            // Si no habia sido aceptado previamente, solo actualizar aceptada con 0.
            try
            {
                conn.SP_UPDATE_ESTADO_SOL_TRANSP(id_solicitud, "2");

                if (estabaAceptado) conn.SP_DELETE_TRANSP_REALIZADO(id_solicitud);

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
