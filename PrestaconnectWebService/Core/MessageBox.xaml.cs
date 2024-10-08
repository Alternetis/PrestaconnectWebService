using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PrestaconnectWebService.Core
{
    /// <summary>
    /// Logique d'interaction pour MessageBox.xaml
    /// </summary>
    public partial class MessageInformation : Window
    {
        public MessageInformation(string text,string textGras = "Information")
        {
            InitializeComponent();
            Text1.Text = textGras;
            Text2.Text = text;
        }

        private void Validate_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        public static void Show(string text, string textGras = "Information")
        {
            MessageInformation messageInformation = new MessageInformation(text, textGras);
            messageInformation.Show();
        }
    }
}
