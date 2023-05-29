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
        public string path = System.IO.Path.Combine(Directory.GetParent(System.IO.Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).FullName)).FullName, @"Resources\");
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

            //if (string.IsNullOrWhiteSpace(_currentCar.Model))
            //    errors.AppendLine("Укажите модель");
            //if (string.IsNullOrWhiteSpace(_currentCar.Mark))
            //    errors.AppendLine("Укажите марку");
            //if (string.IsNullOrWhiteSpace(_currentCar.Color))
            //    errors.AppendLine("Укажите цвет");

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }          

            if (_currentCar.Id == 0)
                CarEntities3.GetContext().Cars.Add(_currentCar);

            //try
            {
                CarEntities3.GetContext().SaveChanges();
                MessageBox.Show("Информация сохранена!");
            }
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message.ToString());
            //}
        }

        private void MouseLeftButtonUp_Click(object sender, MouseButtonEventArgs e)
        {
            //OpenFileDialog ofd = new OpenFileDialog();
            //ofd.Filter = "Image (*.png, *.jpg, *.jpeg) |*.png; *.jpg; *.jpeg";
            //if (ofd.ShowDialog() == true)
            //{
            //    _mainImageData = File.ReadAllBytes(ofd.FileName);
            //    ofd.Multiselect = false;
            //    PhotoService.Source = new ImageSourceConverter()
            //        .ConvertFrom(_mainImageData) as ImageSource;                
            //    string photoName = System.IO.Path.GetFileName(ofd.FileName);
            //    _currentCar.Photo = photoName;
            //    path += photoName;
            //    File.Copy(ofd.FileName, path);
            //}
            
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
