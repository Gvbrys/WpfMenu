using Microsoft.Win32;
using System.IO;
using System.Windows;
using System.Windows.Input;

namespace TextEditorApp
{
    public static class CustomCommands
    {
        public static readonly RoutedUICommand WrapText = new RoutedUICommand(
            "Zawijaj wiersze", "WrapText", typeof(CustomCommands));

        public static readonly RoutedUICommand IncreaseFont = new RoutedUICommand(
            "Powiększ tekst", "IncreaseFont", typeof(CustomCommands));
    }

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void CloseCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void SaveCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "Pliki tekstowe (*.txt)|*.txt|Wszystkie pliki (*.*)|*.*"
            };

            if (saveFileDialog.ShowDialog() == true)
            {
                File.WriteAllText(saveFileDialog.FileName, TextBoxEditor.Text);
            }
        }

        private void SaveCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = !string.IsNullOrWhiteSpace(TextBoxEditor.Text);
        }

        private void WrapTextCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            TextBoxEditor.TextWrapping = TextBoxEditor.TextWrapping == TextWrapping.Wrap ? TextWrapping.NoWrap : TextWrapping.Wrap;
        }

        private void IncreaseFontCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            TextBoxEditor.FontSize += 2;
        }

        private void IncreaseFontCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = TextBoxEditor.FontSize < 30;
        }

        private void TextBoxEditor_SelectionChanged(object sender, RoutedEventArgs e)
        {
            CopyButton.IsEnabled = !string.IsNullOrEmpty(TextBoxEditor.SelectedText);
        }

        private void CopyButton_Click(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(TextBoxEditor.SelectedText);
        }
    }
}
