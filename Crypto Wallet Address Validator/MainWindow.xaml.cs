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
using System.Net;
using System.Text.Json;
using System.Threading;

namespace Crypto_Wallet_Address_Validator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public class Result
    {
        public string address { get; set; }
        public string currency { get; set; }
        public string network { get; set; }
        public bool isValid { get; set; }
    }

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            ServicePointManager.Expect100Continue = true;

            string[] Coins = new string[]
            {
                "BTC", "BCH", "BTG", "BTCP", "BTCZ", "DASH", "DGB", "DOGE", "ETH", "ETC", "ETZ", "HUSH",
                "LTC", "XMR", "NMC", "NANO", "NEO", "XPM", "QTUM", "XRB", "XRP", "VTC", "ZEC", "ZCL", "ZEN"
            };
            ComboCoins.ItemsSource = Coins;
        }

        private void TextAddress_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextAddress.Text = ((TextBox)TextAddress.Template.FindName("TextChild", TextAddress)).Text;
        }

        private void ButtonExit_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void ButtonCheck_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(TextAddress.Text) && !string.IsNullOrEmpty(ComboCoins.Text))
            {
                string ResultJSONFormat = RapidAPI.ValidateAddress(TextAddress.Text, ComboCoins.Text);
                Result CheckResult = JsonSerializer.Deserialize<Result>(ResultJSONFormat);
                if (CheckResult.isValid == true)
                {
                    LabelResult.Foreground = new SolidColorBrush(Color.FromRgb(97, 196, 165));
                    LabelResult.Content = "Wallet Address Syntax Looks Valid.";
                    LabelResult.Visibility = Visibility.Visible;
                }
                else
                {
                    LabelResult.Foreground = new SolidColorBrush(Color.FromRgb(238, 82, 83));
                    LabelResult.Content = "Wallet Address Syntax Looks Invalid.";
                    LabelResult.Visibility = Visibility.Visible;
                }
            }
        }
    }
}
