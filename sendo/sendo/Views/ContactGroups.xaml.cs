using System;
using System.Collections.Generic;
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

namespace sendo.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ContactGroup : Page
    {
        public ContactGroup()
        {
            this.InitializeComponent();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            List<Contacts> listOfContacts = new List<Contacts>();
            listOfContacts.Add(new Contacts ("Akis","Manios" ,"Sendo"));
            listOfContacts.Add(new Contacts("Christy", "Lucyna", "Apple"));
            listOfContacts.Add(new Contacts("Irena", "Ayyub", "Microsoft"));
            Contactgroups.ItemsSource = listOfContacts;
        }

    }


}
