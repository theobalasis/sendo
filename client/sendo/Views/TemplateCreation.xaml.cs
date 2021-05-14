using Newtonsoft.Json.Linq;
using sendo.Core.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace sendo.Views
{
    public sealed partial class Editor : Page, INotifyPropertyChanged
    {
        public Editor()
        {
            InitializeComponent();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void Set<T>(ref T storage, T value, [CallerMemberName]string propertyName = null)
        {
            if (Equals(storage, value))
            {
                return;
            }

            storage = value;
            OnPropertyChanged(propertyName);
        }

        private void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        private void AddVar(object sender, ItemClickEventArgs e)
        {
            var selection = editor.Document.Selection;
            String replace = "<"+e.ClickedItem.ToString()+">";
            selection.SetText(Windows.UI.Text.TextSetOptions.None, replace );
            selection.StartPosition = selection.StoryLength;
        }
        private async void OpenButton_Click(object sender, RoutedEventArgs e)
        {
            // Open a text file.
            Windows.Storage.Pickers.FileOpenPicker open = new Windows.Storage.Pickers.FileOpenPicker();
            open.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.DocumentsLibrary;
            open.FileTypeFilter.Add(".rtf");
            Windows.Storage.StorageFile file = await open.PickSingleFileAsync();

            if (file != null)
            {
                try
                {
                    Windows.Storage.Streams.IRandomAccessStream randAccStream =
                await file.OpenAsync(Windows.Storage.FileAccessMode.Read);

                    // Load the file into the Document property of the RichEditBox.
                    editor.Document.LoadFromStream(Windows.UI.Text.TextSetOptions.FormatRtf, randAccStream);
                }
                catch (Exception)
                {
                    ContentDialog errorDialog = new ContentDialog()
                    {
                        Title = "File open error",
                        Content = "Sorry, I couldn't open the file.",
                        PrimaryButtonText = "Ok"
                    };

                    await errorDialog.ShowAsync();
                }
            }
        }

        private async void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            Windows.Storage.Pickers.FileSavePicker savePicker = new Windows.Storage.Pickers.FileSavePicker();
            savePicker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.DocumentsLibrary;

            // Dropdown of file types the user can save the file as
            savePicker.FileTypeChoices.Add("Rich Text", new List<string>() { ".rtf" });

            // Default file name if the user does not type one in or select a file to replace
            savePicker.SuggestedFileName = "New Document";

            Windows.Storage.StorageFile file = await savePicker.PickSaveFileAsync();
            if (file != null)
            {
                // Prevent updates to the remote version of the file until we
                // finish making changes and call CompleteUpdatesAsync.
                Windows.Storage.CachedFileManager.DeferUpdates(file);
                // write to file
                Windows.Storage.Streams.IRandomAccessStream randAccStream = await file.OpenAsync(Windows.Storage.FileAccessMode.ReadWrite);

                editor.Document.SaveToStream(Windows.UI.Text.TextGetOptions.FormatRtf, randAccStream);

                // Let Windows know that we're finished changing the file so the
                // other app can update the remote version of the file.
                Windows.Storage.Provider.FileUpdateStatus status = await Windows.Storage.CachedFileManager.CompleteUpdatesAsync(file);
                if (status != Windows.Storage.Provider.FileUpdateStatus.Complete)
                {
                    Windows.UI.Popups.MessageDialog errorBox = new Windows.UI.Popups.MessageDialog("File " + file.Name + " couldn't be saved.");
                    await errorBox.ShowAsync();
                }
            }
        }

        private void BoldButton_Click(object sender, RoutedEventArgs e)
        {
            Windows.UI.Text.ITextSelection selectedText = editor.Document.Selection;
            if (selectedText != null)
            {
                Windows.UI.Text.ITextCharacterFormat charFormatting = selectedText.CharacterFormat;
                charFormatting.Bold = Windows.UI.Text.FormatEffect.Toggle;
                selectedText.CharacterFormat = charFormatting;
            }
        }

        private void ItalicButton_Click(object sender, RoutedEventArgs e)
        {
            Windows.UI.Text.ITextSelection selectedText = editor.Document.Selection;
            if (selectedText != null)
            {
                Windows.UI.Text.ITextCharacterFormat charFormatting = selectedText.CharacterFormat;
                charFormatting.Italic = Windows.UI.Text.FormatEffect.Toggle;
                selectedText.CharacterFormat = charFormatting;
            }
        }

        private void UnderlineButton_Click(object sender, RoutedEventArgs e)
        {
            Windows.UI.Text.ITextSelection selectedText = editor.Document.Selection;
            if (selectedText != null)
            {
                Windows.UI.Text.ITextCharacterFormat charFormatting = selectedText.CharacterFormat;
                if (charFormatting.Underline == Windows.UI.Text.UnderlineType.None)
                {
                    charFormatting.Underline = Windows.UI.Text.UnderlineType.Single;
                }
                else
                {
                    charFormatting.Underline = Windows.UI.Text.UnderlineType.None;
                }
                selectedText.CharacterFormat = charFormatting;
            }
        }

        private async void fetchitems(object sender, RoutedEventArgs e)
        {
            VariableList.Items.Clear();
            HttpDataService url =new HttpDataService(ApplicationData.Current.LocalSettings.Values["ServerUrl"] as String);
            String uri = ApplicationData.Current.LocalSettings.Values["ServerUri"] as String;
            JObject json = await url.GetAsync<JObject>(uri, null,true);
            //var raw = await HttpDataService.Main();
            //JObject json = JObject.Parse(raw);
            Varlistadd(json);
        }

        private void Varlistadd(JObject json) {
            foreach (var pair in json)
            {
                if (pair.Value.HasValues == true)
                {
                    Varlistadd((JObject)pair.Value);
                }
                else
                {
                    VariableList.Items.Add(pair.Key);
                }
            }

        }
        private void EditorLostFocus(object sender, RoutedEventArgs e)
        {
            editor.Focus(FocusState.Programmatic);
        }

        private async void SaveComp_Click(object sender, RoutedEventArgs e)
        {
            var patern = "<.+>";
            String editortext = string.Empty;
            editor.Document.GetText(Windows.UI.Text.TextGetOptions.FormatRtf, out editortext);
            //var raw = await HttpDataService.Main();
            //JObject json = JObject.Parse(raw);
            HttpDataService url = new HttpDataService("https://jsonplaceholder.typicode.com");
            JObject json = await url.GetAsync<JObject>("users/1", null, true);
            var comp = Varsplit(editortext , patern, json);
            Windows.Storage.Pickers.FileSavePicker savePicker = new Windows.Storage.Pickers.FileSavePicker();
            savePicker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.DocumentsLibrary;

            // Dropdown of file types the user can save the file as
            savePicker.FileTypeChoices.Add("Rich Text", new List<string>() { ".rtf" });

            // Default file name if the user does not type one in or select a file to replace
            savePicker.SuggestedFileName = "New Document";

            Windows.Storage.StorageFile file = await savePicker.PickSaveFileAsync();
            if (file != null)
            {
                await FileIO.WriteTextAsync(file, comp);

                // Let Windows know that we're finished changing the file so the
                // other app can update the remote version of the file.
                Windows.Storage.Provider.FileUpdateStatus status = await Windows.Storage.CachedFileManager.CompleteUpdatesAsync(file);
                if (status != Windows.Storage.Provider.FileUpdateStatus.Complete)
                {
                    Windows.UI.Popups.MessageDialog errorBox =
                        new Windows.UI.Popups.MessageDialog("File " + file.Name + " couldn't be saved.");
                    await errorBox.ShowAsync();
                }
            }
        }
        private String Varsplit(String crude,string replacer, JObject data ) {
            
            string comp = String.Empty;
            string oldvalue = String.Empty;
            string newvalue = String.Empty;
            foreach (var pair in data)
            {
                if (pair.Value.HasValues == true)
                {
                   comp=Varsplit(crude, replacer, (JObject)pair.Value);
                   crude = comp;
                }
                else { 
                oldvalue = "<" + pair.Key + ">";
                newvalue = "" + pair.Value + "";
                //Debug.WriteLine(oldvalue);
                //Debug.WriteLine(newvalue);
                comp = crude.Replace(oldvalue, newvalue);
                crude = comp;
                }
            }
                return comp;
        }
    }
}
