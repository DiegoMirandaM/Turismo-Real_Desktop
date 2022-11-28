using MahApps.Metro.Controls;
using System.Windows;

namespace TurismoReal_Desktop
{
    /// <summary>
    /// Interaction logic for MainMenu.xaml
    /// </summary>
    public partial class MainMenu : MetroWindow
    {
        public MainMenu()
        {
            InitializeComponent();
        }

        private void btn_winDpto_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Input.Mouse.OverrideCursor = System.Windows.Input.Cursors.Wait;
            Dpto win_dpo = new Dpto();
            win_dpo.Show();

            this.Close();
            System.Windows.Input.Mouse.OverrideCursor = System.Windows.Input.Cursors.Arrow;
        }

        private void btn_cerrarSesion_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult res = MessageBox.Show("¿Estás seguro de que quieres salir?", "Confirmación", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);

            if (res == MessageBoxResult.Yes)
            {
                System.Windows.Input.Mouse.OverrideCursor = System.Windows.Input.Cursors.Wait;
                MainWindow win_login = new MainWindow();
                win_login.Show();
                this.Close();
                System.Windows.Input.Mouse.OverrideCursor = System.Windows.Input.Cursors.Arrow;
            }

        }


        private void btn_transporte_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Input.Mouse.OverrideCursor = System.Windows.Input.Cursors.Wait;

            Transporte win_transporte = new Transporte();
            win_transporte.Show();
            this.Close();
            System.Windows.Input.Mouse.OverrideCursor = System.Windows.Input.Cursors.Arrow;
        }

        private void btn_winServiciosExtra_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Input.Mouse.OverrideCursor = System.Windows.Input.Cursors.Wait;

            ServiciosExtra win_servicios = new ServiciosExtra();
            win_servicios.Show();
            this.Close();
            System.Windows.Input.Mouse.OverrideCursor = System.Windows.Input.Cursors.Arrow;
        }

        private void btn_winUsuarios_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Input.Mouse.OverrideCursor = System.Windows.Input.Cursors.Wait;

            Win_Usuario win_usuario = new Win_Usuario();
            win_usuario.Show();
            this.Close();
            System.Windows.Input.Mouse.OverrideCursor = System.Windows.Input.Cursors.Arrow;
        }

        private void btn_reportes_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Input.Mouse.OverrideCursor = System.Windows.Input.Cursors.Wait;

            Reportes win_reportes = new Reportes();
            win_reportes.Show();
            this.Close();
            System.Windows.Input.Mouse.OverrideCursor = System.Windows.Input.Cursors.Arrow;
        }
    }
}
