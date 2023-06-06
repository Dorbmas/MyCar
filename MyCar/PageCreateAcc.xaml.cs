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
    /// Логика взаимодействия для PageCreateAcc.xaml
    /// </summary>
    public partial class PageCreateAcc : Page
    {
        public Owner _currentOwner = new Owner();
        public PageCreateAcc()
        {
            InitializeComponent();
        }

        private void BtnCreate_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            if (AppConnect.model0db.Owner.Count(x => x.Login == login.Text) > 0)
            {
                MessageBox.Show("пользователь с таким логином есть!",
                    "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            

            try
            {
                if (lastname.Text == "")
                    errors.AppendLine("Введите фамилию");
                if (name.Text == "")
                    errors.AppendLine("Введите имя");
                if (login.Text == "")
                    errors.AppendLine("Введите логин");
                if (password.Text == "")
                    errors.AppendLine("Введите пароль");
                if (passport.Text == "")
                    errors.AppendLine("Введите номер и серию паспорта");

                if (errors.Length > 0)
                {
                    MessageBox.Show(errors.ToString());
                    return;
                }

                Owner ownerObj = new Owner()
                {
                    Name = name.Text,
                    Lastname = lastname.Text,
                    Passport_ID = passport.Text,
                    Login = login.Text,
                    Password = password.Text
                };             

                AppConnect.model0db.Owner.Add(ownerObj);
                AppConnect.model0db.SaveChanges();
                MessageBox.Show("Данные успешно добавлены!",
                    "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                Manager.MainFrame.GoBack();
            }
            catch
            {
                MessageBox.Show("Ошибка при добавлении данных!",
                    "Уведомление", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.GoBack();
        }

        private void PassworBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (secondpassword.Password != password.Text)
            {
                BtnCreate.IsEnabled = false;
                secondpassword.Background = Brushes.LightCoral;
                secondpassword.BorderBrush = Brushes.Red;
            }
            else
            {
                BtnCreate.IsEnabled = true;
                secondpassword.Background = Brushes.LightGreen;
                secondpassword.BorderBrush = Brushes.Green;
            }
        }
    }
}
