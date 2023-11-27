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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для TextPage.xaml
    /// </summary>
    public partial class TextPage : Page
    {
        private Hotel _currentHotel = new Hotel();
        public TextPage(Hotel selectedHotel)
        {
            InitializeComponent();
            if (selectedHotel != null )
                _currentHotel = selectedHotel;
            DataContext = _currentHotel;
            ComboCountries.ItemsSource = FoxTravelEntities.GetContext().Country.ToList();
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();
            if (string.IsNullOrEmpty(_currentHotel.Name))
                errors.AppendLine("Укажите название отеля");
            if (_currentHotel.CountOfStars < 1 || _currentHotel.CountOfStars > 5)
                errors.AppendLine("Количество звёзд - число от 1 до 5");
            if (_currentHotel.Country == null)
                errors.AppendLine("Выберите страну");

            if(errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            if (_currentHotel.ID == 0)
            {
                FoxTravelEntities.GetContext().Hotel.Add(_currentHotel);
            }
            try
            {
                FoxTravelEntities.GetContext().SaveChanges();
                MessageBox.Show("Информация Сохранена!");
                Manager.MainFrame.GoBack();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }

        }
    }
}
