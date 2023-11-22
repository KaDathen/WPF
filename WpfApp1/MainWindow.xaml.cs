using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
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
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
             MainFrame.Navigate(new HomePage());
            Manager.MainFrame = MainFrame;

            ImportTours();
        }

        private void ImportTours()
        {
            var fileData = File.ReadAllLines(@"C:\Users\KaDathen\source\repos\WPF\WpfApp1\bin\Туры.txt");
            var images = Directory.GetFiles(@"C:\Users\KaDathen\source\repos\WPF\WpfApp1\bin\Туры фото");

            foreach ( var line in fileData ) 
            {
                var data = line.Split('\t');

                var tempTour = new Tour
                {
                    Name = data[0].Replace("\"",""),
                    TicketsCount = int.Parse(data[2]),
                    Price = decimal.Parse(data[3]), 
                    IsActual = (data[4] == "0") ? false: true

                };

                foreach(var tourType in data[5].Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries))
                {
                    var currentType = FoxTravelEntities.GetContext().Type.ToList().FirstOrDefault(p => p.Name == tourType);
                    if (currentType != null) 
                        tempTour.Type.Add(currentType);
                }

                try
                {
                     tempTour.ImagePreveiw = File.ReadAllBytes(images.FirstOrDefault(p => p.Contains(tempTour.Name)));
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                FoxTravelEntities.GetContext().Tour.Add(tempTour);
                FoxTravelEntities.GetContext().SaveChanges(); 
            }
        }

        private void Btnback_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.GoBack();
        }

        private void MainFrame_ContentRendered(object sender, EventArgs e)
        {
            if (MainFrame.CanGoBack)
            {
                Btnback.Visibility = Visibility.Visible;
            }
            else
            {
                Btnback.Visibility= Visibility.Hidden;
            }
        }

    }
}
