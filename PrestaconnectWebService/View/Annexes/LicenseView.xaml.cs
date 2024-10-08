using PrestaconnectWebService.Core;
using System.Windows;

namespace PrestaconnectWebService.View.Annexes
{
    /// <summary>
    /// Logique d'interaction pour License.xaml
    /// </summary>
    public partial class LicenseView : Window
    {
        public LicenseView()
        {
            InitializeComponent();
            string value = "Votre licence Prestaconnect est inactive !";
            switch (UpdateVersion.LicenseIsActive)
            {
                default:
                case UpdateVersion.LicenceActivation.disabled:
                    value = "Votre licence Prestaconnect est inactive !";
                    break;
                case UpdateVersion.LicenceActivation.expired:
                    value = "Nous vous informons que votre DUA est arrivé à expiration !" 
                        + "\n" + "Veuillez contactez votre revendeur pour le renouveler !";
                    break;
                case UpdateVersion.LicenceActivation.incorrect:
                    value = "Les informations de connexion liées à la licence \"" + UpdateVersion.License.LicenseKey + "\" ne correspondent pas à celle de votre instance !"
                        + "\n" + "Vous pouvez contacter votre revendeur pour vérifier vos paramètres !";
                    break;
                case UpdateVersion.LicenceActivation.enabled:
                    value = "Votre licence Prestaconnect est opérationnelle !";
                    break;
            }
            TextBlockLicense.Text = value;
        }
    }
}