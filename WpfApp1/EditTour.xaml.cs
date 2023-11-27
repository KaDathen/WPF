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
    /// Логика взаимодействия для EditTour.xaml
    /// </summary>
    public partial class EditTour : Page
    {
        public EditTour()
        {
            InitializeComponent();
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            FoxTravelEntities.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
            DGridHotels.ItemsSource = FoxTravelEntities.GetContext().Tour.ToList();
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new TourCr(null));
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new TourCr((sender as Button).DataContext as Tour));
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            var TourForRemoving = DGridHotels.SelectedItems.Cast<Tour>().ToList();

            if (MessageBox.Show($"Вы точно хотите удалить слудующие {TourForRemoving.Count()} элементов", "Внимание",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    FoxTravelEntities.GetContext().Tour.RemoveRange(TourForRemoving);
                    FoxTravelEntities.GetContext().SaveChanges();
                    MessageBox.Show("Данные удалены");

                    DGridHotels.ItemsSource = FoxTravelEntities.GetContext().Tour.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }
    }
}
