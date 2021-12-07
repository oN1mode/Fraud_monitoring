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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Fund_monitoring
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Cards cards;
        public MainWindow()
        {
            InitializeComponent();
            ParseXlsToJson.Start();
            try
            {
                cards = new Cards();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                throw;
            }
            
            
        }
    }
}
