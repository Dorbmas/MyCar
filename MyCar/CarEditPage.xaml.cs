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
    /// Логика взаимодействия для CarEditPage.xaml
    /// </summary>
    public partial class CarEditPage : Page
    {
        private Cars _currentCar = new Cars();
        int selectedId;
        public CarEditPage(Cars selectedCar, int Id)
        {
            InitializeComponent();

            if (selectedCar != null)
                _currentCar = selectedCar;

            _currentCar.OwnerId = Id;

            DataContext = _currentCar;           
        }
        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            if (string.IsNullOrWhiteSpace(_currentCar.Model))
                errors.AppendLine("Укажите модель");
            if (string.IsNullOrWhiteSpace(_currentCar.Mark))
                errors.AppendLine("Укажите марку");
            if (string.IsNullOrWhiteSpace(_currentCar.Color))
                errors.AppendLine("Укажите цвет");

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }          

            if (_currentCar.Id == 0)
                CarEntities.GetContext().Cars.Add(_currentCar);

            try
            {
                CarEntities.GetContext().SaveChanges();
                MessageBox.Show("Информация сохранена!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
    }
}
