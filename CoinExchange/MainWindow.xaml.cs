using CoinExchange.Controller.Services.CoinService;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace CoinExchange
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        CoinService coinService;
        float totalToExchange;

        public MainWindow()
        {
            HttpClient client = new HttpClient();
            coinService = new CoinService(client);
            InitializeComponent();
            GetCurrenciesOnInit();
        }

        private async void GetCurrencyValueByDate(object sender, SelectionChangedEventArgs e)
        {
            string date = datePicker.SelectedDate.Value.Date.ToString("yyyy-MM-dd");
            var currencies = await coinService.GetCurrencyValueByDate(date);
            cmbBox.ItemsSource = currencies;
            targetCoinCmbBox.ItemsSource = currencies;
            AmountToExchange.Clear();
        }

        private async void GetCurrenciesOnInit()
        {
            var coins = await coinService.GetCoins();
            cmbBox.ItemsSource = coins;
            targetCoinCmbBox.ItemsSource = coins;
        }

        private void CalculateExchange(object sender, RoutedEventArgs e)
        {

            if (cmbBox.SelectedValue is null)
            {
                Label1.Content = "Converted currency wasn't selected";
                return;
            }
            if (AmountToExchange.Text.Length == 0)
            {
                Label1.Content = "Please enter amount of currency";
                return;
            }
            if (targetCoinCmbBox.SelectedValue is null)
            {
                Label1.Content = "Target currency wasn't selected";
                return;
            }
            totalToExchange = (float)cmbBox.SelectedValue *
                (float)Convert.ToDouble(AmountToExchange.Text);
            var exchangedValue = totalToExchange / (float)targetCoinCmbBox.SelectedValue;
            Label1.Content = exchangedValue.ToString();
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}