using Oracle.ManagedDataAccess.Client;
using System;
using System.Windows;
using MahApps.Metro.Controls;
using MahApps.Metro.Behaviors;
using MahApps.Metro.Controls.Dialogs;

using TurismoReal_Desktop_Controlador;

namespace TurismoReal_Desktop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        //public static MainMenu win_menu;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void btn_login_Click(object sender, RoutedEventArgs e)
        {
            Login();
        }


        private async void Login()
        {
            //Validar que se posean datos en los campos: 

            string username = tb_user.Text;
            string pass = tb_pass.Password;

            if (username.Length == 0)
            {
                await this.ShowMessageAsync("Faltan datos", "Por favor, ingrese su nombre de usuario en el campo respectivo.");

                return;
            }

            if (pass.Length == 0)
            {
                await this.ShowMessageAsync("Faltan datos", "Por favor, ingrese su contraseña en el campo respectivo.");

                //MessageBox.Show("Por favor, ingrese su contraseña en el campo respectivo.", "Faltan datos", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            System.Windows.Input.Mouse.OverrideCursor = System.Windows.Input.Cursors.Wait;



            System.Data.Objects.ObjectParameter p_nombre;
            System.Data.Objects.ObjectParameter p_ape_pat;
            System.Data.Objects.ObjectParameter p_id_usuario;
            System.Data.Objects.ObjectParameter p_tipo_usuario;



            Usuario user = new Usuario();
            user.Login(username, pass, out p_nombre, out p_ape_pat, out p_id_usuario, out p_tipo_usuario);

            int.TryParse(p_id_usuario.Value.ToString(), out int id_usuario);




            if (id_usuario == -1)
            {
                System.Windows.Input.Mouse.OverrideCursor = System.Windows.Input.Cursors.Arrow;
                await this.ShowMessageAsync("Datos incorrectos", "Los datos ingresados no corresponden a ningun usuario.\n\nPor favor, intentelo nuevamente");
                //MessageBox.Show("Los datos ingresados no corresponden a ningun usuario.\n\nPor favor, intentelo nuevamente","Datos incorrectos",MessageBoxButton.OK,MessageBoxImage.Error);
            }
            else if (id_usuario > 0 && p_tipo_usuario.Value.ToString() != "Administrador")
            {
                System.Windows.Input.Mouse.OverrideCursor = System.Windows.Input.Cursors.Arrow;
                await this.ShowMessageAsync("Usuario no es administrador", "Esta aplicación es de uso exclusivo de los administradores");
                //MessageBox.Show("Esta aplicación es de uso exclusivo de los administradores. \n\nEl usuario ingresado existe, pero no es un administrador.","Usuario sin acceso",MessageBoxButton.OK,MessageBoxImage.Asterisk);
            }
            else
            {
                System.Windows.Input.Mouse.OverrideCursor = System.Windows.Input.Cursors.Arrow;
                MessageBox.Show($"Bienvenido, {p_nombre.Value} {p_ape_pat.Value}!", "Acceso concedido", MessageBoxButton.OK, MessageBoxImage.Asterisk);

                MainMenu win_mainMenu = new MainMenu();
                win_mainMenu.Show();
                this.Close();

            }
        }

        private void tb_enter_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                Login();
            }
        }
    }
}
