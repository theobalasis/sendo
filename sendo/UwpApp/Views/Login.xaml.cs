using System;
using System.Diagnostics;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Sendo.UwpApp.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Login : Page
    {
        public Login()
        {
            this.InitializeComponent();
        }

        private void doLogin(object sender, RoutedEventArgs e)
        {
            String un = EmailField.Text;
            String pw = PasswordField.Password;
            Debug.WriteLine(un + ":" + pw);
            Frame.Navigate(typeof(Home));
        }

        private void toAdvanced(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(AdvancedLogin));
        }

        private void SignInLabel_SelectionChanged(object sender, RoutedEventArgs e)
        {

        }
    }
}
