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
    /// Логика взаимодействия для EditOwnerPage.xaml
    /// </summary>
    public partial class EditOwnerPage : Page
    {
        Regex regex = new Regex(@"^[а-яА-Я\s-]+$");
        Regex regex1 = new Regex(@"^[0-9\s-]+$");
        Owner _currentOwner = new Owner();
        public EditOwnerPage(Owner selectedOwner, int Id)
        {
            InitializeComponent();

            if (selectedOwner != null)
                _currentOwner = selectedOwner;                     

            var currentOwners = CarEntities3.GetContext().Owner.ToList();
            currentOwners = currentOwners.Where(c => c.Id == Id).ToList();

            _currentOwner = currentOwners.FirstOrDefault();

            DataContext = _currentOwner;
            
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();
            bool isValidName = regex.IsMatch(TextName.Text);
            if (string.IsNullOrWhiteSpace(_currentOwner.Name) || isValidName == false)
                errors.AppendLine("Введите корректно имя");
            bool isValidLastName = regex.IsMatch(TextLastname.Text);
            if (string.IsNullOrWhiteSpace(_currentOwner.Lastname) || isValidLastName == false)
                errors.AppendLine("Введите корректно фамилию");
            if (string.IsNullOrWhiteSpace(_currentOwner.Login))
                errors.AppendLine("Введите логин");
            if (string.IsNullOrWhiteSpace(_currentOwner.Password))
                errors.AppendLine("Введите пароль");
            bool isValidPassport_ID = regex1.IsMatch(TextPassport_ID.Text);
            if (string.IsNullOrWhiteSpace(_currentOwner.Lastname) || isValidPassport_ID == false)
                errors.AppendLine("Введите корректно номер и серию паспорта");

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }
           
            if (_currentOwner.Id == 0)
                CarEntities3.GetContext().Owner.Add(_currentOwner);

            try
            {
                CarEntities3.GetContext().SaveChanges();
                MessageBox.Show("Информация сохранена!");
                Manager.MainFrame.GoBack();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
    }
}
