using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для TourCr.xaml
    /// </summary>
    public partial class TourCr : Page
    {

        byte[] im;

        private Tour _currentTour = new Tour();
        bool imgChange = false;
        public TourCr(Tour selectedTour)
        {
            InitializeComponent();
            if (selectedTour != null)
                _currentTour = selectedTour;
            DataContext = _currentTour;
            
        }

        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image (*.png;*.jpeg;*.jpg)|*.png;*.jpeg;*.jpg|All files (*.*)|*.*";
            if(ofd.ShowDialog() == true)
            {  
                im = File.ReadAllBytes(ofd.FileName);
                imnn.Source = new BitmapImage(new Uri(ofd.FileName));
                imgChange = true;
            }
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            StringBuilder errors = new StringBuilder();
            if (string.IsNullOrEmpty(_currentTour.Name))
                errors.AppendLine("Укажите название отеля");
            if (_currentTour.TicketsCount < 0)
                errors.AppendLine("Число билетов не может быть отрицательным");
            if (_currentTour.Price < 0)
                errors.AppendLine("Цена не может быть отрицательной");
            if(_currentTour.ID == 0 || imgChange == true)
            {
                _currentTour.ImagePreveiw = im;
            }
            

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            if (_currentTour.ID == 0)
            {
                FoxTravelEntities.GetContext().Tour.Add(_currentTour);
            }
            try
            {
                FoxTravelEntities.GetContext().SaveChanges();
                MessageBox.Show("Информация Сохранена!");
                Manager.MainFrame.GoBack();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }


        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if(imnn.Source == null)
            {
                imnn.Source = new BitmapImage(new Uri("/Resources/Photo.png", UriKind.Relative));
            }
            imgChange = false;
        }
    }
}
