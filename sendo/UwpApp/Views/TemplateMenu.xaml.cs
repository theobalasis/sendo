using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Sendo.UwpApp.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MailTemplates : Page
    {
        public MailTemplates()
        {
            this.InitializeComponent();
        }
        private void createTemplate(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Editor));
        }
    }
}
