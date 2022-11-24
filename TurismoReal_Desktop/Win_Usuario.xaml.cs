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

namespace TurismoReal_Desktop
{
    /// <summary>
    /// Interaction logic for Win_Usuario.xaml
    /// </summary>
    public partial class Win_Usuario : MetroWindow
    {
        private bool actualizando;
        private Usuario seleccionado;
        private int categoriaSeleccionada;

        public Win_Usuario()
        {
            InitializeComponent();
            btn_tipoCliente_Click(null, null);
            Cargar_tipos_usuarios();
            Alternar_habil_btns(Btn_updateDatos, false);
        }

        private void btn_retroceder_Click(object sender, RoutedEventArgs e)
        {
            MainMenu win_mainMenu = new MainMenu();
            win_mainMenu.Show();
            this.Close();
        }

        private void btn_tipoCliente_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Input.Mouse.OverrideCursor = System.Windows.Input.Cursors.Wait;

            Usuario user = new Usuario();
            dg_usuarios.ItemsSource = user.ListarUsuariosPorTipo(1);
            categoriaSeleccionada = 1;

            Alternar_habil_btns(btn_tipoCliente, false);
            Alternar_habil_btns(btn_tipoFuncionario, true);
            Alternar_habil_btns(btn_tipoAdmin, true);
            Alternar_habil_btns(btn_tipoDesactivados, true);

            System.Windows.Input.Mouse.OverrideCursor = System.Windows.Input.Cursors.Arrow;
        }
        private void btn_tipoFuncionario_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Input.Mouse.OverrideCursor = System.Windows.Input.Cursors.Wait;

            Usuario user = new Usuario();
            dg_usuarios.ItemsSource = user.ListarUsuariosPorTipo(2);
            categoriaSeleccionada = 2;

            Alternar_habil_btns(btn_tipoCliente, true);
            Alternar_habil_btns(btn_tipoFuncionario, false);
            Alternar_habil_btns(btn_tipoAdmin, true);
            Alternar_habil_btns(btn_tipoDesactivados, true);

            System.Windows.Input.Mouse.OverrideCursor = System.Windows.Input.Cursors.Arrow;
        }
        private void btn_tipoAdmin_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Input.Mouse.OverrideCursor = System.Windows.Input.Cursors.Wait;
            Usuario user = new Usuario();
            dg_usuarios.ItemsSource = user.ListarUsuariosPorTipo(3);
            categoriaSeleccionada = 3;

            Alternar_habil_btns(btn_tipoCliente, true);
            Alternar_habil_btns(btn_tipoFuncionario, true);
            Alternar_habil_btns(btn_tipoAdmin, false);
            Alternar_habil_btns(btn_tipoDesactivados, true);

            System.Windows.Input.Mouse.OverrideCursor = System.Windows.Input.Cursors.Arrow;
        }
        private void btn_tipoDesactivados_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Input.Mouse.OverrideCursor = System.Windows.Input.Cursors.Wait;

            Usuario user = new Usuario();
            dg_usuarios.ItemsSource = user.ListarUsuariosPorTipo(4);
            categoriaSeleccionada = 4;

            Alternar_habil_btns(btn_tipoCliente, true);
            Alternar_habil_btns(btn_tipoFuncionario, true);
            Alternar_habil_btns(btn_tipoAdmin, true);
            Alternar_habil_btns(btn_tipoDesactivados, false);

            System.Windows.Input.Mouse.OverrideCursor = System.Windows.Input.Cursors.Arrow;
        }

        private void dg_usuarios_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // Tomar datos y usuario seleccionado para actualizarlo al hacer doble clic:
            if (actualizando == false)
            {
                if (dg_usuarios.SelectedItem == null) return;

                if (Btn_updateDatos.IsEnabled == false)
                {
                    Alternar_habil_btns(Btn_updateDatos, true);
                }

                seleccionado = dg_usuarios.SelectedItem as Usuario;

                tb_apeMat.Text = seleccionado.APE_MAT;
                tb_apePat.Text = seleccionado.APE_PAT;
                tb_ciudad.Text = seleccionado.CIUDAD;
                tb_direccion.Text = seleccionado.DIRECCION;
                tb_email.Text = seleccionado.EMAIL;
                tb_nombre.Text = seleccionado.NOMBRE;
                tb_rut.Text = seleccionado.RUT + "-" + seleccionado.DV;
                tb_telefono.Text = seleccionado.TELEFONO;
                tb_username.Text = seleccionado.USERNAME;
                cb_tipoUsuario.Text = seleccionado.TIPO_USUARIO.DESCRIPCION;

                // Si el area esta vacia, dejar el textbox vacio. Si tiene datos, tomarlos:                
                tb_area.Text = string.IsNullOrWhiteSpace(seleccionado.AREA_FUNCIONARIO) ? null : seleccionado.AREA_FUNCIONARIO;

            }
        }

        private void Alternar_habil_btns(Button boton, bool estado)
        {
            boton.IsEnabled = estado;
        }

        private void Cargar_tipos_usuarios()
        {
            Tipo_usuario tp = new Tipo_usuario();
            cb_tipoUsuario.ItemsSource = tp.ListarTodo();
            cb_tipoUsuario.SelectedIndex = -1;
        }

        private async void Btn_crearUsuario_Click(object sender, RoutedEventArgs e)
        {
            // Verifica que no falten datos antes de proceder
            if (String.IsNullOrWhiteSpace(tb_nombre.Text.Trim()) ||
                String.IsNullOrWhiteSpace(tb_rut.Text.Trim()) ||
                String.IsNullOrWhiteSpace(tb_direccion.Text.Trim()) ||
                String.IsNullOrWhiteSpace(tb_username.Text.Trim()) ||
                String.IsNullOrWhiteSpace(tb_apePat.Text.Trim()) ||
                String.IsNullOrWhiteSpace(tb_email.Text.Trim()) ||
                String.IsNullOrWhiteSpace(tb_ciudad.Text.Trim()) ||
                String.IsNullOrWhiteSpace(tb_apeMat.Text.Trim()) ||
                String.IsNullOrWhiteSpace(tb_telefono.Text.Trim()) ||
                cb_tipoUsuario.SelectedItem == null
            )
            {
                await this.ShowMessageAsync("Registro fallido", "Por favor, complete todos los campos e intente nuevamente.");
                return;
            }

            // Toma todos los datos proporcionados en los campos de texto para comprobar que no se registre un usuario duplicado, y luego lo ingresa. 
            string nombre = tb_nombre.Text.Trim();
            string fullRut = tb_rut.Text.Trim(); // RUT COMPLETO TAL CUAL. AQUI VALIDAR Y DIVIDIR RUT DE DV PARA REGISTRO!

            char dv = fullRut.ToCharArray().Last<char>();
            string rutDV = fullRut.Replace(".", ""); //Quitar puntos en caso de haber. Queda '-' y DV. 
            if (int.TryParse(rutDV.Substring(0, rutDV.LastIndexOf('-')), out int finalRutInt) == false) // Recortar rut hasta antes del guion y DV, y lo vuelve int. 
            {
                await this.ShowMessageAsync("Registro fallido", "Ha ocurrido un error intentando procesar rut.");
                return;
            }

            string direccion = tb_direccion.Text.Trim();
            string username = tb_username.Text.Trim();
            string apePat = tb_apePat.Text.Trim();
            string email = tb_email.Text.Trim();
            string ciudad = tb_ciudad.Text.Trim();
            string apeMat = tb_apeMat.Text.Trim();
            string telefono = tb_telefono.Text.Trim();
            Decimal.TryParse(cb_tipoUsuario.SelectedValue.ToString(), out decimal idTipoUsuario);
            // Si el tipo de usuario no es funcionario, dejar null el area de trabajo: 
            string area = idTipoUsuario == 2 ? tb_area.Text.Trim() : null;

            // Si se especifico contraseña para sobreescribir, usar esa. Si no, usar rut sin puntos ni guion. 
            string password = string.IsNullOrWhiteSpace(tb_pass.Text.Trim()) ? finalRutInt.ToString() : tb_pass.Text.Trim();

            // Si es funcionario, requerir area. Si no es funcionario, ignorar area: 
            if (idTipoUsuario == 2 && string.IsNullOrWhiteSpace(area))
            {
                await this.ShowMessageAsync("Datos faltantes", "Para los funcionarios, por favor especifique área en donde trabaja.");
                return;
            }





            // Comprobar que usuario no se encuentre ya registrado:
            foreach (Usuario user in dg_usuarios.Items)
            {
                if (rutDV == user.rutCompleto && email == user.EMAIL || username == user.USERNAME)
                {
                    await this.ShowMessageAsync("Usuario duplicado", "El usuario que intenta registrar ya figura en los registros.");
                    return;
                }
            }

            actualizando = true;

            Usuario newUser = new Usuario();
            Boolean resultado = newUser.agregarUsuario(idTipoUsuario, nombre, apePat, apeMat, finalRutInt, dv.ToString(), direccion, ciudad, telefono, email, area, username, TR_Recursos.ConvertirSha256(password));

            if (resultado)
            {
                limpiarCampos();

                await this.ShowMessageAsync("Registro exitoso", "El nuevo departamento se ha registrado exitosamente.");
            }
            else
            {
                await this.ShowMessageAsync("El registro ha fallado", "Algo ha salido mal, por favor intentelo nuevamente.");
            }

            actualizando = false;
        }

        private async void Btn_updateDatos_Click(object sender, RoutedEventArgs e)
        {
            // Verifica que no falten datos antes de proceder
            if (String.IsNullOrWhiteSpace(tb_nombre.Text.Trim()) ||
                String.IsNullOrWhiteSpace(tb_rut.Text.Trim()) ||
                String.IsNullOrWhiteSpace(tb_direccion.Text.Trim()) ||
                String.IsNullOrWhiteSpace(tb_username.Text.Trim()) ||
                String.IsNullOrWhiteSpace(tb_apePat.Text.Trim()) ||
                String.IsNullOrWhiteSpace(tb_email.Text.Trim()) ||
                String.IsNullOrWhiteSpace(tb_ciudad.Text.Trim()) ||
                String.IsNullOrWhiteSpace(tb_apeMat.Text.Trim()) ||
                String.IsNullOrWhiteSpace(tb_telefono.Text.Trim()) ||
                cb_tipoUsuario.SelectedItem == null
            )
            {
                await this.ShowMessageAsync("Registro fallido", "Por favor, complete todos los campos e intente nuevamente.");
                return;
            }

            // Toma todos los datos proporcionados en los campos de texto para comprobar que no se registre un usuario duplicado, y luego lo ingresa. 
            string nombre = tb_nombre.Text.Trim();
            string fullRut = tb_rut.Text.Trim(); // RUT COMPLETO TAL CUAL. AQUI VALIDAR Y DIVIDIR RUT DE DV PARA REGISTRO!

            char dv = fullRut.ToCharArray().Last<char>();
            string rutDV = fullRut.Replace(".", ""); //Quitar puntos en caso de haber. Queda '-' y DV. 
            if (int.TryParse(rutDV.Substring(0, rutDV.LastIndexOf('-')), out int finalRutInt) == false) // Recortar rut hasta antes del guion y DV, y lo vuelve int. 
            {
                await this.ShowMessageAsync("Registro fallido", "Ha ocurrido un error intentando procesar rut.");
                return;
            }

            string direccion = tb_direccion.Text.Trim();
            string username = tb_username.Text.Trim();
            string apePat = tb_apePat.Text.Trim();
            string email = tb_email.Text.Trim();
            string ciudad = tb_ciudad.Text.Trim();
            string apeMat = tb_apeMat.Text.Trim();
            string telefono = tb_telefono.Text.Trim();
            Decimal.TryParse(cb_tipoUsuario.SelectedValue.ToString(), out decimal idTipoUsuario);
            // Si el tipo de usuario no es funcionario, dejar null el area de trabajo: 
            string area = idTipoUsuario == 2 ? tb_area.Text.Trim() : null;

            // Si se especifico contraseña para sobreescribir, usar esa. Si no, usar la actual. 
            string password = string.IsNullOrWhiteSpace(tb_pass.Text.Trim()) ? seleccionado.PASSWORD : TR_Recursos.ConvertirSha256(tb_pass.Text.Trim());

            // Si es funcionario, requerir area. Si no es funcionario, ignorar area: 
            if (idTipoUsuario == 2 && string.IsNullOrWhiteSpace(area))
            {
                await this.ShowMessageAsync("Datos faltantes", "Para los funcionarios, por favor especifique área en donde trabaja.");
                return;
            }

            // No debe actualizar si son los mismos datos. Si cambia al menos uno, actualiza. 
            if (tb_nombre.Text.Trim() == seleccionado.NOMBRE &&
                tb_rut.Text.Trim() == seleccionado.rutCompleto &&
                tb_direccion.Text.Trim() == seleccionado.DIRECCION &&
                tb_username.Text.Trim() == seleccionado.USERNAME &&
                tb_apePat.Text.Trim() == seleccionado.APE_PAT &&
                tb_email.Text.Trim() == seleccionado.EMAIL &&
                tb_ciudad.Text.Trim() == seleccionado.CIUDAD &&
                tb_apeMat.Text.Trim() == seleccionado.APE_MAT &&
                tb_telefono.Text.Trim() == seleccionado.TELEFONO &&
                TR_Recursos.ConvertirSha256(tb_pass.Text.Trim()) == seleccionado.PASSWORD &&
                ((Tipo_usuario)cb_tipoUsuario.SelectedItem).DESCRIPCION == seleccionado.TIPO_USUARIO.DESCRIPCION
            )
            {
                await this.ShowMessageAsync("Mismos datos", "Aún no se han modificado datos del registro original.");
                return;
            }
            else
            {
                // Actualización de los datos del usuario 
                actualizando = true;

                Boolean resultado = seleccionado.updateUsuario(seleccionado.ID_USUARIO, idTipoUsuario, nombre, apePat, apeMat, finalRutInt, dv.ToString(), direccion, ciudad, telefono, email, area, username, password);

                if (resultado)
                {
                    limpiarCampos();
                    await this.ShowMessageAsync("Actualización exitosa", "Los cambios han sido guardados.");

                }
                else
                {
                    await this.ShowMessageAsync("La actualización ha fallado", "Algo ha salido mal, por favor intentelo nuevamente.");
                }

                actualizando = false;
            }
        }

        private void limpiarCampos()
        {
            tb_nombre.Clear();
            tb_rut.Clear();
            tb_direccion.Clear();
            tb_username.Clear();
            tb_apePat.Clear();
            tb_email.Clear();
            tb_ciudad.Clear();
            tb_apeMat.Clear();
            tb_telefono.Clear();
            cb_tipoUsuario.SelectedIndex = -1;
            tb_area.Clear();
            tb_pass.Clear();

            seleccionado = null;

            dg_usuarios.SelectedItem = null;

            Alternar_habil_btns(Btn_updateDatos, false);

            RecargarListadoUsuarios();
        }

        private void RecargarListadoUsuarios()
        {
            Usuario user = new Usuario();
            dg_usuarios.ItemsSource = user.ListarUsuariosPorTipo(categoriaSeleccionada);
        }

    }
}
