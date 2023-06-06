using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace MyCar
{
    /// <summary>
    /// Логика взаимодействия для CarPage.xaml
    /// </summary>
    public partial class CarPage : Page
    {
        int selectedId = 0;
        public CarPage(int Id)
        {
            InitializeComponent();
            selectedId = Id;           
            UpdateCars(Id);
            
        }

        private void UpdateCars(int Id)
        {          
            var currentCars = CarEntities3.GetContext().Cars.ToList();
            currentCars = currentCars.Where(c => c.OwnerId == Id).ToList();
            ListViewCar.ItemsSource = currentCars;
        }

        private void TBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new CarEditPage((sender as Button).DataContext as Cars, selectedId));
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new CarEditPage(null, selectedId));
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            var carsForRemoving = ListViewCar.SelectedItems.Cast<Cars>().ToList();

            if (MessageBox.Show($"ВЫ точно хотите удалить следующие {carsForRemoving.Count()} элеметов?", "Внимание",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    CarEntities3.GetContext().Cars.RemoveRange(carsForRemoving);
                    CarEntities3.GetContext().SaveChanges();
                    MessageBox.Show("Данные удалены!");

                    var currentCars = CarEntities3.GetContext().Cars.ToList();
                    currentCars = currentCars.Where(c => c.OwnerId == selectedId).ToList();
                    ListViewCar.ItemsSource = currentCars;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        private void Page_IsVisibileChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                CarEntities3.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                ListViewCar.ItemsSource = CarEntities3.GetContext().Cars.ToList();
            }
        }

        private void onLoad(object sender, RoutedEventArgs e)
        {
            UpdateCars(selectedId);
        }

        private void BtnExit_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();
            Window.GetWindow(this).Close();
        }

        private void BtnMore_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new CarMorePage((sender as Button).DataContext as Cars));
        }
    }
}
