﻿using System;
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
    /// Логика взаимодействия для HomePage.xaml
    /// </summary>
    public partial class HomePage : Page
    {
        public HomePage()
        {
            InitializeComponent();
            
            //DGridHotels.ItemsSource = FoxTravelEntities.GetContext().Hotel.ToList();
        }
        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new TextPage((sender as Button).DataContext as Hotel));
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new TextPage(null));
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            var hotelsForRemoving = DGridHotels.SelectedItems.Cast<Hotel>().ToList();

            if(MessageBox.Show($"Вы точно хотите удалить слудующие {hotelsForRemoving.Count()} элементов","Внимание",
                MessageBoxButton.YesNo,MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    FoxTravelEntities.GetContext().Hotel.RemoveRange(hotelsForRemoving);
                    FoxTravelEntities.GetContext().SaveChanges();
                    MessageBox.Show("Данные удалены");

                    DGridHotels.ItemsSource = FoxTravelEntities.GetContext().Hotel.ToList();
                }
                catch(Exception ex) 
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                FoxTravelEntities.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                DGridHotels.ItemsSource = FoxTravelEntities.GetContext().Hotel.ToList();
            }
        }
    }
}
