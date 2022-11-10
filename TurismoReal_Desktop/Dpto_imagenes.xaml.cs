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
        }

        private async void btn_buscarArchivo_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = false;
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);

            openFileDialog.Filter = "Imagenes (*.jpg;*.jpeg;*.png)|*.jpg;*jpeg;*.png|Todos los archivos (*.*)|*.*";

            // Si se selecciona un archivo, guardar la imagen como byte array, y la ruta del archivo.
            if (openFileDialog.ShowDialog() == true)
            {
                try
                {
                    System.Windows.Input.Mouse.OverrideCursor = System.Windows.Input.Cursors.Wait;
                    
                    // Linea comentada toma imagen especificada y la carga como img principal de la ventana.
                    //img_principal.Source = ByteToImage(File.ReadAllBytes(openFileDialog.FileName)); 
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

        private static ImageSource ByteToImage(byte[] imageData)
        {
            BitmapImage biImg = new BitmapImage();
            MemoryStream ms = new MemoryStream(imageData);
            biImg.BeginInit();
            biImg.StreamSource = ms;
            biImg.EndInit();

            ImageSource imgSrc = biImg as ImageSource;

            return imgSrc;
        }

        private void RecargarImagenesDpto(object sender, MouseButtonEventArgs e)
        {
            Imagen imgn = new Imagen();
            List<Imagen> listadoImagenes = imgn.ObtenerImgsDeUnDpto(selectedDpto.ID_DPTO);

            if (listadoImagenes.Count < 1)
            {
                img_principal.Source = null;
                return;
            }

            foreach (var img in listadoImagenes)
            {
                /* Tengo que iterar por cada imagen, y agregarla como elemento imagen dentro del stackpanel, agregandoles
                 * el evento Image_MouseDown que haga que cambie la principal y seleccionada por la clicada, para ver mas 
                 * grande y para eliminar tambien.
                 * 
                 * TESTEAR ESTO!!
                */

                Image newImage = new Image();
                newImage.Source = ByteToImage(img.FOTO);
                newImage.MouseDown += Image_MouseDown;

                stk_imagenes.Children.Add(newImage);
            }
            img_principal.Source = (stk_imagenes.Children[0] as Image).Source;

        }

        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            // ERROR: ESTO NO ESTA PUDIENDO CAMBIAR LA IMAGEN SELECCIONADA AÚN. 

            img_principal.Source = e.Source as ImageSource;
            selectedImg = e.Source as Imagen;
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
    }
}
