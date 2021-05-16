using Sendo.UwpApp.Services;
using System;
using System.Diagnostics;
using Windows.Storage;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace Sendo.UwpApp.Views
{
    /// <summary>
    /// Handles logging in to the app.
    /// </summary>
    public sealed partial class Login : Page
    {
        private readonly HttpService _httpService;

        public Login()
        {
            this.InitializeComponent();
            _httpService = new HttpService();
        }

        private async void SimpleLogin(object sender, RoutedEventArgs e)
        {
            String email = EmailField.Text;
            String password = PasswordField.Password;

            var response = await _httpService.PostAsync("authentication", $"{{ \"MailAddress\":\"{email}\", \"Password\":\"{password}\" }}");
            if (response.IsSuccessful)
            {
                ApplicationData.Current.LocalSettings.Values["SessionToken"] = response.Content;
                Frame.Navigate(typeof(ShellPage));
            }
            else
            {
                EmailField.BorderBrush = new SolidColorBrush(Colors.Red);
                PasswordField.BorderBrush = new SolidColorBrush(Colors.Red);
            }
        }

        private void AdvancedLogin(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(AdvancedLogin));
        }

        private void SignInLabel_SelectionChanged(object sender, RoutedEventArgs e)
        {

        }
    }
}
