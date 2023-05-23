using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
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
    /// Логика взаимодействия для PageLogin.xaml
    /// </summary>
    public partial class PageLogin : Page
    {
        public PageLogin()
        {
            InitializeComponent();
           
        }

        //public delegate void TextValueChangedHandler(int selectedId);
        //public event TextValueChangedHandler TextValueChanged;

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int selectedId = 0;
                var ownerObj = AppConnect.model0db.Owner.FirstOrDefault(x => x.Login == login.Text && x.Password == password.Password);
                if (ownerObj == null)
                {
                    MessageBox.Show("Такого пользователя нет!", "Ошибка при авторизации!",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    MessageBox.Show("Добро пожаловать, " + ownerObj.Name + "!",
                        "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);

                    string textLogin = login.Text;
                    var currentOwner = CarEntities3.GetContext().Owner.ToList();
                    currentOwner = currentOwner.Where(x => x.Login == login.Text).ToList();
                    if (currentOwner.Count > 0)
                    {
                        selectedId = currentOwner[0].Id;
                    }
                                       
                    MainWindow mainWindow = new MainWindow(selectedId);
                    mainWindow.Show();                               
                    Window.GetWindow(this).Close();
                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка" + ex.Message.ToString() + "Критическая работа приложения!",
                    "Уведомление", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            
        }

        private void BtnRegistration_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new PageCreateAcc());
        }
    }
}
