using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Sendo.UwpApp.Views
{
    public sealed partial class MailTemplates : Page
    {
        public MailTemplates()
        {
            this.InitializeComponent();
        }

        private void CreateTemplate(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Editor));
        }
    }
}
