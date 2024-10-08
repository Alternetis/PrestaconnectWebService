using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
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
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Bukimedia.PrestaSharp.Entities;
using Bukimedia.PrestaSharp.Entities.FilterEntities;
using Bukimedia.PrestaSharp.Factories;
using PrestaconnectWebService.View.Annexes;
using PrestaconnectWebService.Contexts;
using PrestaconnectWebService.Core;
using PrestaconnectWebService.Model.Prestaconnect.Repository;
using PrestaconnectWebService.Model.Prestashop;
using System.IO;
using System.Net.Security;
using System.Net;
using System.Xml.Serialization;
using System.Security.Cryptography.X509Certificates;
using System.Runtime.CompilerServices;
using PrestaconnectWebService.View.Catalogue;
using PrestaconnectWebService.Core.Loading;
using System.Threading;
using PrestaconnectWebService.Model.Prestashop.Entities;
using Bukimedia.PrestaSharp.Entities.AuxEntities;

namespace PrestaconnectWebService.View.MainWin
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            #region Verification Licence 

            UpdateVersion.LicenseIsActive = ReadLicence();
            if (UpdateVersion.License != null && UpdateVersion.License.Comment.ToUpper() == "LICENCE_TEST")
            {
                UpdateVersion.LicenseIsActive = UpdateVersion.LicenceActivation.enabled;
                UpdateVersion.License = new UpdateVersion.Key() { Option1 = true, Option2 = true, Option3 = false };
            }

            #endregion

            if (UpdateVersion.License != null && UpdateVersion.LicenseIsActive == UpdateVersion.LicenceActivation.enabled)
            {
                DataContext = new MainContext();
                InitializeComponent();

                #region Theme Loading

                Model.Prestaconnect.ConfigRepository ConfigRepository = new Model.Prestaconnect.ConfigRepository();
                if (ConfigRepository.ExistName(Global.ConfigTheme))
                {
                    Model.Prestaconnect.Class.Config Config = ConfigRepository.ReadName(Global.ConfigTheme);
                    foreach (ComboBoxItem ComboBoxItem in ComboBoxTheme.Items)
                    {
                        if (ComboBoxItem.Content.ToString() == Config.Con_Value)
                        {
                            ComboBoxTheme.SelectedItem = ComboBoxItem;
                            break;
                        }
                    }
                }

                #endregion

                #region Shop

                LoadShopCombo();
                #endregion

                LoadLogo();
                LoadOrdersAndCustomers();
            }
        }


        #region Licence 
        private UpdateVersion.LicenceActivation ReadLicence()
        {
            UpdateVersion.LicenceActivation isLicence = UpdateVersion.LicenceActivation.disabled;

            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                ServicePointManager.ServerCertificateValidationCallback += new RemoteCertificateValidationCallback(ValidateRemoteCertificate);
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(string.Format("https://mezeau.alternetis.fr/api/licensekeys.xml?licensekey={0}", Properties.Settings.Default.LicenceKey));
                request.Credentials = new NetworkCredential("prestaconnect", "AgX83ixj95g28V49hi5rHDEWX");
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                Encoding encoding = Encoding.GetEncoding("utf-8");

                XmlSerializer serializer = new XmlSerializer(typeof(UpdateVersion.response));
                using (Stream stream = response.GetResponseStream())
                {
                    UpdateVersion.response serviceResponse = (UpdateVersion.response)serializer.Deserialize(stream);
                    if (serviceResponse != null && serviceResponse.item != null)
                    {
                        UpdateVersion.License = new UpdateVersion.Key()
                        {
                            Active = serviceResponse.item.active,
                            BDDPrestaconnect = serviceResponse.item.bddprestaconnect,
                            BDDPrestashop = serviceResponse.item.bddprestashop,
                            BDDSage = serviceResponse.item.bddsage,
                            Comment = serviceResponse.item.comment,
                            Dealer = serviceResponse.item.dealer,
                            Domain = serviceResponse.item.domain,
                            DUADate = serviceResponse.item.duadate,
                            LicenseKey = serviceResponse.item.licensekey,
                            Option1 = serviceResponse.item.option1,
                            Option2 = serviceResponse.item.option2,
                            Option3 = serviceResponse.item.option3,
                            Organization = serviceResponse.item.organization,
                            Prestashop = serviceResponse.item.prestashop,
                            Product = serviceResponse.item.product,
                            Sage = serviceResponse.item.sage,
                        };
                    }
                }
                if(UpdateVersion.License != null)
                {
                    if (UpdateVersion.License.Active && UpdateVersion.License.DUADate.Date >= DateTime.Today)
                    {
                        if (UpdateVersion.License.BDDPrestaconnect != Global.GetConnectionInfos().PrestaconnectDatabase
                            || UpdateVersion.License.BDDSage != Global.GetConnectionInfos().SageDatabase
                            || UpdateVersion.License.BDDPrestashop != Global.GetConnectionInfos().PrestashopDatabase
                            || UpdateVersion.License.Domain != Global.GetConnectionInfos().PrestashopServer)
                        {
                            isLicence = UpdateVersion.LicenceActivation.incorrect;
                        }
                        else
                        {
                            isLicence = UpdateVersion.LicenceActivation.enabled;
                        }
                    }
                    else
                    {
                        isLicence = UpdateVersion.LicenceActivation.expired;
                    }
                }
  
            }
            catch (Exception ex)
            {
                //Error.SendMailError(ex.ToString());
                //Error.SendMailWS(ex);
                Core.Log.WriteLog(ex.ToString());
            }

            return isLicence;
        }

        private static bool ValidateRemoteCertificate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors policyErrors)
        {
            return true;
        }
        #endregion

        #region Theme

        private void ComboBoxTheme_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem selectedItem = (ComboBoxItem)ComboBoxTheme.SelectedItem;
            string Theme = (string)selectedItem.Content;
            
            string uri = @"\Resources\Theme\" + Theme.Replace(" ","") + ".xaml";
            ((App)Application.Current).ChangeTheme(new Uri(uri, UriKind.Relative));

            Model.Prestaconnect.ConfigRepository ConfigRepository = new Model.Prestaconnect.ConfigRepository();
            Model.Prestaconnect.Class.Config Config = new Model.Prestaconnect.Class.Config();
            bool isConfig = false;
            if (ConfigRepository.ExistName(Global.ConfigTheme))
            {
                Config = ConfigRepository.ReadName(Global.ConfigTheme);
                isConfig = true;
            }
            Config.Con_Value = Theme;
            if (isConfig == true)
            {
                ConfigRepository.Save();
            }
            else
            {
                Config.Con_Name = Global.ConfigTheme;
                ConfigRepository.Add(Config);
            }
        }
        #endregion

        #region Prestashop
        private void ImagePrestashopLogo_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Global.GoShop();
        }
        private void HyperlinkLabelPrestashopLink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            Global.GoShop();
            e.Handled = true;
        }
        private void ShopSelected_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Bukimedia.PrestaSharp.Entities.shop shop = ShopSelected.SelectedItem as Bukimedia.PrestaSharp.Entities.shop;
            if(shop == null)
            {
                shop = Global.shopGroupSelected.shops[0];
            }
            Global.ChangeShop(shop);
            LoadOrdersAndCustomers();
        }
        private void ShopGroupSelected_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ShopGroupDetail shopGroup = ShopGroupSelected.SelectedItem as ShopGroupDetail;
            Global.ChangeShopGroup(shopGroup);
            ShopSelected.ItemsSource = shopGroup.shops;
            ShopSelected.SelectedItem = shopGroup.shops[0];
        }
        private void LoadLogo()
        {
            string logoUri = "/img/logo.png";
            ImagePrestashopLogo.Source = new BitmapImage(new Uri(Properties.Settings.Default.UrlPrestashop + logoUri));
            ImagePrestashopLogo.ToolTip = Properties.Settings.Default.UrlPrestashop;
        }

        public void LoadShopCombo()
        {
            ShopGroupDetail shopsGroup = new ShopGroupDetail();
            Global.shopGroup = shopsGroup.GetShopGroupDetails();
            ShopGroupSelected.ItemsSource = Global.shopGroup;
            ShopGroupSelected.SelectedItem = Global.shopGroup[0];

            ShopSelected.ItemsSource = Global.shopGroup[0].shops;
            ShopSelected.SelectedItem = Global.shopGroup[0].shops[0];
        }


        #endregion

        private void ButtonActualiser_Click(object sender, RoutedEventArgs e)
        {
                LoadOrdersAndCustomers();
        }

        private void LoadOrdersAndCustomers()
        {
            try
            {
                IsEnabled = false;
                Mouse.OverrideCursor = Cursors.Wait;


                ViewModel.OrderViewModel orderResume = new ViewModel.OrderViewModel();
                DataGridCommande.ItemsSource = orderResume.ReadOrderResume(20); ;

                ViewModel.CustomerViewModel customers = new ViewModel.CustomerViewModel();
                DataGridClient.ItemsSource = customers.GetClient(20);

            }
            finally
            {
                Mouse.OverrideCursor = null;
                IsEnabled = true;
            }

            //Temp.ListLocalCustomer = new Model.Local.CustomerRepository().List();
            //Temp.ListF_COMPTET_BtoB = new Model.Sage.F_COMPTETRepository().ListBtoB((short)TiersType.TiersTypeClient);

            //Model.Prestashop.PsCustomerRepository PsCustomerRepository = new Model.Prestashop.PsCustomerRepository();
            //DataGridClient.ItemsSource = (Global.GetConfig().ConfigClientFiltreCommande)
            //    ? PsCustomerRepository.ListTopActiveOrderByDateAdd(20, 1, Global.CurrentShop.IDShop)
            //    : PsCustomerRepository.ListTopActiveOrderByDateAddWithOrder(20, 1, Global.CurrentShop.IDShop);
        }

        private void ButtonLog_Click(object sender, RoutedEventArgs e)
        {
            LogInformationsView informations = new LogInformationsView();
            informations.ShowDialog();
        }
        private void ButtonAbout_Click(object sender, RoutedEventArgs e)
        {
            AboutView about = new AboutView();
            about.ShowDialog();
        }

        private void MenuItemCatalogue_Click(object sender, RoutedEventArgs e)
        {
            LoadingView loading = new LoadingView();
            loading.Show();
            CatalogueView Catalogue = new CatalogueView();
            loading.Close();
            Catalogue.ShowDialog();
            ShopGroupSelected.SelectedItem = Global.shopGroupSelected;
            ShopSelected.SelectedItem = Global.shopSelected;
        }

        private void ButtonGamme_Click(object sender, RoutedEventArgs e)
        {
            LoadingView loading = new LoadingView();
            loading.Show();
            View.Gamme.GammeView gammeView = new View.Gamme.GammeView();
            loading.Close();
            gammeView.ShowDialog();
        }

        private void ButtonArticles_Click(object sender, RoutedEventArgs e)
        {
            LoadingView loading = new LoadingView();
            loading.Show();
            View.Articles.ArticlesView articleView = new View.Articles.ArticlesView();
            loading.Close();
            articleView.ShowDialog();
        }
    }
}
