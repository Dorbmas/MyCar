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

namespace MyCar
{
    /// <summary>
    /// Логика взаимодействия для CarEditPage.xaml
    /// </summary>
    public partial class CarEditPage : Page
    {
        private byte[] _mainImageData = null;
        public string img = "emptycar.png";
        public string path = System.IO.Path.Combine(Directory.GetParent(System.IO.Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).FullName)).FullName, @"Resources\");
        public string selectedFileName;
        public string extension = ".jpg";
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

        private void MouseLeftButtonUp_Click(object sender, MouseButtonEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Файлы изображения (*.jpg, *.jpeg, *.png) | *.jpg; *.jpeg; *.png";

            if (openFileDialog.ShowDialog() == true)
            {
                string filePath = openFileDialog.FileName;
                string fileName = System.IO.Path.GetFileName(filePath);

                SaveFileNameToDatabase(fileName);

                string destinationPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);
                File.Copy(filePath, destinationPath, true);

                BitmapImage bitmap = new BitmapImage(new Uri(destinationPath));
                PhotoService.Source = bitmap;
            } 
        }

        private void SaveFileNameToDatabase(string fileName)
        {
            string connectionString = "Data Source=(localdb)\\MSSQLLocalDB; Initial Catalog=MyCar; User ID=root; Password=";
            string query = "INSERT INTO Cars (Photo) VALUES (@FileName)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@FileName", fileName);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        private void BtnAddPhoto_Click(object sender, RoutedEventArgs e)
        {          
            
        }
    }
}
