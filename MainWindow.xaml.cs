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
        int numberCard;
        Cards cards;
        Dictionary<string, string> listRegulations;

        public MainWindow()
        {
            InitializeComponent();
            ParseXlsToJson.ParseAndSerializationXlsToJson();
        }

        #region EventForBtnCheckCard
        private void btnCheckCard_Click(object sender, RoutedEventArgs e)
        {
            cards = new Cards();

            try
            {
                if (inputNumberCard.Text.Length == 6 && inputNumberCard.Text != "Введите номер карты")
                {
                    numberCard = Convert.ToInt32(inputNumberCard.Text);
                    var card = SearchCardInListCards(numberCard, cards);
                    (bool, string, string) resultCompare = CompareCardWithRegulations(card, listRegulations);
                    OutputResultCompare(resultCompare);
                }
                else MessageBox.Show("Вы ввели некорректный номер карты! \nДля проверки карты необходимо ввести 6 цифр.");
                    
            }
            catch (FormatException)
            {
                MessageBox.Show("Вы ввели некорректный номер карты! \nДля проверки карты необходимо ввести 6 цифр.");
            }
        }

        #endregion

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
        private void inputValueRegulation_GotFocus(object sender, RoutedEventArgs e)
        {
            if (inputValueRegulation.Text == "Введите значение правила") inputValueRegulation.Clear();
        }
        #endregion

        #region EventForCbTypeTransaction
        private void cbTypeTransaction_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            listRegulations = new Dictionary<string, string>();
        }

        #endregion

        #region EventForBtnClearListRegulation
        private void btnClearListRegulation_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                listRegulations = new Dictionary<string, string>();
                MessageBox.Show("Список правил успешно очищен.");
            }
            catch (Exception exc)
            {
                Debug.WriteLine($"Msg: {exc.Message}");
                throw;
            }
        }
        #endregion

        #region EventForBtnAddRegulation
        private void btnAddRegulation_Click(object sender, RoutedEventArgs e)
        {
            if (listRegulations.Count == 0) listRegulations.Add("TypeTranzaction", cbTypeTransaction.Text);
            if (cbTypeTransaction.SelectedIndex > -1 &&
                cbRegulation.SelectedIndex > -1 &&
                cbEquality.SelectedIndex > -1 &&
                inputValueRegulation.Text != null &&
                inputValueRegulation.Text != "Введите значение правила")
            {
                string equality;
                if (cbEquality.Text == "Равно") equality = "=";
                else equality = "!";
                try
                {
                    listRegulations.Add(cbRegulation.Text, equality + inputValueRegulation.Text.Trim().ToUpper());
                    MessageBox.Show($"Правило успешно добавлено.");
                }
                catch (ArgumentException ArgExc)
                {
                    Debug.WriteLine($"Msg exception: {ArgExc.Message}");
                    MessageBox.Show($"Ранее вы добавили правило похожее правило. \nОбнулите список или добавьте другое правило для проверки карты.");
                }
                
            }
            else MessageBox.Show("Вы не заполнили все поля для добавления правила");
        }

        #endregion

        /// <summary>
        /// Поиск карты из общего списка карт
        /// </summary>
        /// <param name="numberCard">Номер карты указанный пользователем</param>
        /// <param name="cards">Список карт</param>
        /// <returns></returns>
        private Dictionary<string, string> SearchCardInListCards(int numberCard, Cards cards)
        {
            var cardDict = new Dictionary<string, string>();

            foreach (var card in cards.DataCards)
            {
                if (card.Bin == numberCard)
                {
                    cardDict.Add("ID", card.Id.ToString());
                    cardDict.Add("BIN", card.Bin.ToString());
                    cardDict.Add("BRAND", card.Brand);
                    cardDict.Add("BANK_NAME", card.BankName);
                    cardDict.Add("BIN_TYPE", card.BinType);
                    cardDict.Add("BIN_LEVEL", card.BinLevel);
                    cardDict.Add("ISO_COUNTRY", card.IsoCountry);
                    cardDict.Add("COUNTRY_ISO", card.CountryIso);
                    cardDict.Add("COUNTRY2_ISO", card.Country2Iso);
                    cardDict.Add("COUNTRY3_ISO", card.Country3Iso.ToString());
                    break;
                }
            }

            return cardDict;
        }

        /// <summary>
        /// Сравнение карты с заданными правилами
        /// </summary>
        /// <param name="card">Проверяемая карта</param>
        /// <param name="regulations">Список заданных правил</param>
        /// <returns></returns>
        private (bool, string, string) CompareCardWithRegulations(Dictionary<string, string> card, Dictionary<string, string> regulations)
        {
            bool flag = default(bool);
            (bool, string, string) tuple = default;

            foreach (var item in card)
            {
                foreach (var reg in regulations)
                {
                    if (item.Key == reg.Key)
                    {
                        string regValue = reg.Value.ToString();
                        string regValueSecondChar = Convert.ToString(regValue[0]);
                        string remainingKeyValue = regValue.Remove(0,1);
                        if (regValueSecondChar == "=")
                        {
                            if (item.Value == remainingKeyValue) 
                            {
                                flag = true;
                                tuple = (flag, null, null);
                            } 
                            else
                            {
                                flag = false;
                                tuple = (flag, item.Key.ToString(), reg.Value);
                                break;
                            }

                        }
                        else
                        {
                            if (item.Value != remainingKeyValue)
                            {
                                flag = true;
                                tuple = (flag, null, null);
                            }
                            else
                            {
                                flag = false;
                                tuple = (flag, item.Key.ToString(), reg.Value);
                                break;
                            }
                        }
                    }
                }
            }
            return tuple;
        }

        /// <summary>
        /// Вывод результата проверки
        /// </summary>
        /// <param name="resultCompare"></param>
        private void OutputResultCompare((bool, string, string) resultCompare)
        {
            try
            {
                if (resultCompare.Item1 == true)
                {
                    MessageBox.Show("Введаная карта успешно прошла проверку по заданным правилам.");
                }
                else
                {
                    if (resultCompare.Item3.First() == '!')
                    {
                        MessageBox.Show($"Операция {listRegulations.First().Value.ToLower()} невозможна по причине {resultCompare.Item2} равен {resultCompare.Item3.Remove(0, 1)}");
                    }
                    else
                    {
                        MessageBox.Show($"Операция {listRegulations.First().Value.ToLower()} невозможна по причине {resultCompare.Item2} не равен {resultCompare.Item3.Remove(0, 1)}");
                    }

                }
            }
            catch (ArgumentNullException ArgNullExc)
            {
                MessageBox.Show($"Карта {numberCard} не найдена. Проверьте корректность введенных данных.");
                Debug.WriteLine($"Msg: {ArgNullExc.Message}");
            }
            
        }


    }
}
