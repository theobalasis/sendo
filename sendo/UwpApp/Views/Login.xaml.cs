using sendo.Views;
using Sendo.Helpers;
using Sendo.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Sendo.Views
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
            String un = email.Text;
            String pw = passwd.Password;
            Debug.WriteLine(un+":"+pw);
            Frame.Navigate(typeof(Home));
        }

        private void toAdvanced(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(AdvancedLogin));
        }
    }
}
