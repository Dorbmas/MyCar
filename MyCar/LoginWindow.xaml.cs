﻿using System;
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
using System.Windows.Shapes;

namespace MyCar
{
    /// <summary>
    /// Логика взаимодействия для LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
            AppConnect.model0db = new CarEntities();
            Manager.MainFrame = FrmMain;

            FrmMain.Navigate(new PageLogin());
            
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            PageLogin pageLogin = new PageLogin();
           // pageLogin.TextValueChanged += pageLogin_TextValueChanged;
            FrmMain.Navigate(pageLogin);
        }

        private void pageLogin_TextValueChanged(int selectedId)
        {
            MainWindow mainWindow = new MainWindow(selectedId);
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            
        }
    }
}
