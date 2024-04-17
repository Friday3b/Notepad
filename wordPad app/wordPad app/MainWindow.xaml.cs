using System.Text;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using Microsoft.Win32;

namespace wordPad_app
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Save_button (object sender, RoutedEventArgs e)
        {
            string filename = "Wordpad.json";
            var jsonData  = JsonSerializer.Serialize(textbox1.Text , new JsonSerializerOptions { WriteIndented = true} );//PROBLEM
            File.WriteAllText(filename, jsonData);
            MessageBox.Show("File saved successfully ");
        }



        //buradaki System.ComponentModel.CancelEventArgs e bu event den dolayi MainWindow.xaml faylinda 
        //windowsdan sonra "Closing="MainWindow_Closing"  bu kodu yazdiq ki cixarken avtomatik baglansin ve 
        //save olunsun 
        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            string filename = "AutoSaved.json";
            var jsonData = JsonSerializer.Serialize(textbox1.Text, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(filename, jsonData);
            MessageBox.Show("File auto-saved successfully ");

            this.Close();
        }

        private void Copy_button(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(textbox1.SelectedText))
            {
                string filename = "copyfile";
                Clipboard.SetText(textbox1.SelectedText);
                var jsonData = JsonSerializer.Serialize(textbox1.SelectedText, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(filename, jsonData);

                MessageBox.Show("Selected text has copied");
            }
            else
            {
                MessageBox.Show("there is no text"); 
            }
        }
       
        private void Paste_button (object sender, RoutedEventArgs e)
        {

            string filename = "copyfile"; 
            
            if (File.Exists(filename))
            {
                string jsondata  = File.ReadAllText(filename); 
                textbox1.Text = JsonSerializer.Deserialize<string>(jsondata);
                MessageBox.Show("Text pasted successfully");

            }

        }
        private void Cut_button (object sender, RoutedEventArgs e)
        {
            string filename = "copyfile";
            var jsonData = JsonSerializer.Serialize(textbox1.Text, new JsonSerializerOptions { WriteIndented = true });//PROBLEM
            File.WriteAllText(filename, jsonData);
            MessageBox.Show("File has cutted successfully ");
            textbox1.Clear();

        }
        private void FileOpen_button(object sender, RoutedEventArgs e)
        {
            string filePath = filepath.Text;

            try
            {
                if (File.Exists(filePath))
                {
                    textbox1.Text = File.ReadAllText(filePath);
                    MessageBox.Show(" Opened successfully ");
                }
                else
                {
                    MessageBox.Show("There is no tomorrow --> motivation club ");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("error : " + ex.Message);
            }
        }

        private void SelectAll_button (object sender, RoutedEventArgs e)
        {

            textbox1.SelectAll();
            textbox1.SelectionBrush = Brushes.Green;
            textbox1.SelectionOpacity = 0.5;


        }


    }


}