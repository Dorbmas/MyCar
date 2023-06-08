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

namespace MyCar
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int selectedId1;
        public MainWindow(int selectedId)
        {
            InitializeComponent();
            selectedId1 = selectedId;
            MainFrame.Navigate(new CarPage(selectedId1));
            Manager.MainFrame = MainFrame;                     
        }

        public object PageLogin { get; internal set; }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.GoBack();
        }

        private void MainFrame_ContentRendered(object sender, EventArgs e)
        {
            if (MainFrame.CanGoBack)
            {
                BtnBack.Visibility = Visibility.Visible;
                BtnEditOwner.Visibility = Visibility.Hidden;
            }
            else
            {
                BtnBack.Visibility= Visibility.Hidden;
                BtnEditOwner.Visibility = Visibility.Visible;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            CarPage carPage = new CarPage(selectedId1);
            MainFrame.Navigate(carPage);
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            
        }

        private void BtnEditOwner_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new EditOwnerPage((sender as Button).DataContext as Owner, selectedId1));
        }
    }
}
