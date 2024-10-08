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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PrestaconnectWebService.View.Annexes
{
    /// <summary>
    /// Logique d'interaction pour Update.xaml
    /// </summary>
    public partial class Update : UserControl
    {
        public Update()
        {
            InitializeComponent();
            System.Windows.Resources.StreamResourceInfo info = Application.GetResourceStream(new Uri("updates_prestaconnect.html", UriKind.Relative));
            if (info != null)
                webBrowserControl.NavigateToStream(info.Stream);
            //webBrowserControl.Source = new Uri(@"file:///" + Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "updates_prestaconnect.html").Replace('\\', '/'));
            
        }
    }
}
