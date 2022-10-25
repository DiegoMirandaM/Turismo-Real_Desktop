﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurismoReal_Desktop_DALC;

namespace TurismoReal_Desktop_Controlador
{
    public class Departamento
    {
        public decimal ID_DPTO { get; set; }
        public decimal ID_CIUDAD { get; set; }
        public string NOMBRE { get; set; }
        public string DIRECCION { get; set; }
        public string SUPERFICIE_DPTO { get; set; }
        public string NRO_DPTO { get; set; }
        public decimal PRECIO_DPTO { get; set; }
        public string DISPONIBLE { get; set; }
        public string ArrendableStr { get; set; }
        public string CONDICION { get; set; }
        public virtual ICollection<ARRIENDO> ARRIENDO { get; set; }
        public virtual Ciudad Negocio_Ciudad { get; set; }
        public virtual ICollection<IMAGEN> IMAGEN { get; set; }
        public virtual ICollection<INVENTARIO> INVENTARIO { get; set; }
        public virtual ICollection<MANTENCION> MANTENCION { get; set; }
        public virtual ICollection<DISPONIBILIDAD_SERVICIO> DISPONIBILIDAD_SERVICIO { get; set; }

        public string disp_createOrUpdate { get; set; }
        public Boolean disp_asociado { get; set; }
        public Boolean disp_habilitado { get; set; }


        //private TurismoReal_Entities conn = new TurismoReal_Entities();
        private TurismoReal_Entities_Final conn = new TurismoReal_Entities_Final();


        public List<Departamento> ListarTodo()
        {
            try
            {
                List<Departamento> listDep = new List<Departamento>();
                List<DEPARTAMENTO> listDatos = conn.DEPARTAMENTO.ToList<DEPARTAMENTO>();

                foreach (DEPARTAMENTO dato in listDatos)
                {
                    Departamento depa = new Departamento();
                    depa.ID_DPTO = dato.ID_DPTO;
                    depa.ID_CIUDAD = dato.ID_CIUDAD;
                    depa.NOMBRE = dato.NOMBRE;
                    depa.DIRECCION = dato.DIRECCION;
                    depa.SUPERFICIE_DPTO = dato.SUPERFICIE_DPTO;
                    depa.NRO_DPTO = dato.NRO_DPTO;
                    depa.PRECIO_DPTO = dato.PRECIO_DPTO;
                    depa.DISPONIBLE = dato.DISPONIBLE;
                    depa.ArrendableStr = dato.DISPONIBLE.Equals("0") ? "No" : "Sí";
                    depa.CONDICION = dato.CONDICION;
                    depa.ARRIENDO = dato.ARRIENDO;
                    
                    Ciudad city = new Ciudad();
                    city.ID_CIUDAD = dato.CIUDAD.ID_CIUDAD;
                    city.NOMBRE = dato.CIUDAD.NOMBRE;

                    depa.Negocio_Ciudad = city;

                    depa.IMAGEN = dato.IMAGEN;
                    depa.INVENTARIO = dato.INVENTARIO;
                    depa.MANTENCION = dato.MANTENCION;
                    depa.DISPONIBILIDAD_SERVICIO = dato.DISPONIBILIDAD_SERVICIO;
        
                    listDep.Add(depa);
                }
                return listDep;
            }
            catch (Exception)
            {
                return new List<Departamento>();
            }
        }
        
        public Boolean alternarDisponibilidad(decimal id_dpto, string nuevoEstado)
        {
            try
            {
                conn.SP_ALTERNAR_DISP_DPTO(id_dpto, nuevoEstado);
                conn.SaveChanges();

                return true;
            }
            catch(Exception)
            {
                return false;
            }
        }

        public Boolean agregarDpto(decimal idCiudad, string newDpto_NOMBRE, string newDpto_DIRECCION, string newDpto_SUPERFICIE_DPTO, string nroDpto, decimal precioDecimal, string newDpto_DISPONIBLE, string newDpto_CONDICION)
        {
            try
            {
                conn.SP_CREATE_DPTO(idCiudad, newDpto_NOMBRE, newDpto_DIRECCION, newDpto_SUPERFICIE_DPTO, precioDecimal, newDpto_DISPONIBLE, newDpto_CONDICION, nroDpto);
                conn.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
            
        }

        public Boolean updateDpto(decimal ID_DPTO, decimal idCiudad, string newDpto_NOMBRE, string newDpto_DIRECCION, string newDpto_SUPERFICIE_DPTO, string nroDpto, decimal precioDecimal, string DISPONIBLE, string newDpto_estado)
        {
            try
            {
                conn.SP_UPDATE_DPTO(ID_DPTO, idCiudad, newDpto_NOMBRE, newDpto_DIRECCION, newDpto_SUPERFICIE_DPTO, nroDpto, precioDecimal, DISPONIBLE, newDpto_estado);
                conn.SaveChanges();

                return true;
            }
            catch(Exception)
            {
                return false;
            }
        }

    }
}