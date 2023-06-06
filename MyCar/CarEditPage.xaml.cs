using Microsoft.Win32;
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
using System.IO;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.Data;

namespace MyCar
{
    /// <summary>
    /// Логика взаимодействия для CarEditPage.xaml
    /// </summary>
    public partial class CarEditPage : Page
    {
        byte[] imageData;
        byte[] imageDataNull;
        private Cars _currentCar = new Cars();       
        public CarEditPage(Cars selectedCar, int Id)
        {
            InitializeComponent();

            if (selectedCar != null)
                _currentCar = selectedCar;

            _currentCar.OwnerId = Id;

            DataContext = _currentCar;

            ComboMarks.ItemsSource = CarEntities3.GetContext().Marks.ToList();
            ComboModels.ItemsSource = CarEntities3.GetContext().Models.ToList();
            ComboColors.ItemsSource = CarEntities3.GetContext().Colors.ToList();
            ComboTransmissionTypes.ItemsSource = CarEntities3.GetContext().TransmissionTypes.ToList();
        }
        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            if (_currentCar.Marks == null)
                errors.AppendLine("Выбериет марку");
            if (_currentCar.Models == null)
                errors.AppendLine("Выбериет модель");
            if (_currentCar.Colors == null)
                errors.AppendLine("Выбериет цвет");
            if (_currentCar.TransmissionTypes == null)
                errors.AppendLine("Выбериет тип трансмиссии");
            if (string.IsNullOrWhiteSpace(_currentCar.VIN))
                errors.AppendLine("Введите VIN номер");
            if (string.IsNullOrWhiteSpace(_currentCar.Mileage))
                errors.AppendLine("Введите пробег");
            if (_currentCar.Horsepower <= 0)
                errors.AppendLine("Введите кол-во лошадиных сил");
            if (_currentCar.YearOfManufacture <=1900)
                errors.AppendLine("Введите верный год производства");
            if (string.IsNullOrWhiteSpace(_currentCar.RegisterSign))
                errors.AppendLine("Введите номерной знак");           

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }          

            if (_currentCar.Photo == null)
            {
                imageDataNull = File.ReadAllBytes("emptycar.png");               

                _currentCar.Photo = imageDataNull;
            }

            if (_currentCar.Id == 0)
                CarEntities3.GetContext().Cars.Add(_currentCar);

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

        private void MouseLeftButtonUp_Click(object sender, MouseButtonEventArgs e)
        {
                       
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image (*.png, *.jpg, *.jpeg) |*.png; *.jpg; *.jpeg";
            openFileDialog.Multiselect = false;
         
            if (openFileDialog.ShowDialog() == true)
            {
                imageData = File.ReadAllBytes(openFileDialog.FileName);
                PhotoService.Source = new ImageSourceConverter()
                    .ConvertFrom(imageData) as ImageSource;
                
                _currentCar.Photo = imageData;
            }
            
        }        

        private void BtnAddPhoto_Click(object sender, RoutedEventArgs e)
        {          
            
        }
    }
}
