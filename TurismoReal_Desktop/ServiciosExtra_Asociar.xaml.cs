/*  En esta parte, iterar por depto buscando correlacion en listado de disponibilidad. 
    Si no existe correlacion, ambos checkbox son false, y disp_createOrUpdate es Create.
    Si existe, "Ofrecido aqui" es verdadero, disponible toma el valor desde Disponibilidad_servicio, y disp_createOrUpdate es Update. 
              
    Copiar listado resultante de Dptos para poblar la tabla, de manera que se pueda comparar la version original del listado con la modificada,
    realizando updates y deletes solo donde se modificaron las cosas.
              
    Entonces, al presionar guardar cambios, comparar cada registro del datagrid con su version original, si se desmarco Ofrecido, delete from.
    Si Ofrecido sigue marcado pero se altero disponible, update from.
              
    Terminando ese proceso, volver a ejecutar este metodo para refrescar datagrid. 
              
    Si se desmarca Ofrecido, Disponible también, esto porque no ofrecido = registro de disponibilidad asociado es eliminado. 
    Si Ofrecido está desmarcado pero se marca Disponible, Ofrecido tambien debe marcarse. 
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

using MahApps.Metro.Controls;
using MahApps.Metro.Behaviors;
using MahApps.Metro.Controls.Dialogs;
using TurismoReal_Desktop_Controlador;
using System.Collections;

namespace TurismoReal_Desktop
{
    /// <summary>
    /// Interaction logic for ServiciosExtra_Asociar.xaml
    /// </summary>
    public partial class ServiciosExtra_Asociar : MetroWindow
    {
        public Servicio_extra selectedService;

        List<Departamento> listDptosOriginal;
        List<Departamento> listDptosModificable;

        public ServiciosExtra_Asociar()
        {
            InitializeComponent();
        }

        public ServiciosExtra_Asociar (Servicio_extra servicio)
        {
            InitializeComponent();
            selectedService = servicio;
            lb_selectedServicio.Content = ArmarLabel();
            RecargarListadoDptos();
        }

        private async void RecargarListadoDptos()
        {
            listDptosModificable = new List<Departamento>();
            listDptosOriginal = new List<Departamento>();

            Departamento dpto = new Departamento();
            Disponibilidad_servicio disp = new Disponibilidad_servicio();

            listDptosOriginal = dpto.ListarTodo();
            List<Disponibilidad_servicio> listDisponibilidades = disp.ListarTodoDeServicio(selectedService.ID_SERVICIO);

            Boolean listDptos_Vacio = listDptosOriginal.Count == 0;
            Boolean listDisponibilidad_Vacio = listDisponibilidades.Count == 0;

            Boolean coincidenciaEncontrada;

            // Si no hay departamentos en el listado, detener el proceso aqui. 
            if (listDptos_Vacio)
            {
                await this.ShowMessageAsync("No se pudo recuperar el listado", "Por favor, asegurese de tener departamentos ya registrados en el sistema, e intentelo nuevamente.");
                return;
            }

            // Si hay al menos un depto, pero no hay registros de disponibilidad, el servicio no se ofrece en ninguno, y tampoco está disponible. 
            if (listDisponibilidad_Vacio)
            {
                foreach (Departamento depto in listDptosOriginal)
                {
                    dpto.disp_createOrUpdate = "CREATE";
                    dpto.disp_asociado = false;
                    dpto.disp_habilitado = false;
                }

                // Se hace una copia para comparar luego el original y el posiblemente modificado. ERROR: ESTO ES UNA COPIA SUPERFICIAL, DEBE SER PROFUNDA PARA QUE SIRVA!!
                //listDptosModificable = listDptosOriginal; 

                // Se carga el datagrid con los dptos modificables.
                dg_relacionDptos.ItemsSource = listDptosModificable;

                return;
            }

            // Si tanto la lista de dptos la lista de disponibilidad tienen datos, iterar por dpto para luego mostrar disponibilidad adecuada.  
            foreach (Departamento depto in listDptosOriginal)
            {
                coincidenciaEncontrada = false;
                // Iterar, considerando cada depto, por cada registro de disponibilidad, buscando relacion y asignando datos segun corresponda. 
                foreach (Disponibilidad_servicio dispServ in listDisponibilidades)
                {
                    // Si coincide el id de depto en el registro, es porque se ofrece el servicio en ese depto actualmente.
                    if (dispServ.ID_DPTO == depto.ID_DPTO)
                    {
                        depto.disp_createOrUpdate = "UPDATE";
                        depto.disp_asociado = true;
                        depto.disp_habilitado = dispServ.ACTUALMENTE_DISPONIBLE == "0" ? false : true;

                        coincidenciaEncontrada = true;

                        // Aqui tal vez sería posible volver mas eficiente el proceso al no seguir iterando por regs de disponibilidad si ya hubo coincidencia. 
                    }
                }
                if (coincidenciaEncontrada == false)
                {
                    depto.disp_createOrUpdate = "CREATE";
                    depto.disp_asociado = false;
                    depto.disp_habilitado = false;
                }

            }
            // Se hace una copia del original para comparar luego el este con el posiblemente modificado.
            foreach (Departamento depto in listDptosOriginal)
            {
                listDptosModificable.Add(depto.ClonarDpto());
            }

            

            // Se carga el datagrid con los dptos modificables.
            dg_relacionDptos.ItemsSource = listDptosModificable;
        }


        private async void Btn_guardarCambios_Click(object sender, RoutedEventArgs e)
        {
            // Obtener ambos listados, el original y el actual para compararlos, y hacer distintas acciones dependiendo de cada tipo de cambio.
            List<Departamento> listadoMod = (List<Departamento>)dg_relacionDptos.ItemsSource;

            int contadorCreate = 0;
            int contadorUpdate = 0;
            int contadorDelete = 0;

            for (int i = 0; i < listadoMod.Count; i++)
            {
                Departamento original = listDptosOriginal[i];
                Departamento mod = listadoMod[i];

                string disponibilidadTemporal;

                // Si algo cambio, proceder con CRUD, de lo contrario omitir:
                if (mod.disp_asociado != original.disp_asociado ||
                    mod.disp_habilitado != original.disp_habilitado)
                {
                    // Si antes estaba asociado pero se quito, delete:
                    if (original.disp_asociado && mod.disp_asociado == false)
                    {
                        mod.DeleteAsociacionServExtra(mod.ID_DPTO, selectedService.ID_SERVICIO);
                        contadorDelete++;
                    }

                    // Si antes no estaba asociado pero ahora si, create:
                    else if (original.disp_asociado == false && mod.disp_asociado)
                    {
                        disponibilidadTemporal = mod.disp_habilitado == true ? "1" : "0";
                        mod.CreateAsociacionServExtra(mod.ID_DPTO, selectedService.ID_SERVICIO, disponibilidadTemporal);
                        contadorCreate++;
                    }

                    // Si sigue asociado, pero se modifico disponible, update:
                    else if (original.disp_asociado && mod.disp_asociado && original.disp_habilitado != mod.disp_habilitado)
                    {
                        disponibilidadTemporal = mod.disp_habilitado == true ? "1" : "0";
                        mod.UpdateAsociacionServExtra(mod.ID_DPTO, selectedService.ID_SERVICIO, disponibilidadTemporal);
                        contadorUpdate++;
                    }

                }

            }

            await this.ShowMessageAsync("Cambios guardados exitosamente", "Se han registrado los cambios especificados.");
            RecargarListadoDptos();
            return;
        }


        private string ArmarLabel()
        {
            string resultado = String.Concat("Servicio actualmente seleccionado: ", selectedService.DESCRIPCION, " ($", selectedService.COSTO_ACTUAL, ")");

            return resultado;
        }

        private void btn_retroceder_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        
    }
}
