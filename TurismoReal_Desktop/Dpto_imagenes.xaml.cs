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
using Microsoft.Win32;
using System.IO;
using TurismoReal_Desktop_Controlador;

namespace TurismoReal_Desktop
{
    /// <summary>
    /// Interaction logic for Dpto_imagenes.xaml
    /// </summary>
    public partial class Dpto_imagenes : MetroWindow
    {
        private Departamento selectedDpto;
        private Imagen selectedImg;
        private byte[] nuevaImg;

        public Dpto_imagenes()
        {
            InitializeComponent();
        }

        public Dpto_imagenes(Departamento dpto)
        {
            InitializeComponent();
            selectedDpto = dpto;

            RecargarImagenesDpto(null, null);
            btn_subirImagen.IsEnabled = false;
            btn_eliminarImagen.IsEnabled = false;
        }

        private async void btn_buscarArchivo_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = false;
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);

            openFileDialog.Filter = "Imagenes (*.jpg;*.jpeg;*.png)|*.jpg;*jpeg;*.png";

            // Si se selecciona un archivo, guardar la imagen como byte array, y la ruta del archivo.
            if (openFileDialog.ShowDialog() == true)
            {
                try
                {
                    System.Windows.Input.Mouse.OverrideCursor = System.Windows.Input.Cursors.Wait;

                    tb_rutaImg.Text = openFileDialog.FileName;
                    btn_subirImagen.IsEnabled = true;
                    nuevaImg = File.ReadAllBytes(openFileDialog.FileName);

                    System.Windows.Input.Mouse.OverrideCursor = System.Windows.Input.Cursors.Arrow;
                }
                catch (Exception)
                {
                    await this.ShowMessageAsync("Tipo de archivo incorrecto", "Por favor, seleccione solo imagenes con extensión .jpg, .jpeg o .png.");
                    return;
                }
            }
        }

        private void btn_retroceder_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void RecargarImagenesDpto(object sender, MouseButtonEventArgs e)
        {
            Imagen imgn = new Imagen();
            List<Imagen> listadoImagenes = imgn.ObtenerImgsDeUnDpto(selectedDpto.ID_DPTO);

            // Si no hay imagenes del depto
            if (listadoImagenes.Count < 1)
            {
                img_principal.Source = null;
                return;
            }

            dg_imagenes.ItemsSource = listadoImagenes;

            img_principal.Source = listadoImagenes[0].fotoImg;

        }

        private async void btn_subirImagen_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Input.Mouse.OverrideCursor = System.Windows.Input.Cursors.Wait;

            Imagen imgn = new Imagen();
            Boolean resultado = imgn.CreateImagenDpto(selectedDpto.ID_DPTO, nuevaImg);

            if (resultado)
            {
                RecargarImagenesDpto(null, null);

                await this.ShowMessageAsync("Carga de imagen exitosa", "La nueva imagen ha sido subida satisfactoriamente.");
            }
            else
            {
                await this.ShowMessageAsync("Algo salió mal", "No ha sido posible cargar la imagen, por favor intentelo nuevamente.");
            }

            System.Windows.Input.Mouse.OverrideCursor = System.Windows.Input.Cursors.Arrow;
        }

        private async void btn_eliminarImagen_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult res = MessageBox.Show("¿Estás seguro que quieres eliminar PERMANENTEMENTE la imagen seleccionada?", "Confirmación", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);

            if (res == MessageBoxResult.Yes)
            {
                Boolean resultado = selectedImg.DeleteImagenDpto(selectedImg.ID_IMAGEN);

                if (resultado)
                {
                    RecargarImagenesDpto(null, null);
                    btn_eliminarImagen.IsEnabled = false;
                    selectedImg = null;
                    await this.ShowMessageAsync("Imagen borrada", "La imagen seleccionada ha sido borrada exitosamente.");
                }
            }
        }

        private void dg_imagenes_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            selectedImg = dg_imagenes.SelectedItem as Imagen;
            img_principal.Source = selectedImg.fotoImg;
            btn_eliminarImagen.IsEnabled = true;
        }
    }
}
