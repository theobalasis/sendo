using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Sendo.UwpApp.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AdvancedLogin : Page
    {
        public AdvancedLogin()
        {
            this.InitializeComponent();
        }

        private void doLogin(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Home));
        }

        private void back(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Login));
        }
    }
}
