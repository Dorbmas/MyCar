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
        
        private Cars _currentCar = new Cars();
        private Owner _currentOwner = new Owner();
        private PageLogin _currentLogin = new PageLogin();

        public CarPage()
        {
            InitializeComponent();
            var currentCars = CarEntities.GetContext().Cars.ToList();
            DataContext = _currentCar;
            


          
            ListViewCar.ItemsSource = currentCars;
            

        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new CarEditPage((sender as Button).DataContext as Cars));
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new CarEditPage(null));
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            var carsForRemoving = ListViewCar.SelectedItems.Cast<Cars>().ToList();

            if (MessageBox.Show($"ВЫ точно хотите удалить следующие {carsForRemoving.Count()} элеметов?", "Внимание",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    CarEntities.GetContext().Cars.RemoveRange(carsForRemoving);
                    CarEntities.GetContext().SaveChanges();
                    MessageBox.Show("Данные удалены!");

                    ListViewCar.ItemsSource = CarEntities.GetContext().Cars.ToList();
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
                CarEntities.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                ListViewCar.ItemsSource = CarEntities.GetContext().Cars.ToList();
            }
        }
    }
}
