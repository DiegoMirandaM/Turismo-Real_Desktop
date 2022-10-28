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

        public Win_Usuario()
        {
            InitializeComponent();
            btn_tipoCliente_Click(null, null);
            Cargar_tipos_usuarios();
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

            System.Windows.Input.Mouse.OverrideCursor = System.Windows.Input.Cursors.Arrow;
        }

        private void btn_tipoFuncionario_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Input.Mouse.OverrideCursor = System.Windows.Input.Cursors.Wait;

            Usuario user = new Usuario();
            dg_usuarios.ItemsSource = user.ListarUsuariosPorTipo(2);

            System.Windows.Input.Mouse.OverrideCursor = System.Windows.Input.Cursors.Arrow;
        }

        private void btn_tipoAdmin_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Input.Mouse.OverrideCursor = System.Windows.Input.Cursors.Wait;
            Usuario user = new Usuario();
            dg_usuarios.ItemsSource = user.ListarUsuariosPorTipo(3);

            System.Windows.Input.Mouse.OverrideCursor = System.Windows.Input.Cursors.Arrow;
        }

        private void btn_tipoDesactivados_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Input.Mouse.OverrideCursor = System.Windows.Input.Cursors.Wait;

            Usuario user = new Usuario();
            dg_usuarios.ItemsSource = user.ListarUsuariosPorTipo(4);

            System.Windows.Input.Mouse.OverrideCursor = System.Windows.Input.Cursors.Arrow;
        }

        private void dg_usuarios_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (actualizando == false)
            {
                if (dg_usuarios.SelectedItem == null) return; 

                if (Btn_updateDatos.IsEnabled == false)
                {
                    Alternar_habil_btns(true);
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
                cb_tipoUsuario.SelectedItem = seleccionado.TIPO_USUARIO;

                

                
            }
        }

        private void Alternar_habil_btns(bool estado)
        {
            Btn_updateDatos.IsEnabled = estado;
        }

        private void Cargar_tipos_usuarios()
        {
            Tipo_usuario tp = new Tipo_usuario();
            cb_tipoUsuario.ItemsSource = tp.ListarTodo();
            cb_tipoUsuario.SelectedItem = "Tipos de usuario";
        }
    }
}
