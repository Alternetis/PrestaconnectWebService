using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PrestaconnectWebService.View.Annexes
{
	/// <summary>
	/// Logique d'interaction pour About.xaml
	/// </summary>
	public partial class AboutView : Window
	{
		public AboutView()
		{
			InitializeComponent();
            LabelPrestaconnectVersion.Content = "PrestaConnect Web Service version " + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
            LabelPrestaconnectSociete.Content = "Société Alternetis - 42 chemin de Mézeau 86000 Poitiers France";
            LabelPrestaconnectDescription.Content = "Synchroniser Sage & Prestashop en 1 seul outil";
            LabelPrestaconnectCopyright.Content = "2015 Alternetis";

            string dua = (Core.UpdateVersion.License != null && Core.UpdateVersion.License.DUADate < DateTime.Now.AddYears(1).AddMonths(1)) 
                            ?   " / DUA valable jusqu'au " + Core.UpdateVersion.License.DUADate.ToShortDateString() 
                                : string.Empty;
            LabelPrestaconnectLicence.Content = "N° de licence : " + Properties.Settings.Default.LicenceKey.ToString() + dua;
			// Insérez le code requis pour la création d’objet sous ce point.
		}

        private void HyperlinkAlternetisLink_RequestNavigate(object sender, System.Windows.Navigation.RequestNavigateEventArgs e)
        {
            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(e.Uri.AbsoluteUri));
            e.Handled = true;
        }

        private void HyperlinkPrestaconnectLink_RequestNavigate(object sender, System.Windows.Navigation.RequestNavigateEventArgs e)
        {
            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(e.Uri.AbsoluteUri));
            e.Handled = true;
        }

        private void Image_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            //UpdatesDialog UpdatesDialog = new UpdatesDialog();
            //UpdatesDialog.ShowDialog();
        }
	}
}