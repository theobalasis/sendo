using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Sendo.UwpApp.Views
{
    public sealed partial class AddContact : Page
    {
        public AddContact()
        {
            this.InitializeComponent();
        }

        private async void add(object sender, RoutedEventArgs e)
        {
            NewContact c = new NewContact();
            c.Firstname = Fname.Text;
            c.Middlename = Mname.Text;
            c.Lastname = Lname.Text;
            c.Email = EmailT.Text;
            if (Gender.SelectedIndex == 0)
            {
                c.Gender = 'M';
            }
            else
            {
                c.Gender = 'F';
            }
            c.DateofBirth = Dob.SelectedDate.Value.ToString("yyyy-MM-dd");
            string json = JsonConvert.SerializeObject(c, Formatting.Indented);

            var savePicker = new Windows.Storage.Pickers.FileSavePicker();
            savePicker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.DocumentsLibrary;
            savePicker.FileTypeChoices.Add("Json", new List<string>() { ".json" });
            savePicker.SuggestedFileName = "Contact";
            Windows.Storage.StorageFile file = await savePicker.PickSaveFileAsync();
            if (file != null)
            {
                Windows.Storage.CachedFileManager.DeferUpdates(file);
                await Windows.Storage.FileIO.WriteTextAsync(file, json);
                Windows.Storage.Provider.FileUpdateStatus status = await Windows.Storage.CachedFileManager.CompleteUpdatesAsync(file);
                if (status == Windows.Storage.Provider.FileUpdateStatus.Complete)
                {
                    json = "File " + file.Name + " was saved.";
                }
                else
                {
                    json = "File " + file.Name + " couldn't be saved.";
                }
            }
            else
            {
                json = "Operation cancelled.";
            }
        }

        public class NewContact
        {
            public string Firstname { get; set; }
            public string Middlename { get; set; }
            public string Lastname { get; set; }
            public string Email { get; set; }
            public char Gender { get; set; }
            public string DateofBirth { get; set; }
        }
    }
}
