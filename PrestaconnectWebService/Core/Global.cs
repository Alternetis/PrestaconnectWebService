using Objets100cLib;
using PrestaconnectWebService.Model.Prestaconnect.Class;
using PrestaconnectWebService.Model.Prestashop.Entities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace PrestaconnectWebService.Core
{
    public class Global
    {
        //public static string URL_Prestashop = string.Empty;
        public static PrestaSharp.Authentification Auth = new PrestaSharp.Authentification();


        public static List<ShopGroupDetail> shopGroup = new List<ShopGroupDetail>();
        public static ShopGroupDetail shopGroupSelected = new ShopGroupDetail();
        public static Bukimedia.PrestaSharp.Entities.shop shopSelected = new Bukimedia.PrestaSharp.Entities.shop();

        //public static bool UILaunch = false;

        #region Config 
        public static string ConfigTheme = "[CONFIGTHEME]";
        #endregion

        #region Prestashop
        public static void GoShop()
        {
            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(Properties.Settings.Default.UrlPrestashop));
        }

        public static void ChangeShop(Bukimedia.PrestaSharp.Entities.shop shop)
        {
            if(shop != null)
            {
                shopSelected = shop;
            }
          
        }

        public static void ChangeShopGroup(ShopGroupDetail shopGroup)
        {
            shopGroupSelected = shopGroup;
        }
        #endregion

        #region Sage Objet Métier
        private static BSCIALApplication100c _sageApplication;

        public static BSCIALApplication100c ConnectionOM(bool retry = false)
        {
            BSCIALApplication100c sageApplication = new BSCIALApplication100c();
            try
            {
                sageApplication.Name = Properties.Settings.Default.GCMFILE;
                sageApplication.Loggable.UserName = Properties.Settings.Default.SAGEUSER;
                sageApplication.Loggable.UserPwd =Properties.Settings.Default.SAGEPASSWORD;
                sageApplication.Open();
            }
            catch (Exception ex)
            {
                if (retry)
                {
                    Core.Log.WriteLog(ex.ToString());
                    throw new Exception("Erreur à la connexion des objets métiers");
                }
                else
                {
                    //new Model.Sage.cbSysLinkRepository().FixLink(Path.GetFileNameWithoutExtension(Properties.Settings.Default.GCMFILE));
                    ConnectionOM(true);
                }
            }
            return sageApplication;
        }

        public static BSCIALApplication100c SageApplication
        {
            get
            {
                if (_sageApplication == null)
                {
                    _sageApplication = ConnectionOM();
                }
                if (!_sageApplication.IsOpen)
                {
                    _sageApplication.Open();
                }
                return _sageApplication;
            }
        }
        #endregion


        private static ConnectionInfos connectionInfos;
        internal static ConnectionInfos GetConnectionInfos()
        {
            if (connectionInfos == null)
                connectionInfos = new ConnectionInfos();

            return connectionInfos;
        }

        public static BitmapImage BytesToImage(byte[] imageData)
        {
            if (imageData == null || imageData.Length == 0)
                return null;

            using (MemoryStream ms = new MemoryStream(imageData))
            {
                BitmapImage bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.StreamSource = ms;
                bitmapImage.EndInit();

                return bitmapImage;
            }
        }

        public static BitmapImage NotFoundImage()
        {
            // Chemin vers la ressource image dans votre projet
            string imagePath = "pack://application:,,,/Resources/Images/NoImageSmall.jpg";

            // Créer un objet BitmapImage
            BitmapImage bitmap = new BitmapImage();

            // Définir l'URI source avec le chemin de la ressource image
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(imagePath);
            bitmap.EndInit();

            return bitmap;
        }


    }
}
