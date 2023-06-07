using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
        Regex regex = new Regex(@"^[a-zA-Z\s-]+$");
        public PageCreateAcc()
        {
            InitializeComponent();
        }

        private void BtnCreate_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            if (AppConnect.model0db.Owner.Count(x => x.Login == login.Text) > 0)
            {
                MessageBox.Show("Пользователь с таким логином есть!",
                    "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            try
            {
                bool isValidLastName = regex.IsMatch(lastname.Text);
                if (lastname.Text == "" || isValidLastName == false)
                    errors.AppendLine("Введите корректно фамилию");
                bool isValidName = regex.IsMatch(name.Text);
                if (name.Text == "")
                    errors.AppendLine("Введите корректно имя");
                if (login.Text == "")
                    errors.AppendLine("Введите корректно логин");
                if (password.Text == "")
                    errors.AppendLine("Введите корректно пароль");
                if (passport.Text == "")
                    errors.AppendLine("Введите корректно номер и серию паспорта");

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
