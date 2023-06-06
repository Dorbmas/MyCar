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
    /// Логика взаимодействия для CarMorePage.xaml
    /// </summary>
    public partial class CarMorePage : Page
    {
        private Cars _currentCar = new Cars();
        public CarMorePage(Cars selectedCar)
        {
            InitializeComponent();

            if (selectedCar != null)
                _currentCar = selectedCar;

            DataContext = _currentCar;
        }
    }
}
