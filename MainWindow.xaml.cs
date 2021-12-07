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
            if (inputNumberCard.Text.Length == 6) numberCard = new NumberCard(Convert.ToInt32(inputNumberCard.Text));
            else MessageBox.Show("Вы ввели некорректный номер карты! \nДля проверки карты необходимо ввести 6 цифр.");
        }

        #region EventForInputNumberCard
        private void inputNumberCard_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            inputNumberCard.Clear();
        }
        private void inputNumberCard_GotFocus(object sender, RoutedEventArgs e)
        {
            if (inputNumberCard.Text == "Введите номер карты") inputNumberCard.Clear();
        }
        #endregion

        #region EventForInputValueRegulation
        private void inputValueRegulation_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            inputValueRegulation.Clear();
        }
        private void inputValueRegulation_GotFocus_1(object sender, RoutedEventArgs e)
        {
            if (inputValueRegulation.Text == "Введите значение правила") inputValueRegulation.Clear();
        }
        #endregion

    }
}
