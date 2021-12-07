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
using System.Diagnostics;

namespace Fund_monitoring
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        NumberCard numberCard;
        Cards cards;
        public MainWindow()
        {
            InitializeComponent();
            ParseXlsToJson.ParseAndSerializationXlsToJson();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            cards = new Cards();
            try
            {
                numberCard = new NumberCard(Convert.ToInt32(inputNumberCard.Text));
            }
            catch (FormatException fExc)
            {
                Debug.WriteLine($"{fExc.Message}, {fExc.GetType()}");
                MessageBox.Show("Вы ввели некорректный номер карты!");
            }
        }
    }
}
