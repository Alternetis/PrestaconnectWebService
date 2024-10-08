using Bukimedia.PrestaSharp.Factories;
using PrestaconnectWebService.Core;
using PrestaconnectWebService.Model.Prestaconnect.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace PrestaconnectWebService.ViewModel
{
    public class CatalogueSelectedViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public Bukimedia.PrestaSharp.Entities.category Category { get; set; }
        public int SageId { get; set; }
        public long CatId { get; set; }
        public long? PreId { get; set; }
        public long? IdShop { get; set; }
        public bool CatSync { get; set; }

        public BitmapImage smallImage { get; set; }



        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public string ViewGoogle(int IdLangage)
        {
            int id = IdLangage-1;
            return @"<html><div style=""width: 600px; font-family: arial,sans-serif;""><h3 style=""font-size: 20px; margin: 0px 0px 3px 0px; padding: 0px; color: #1a0dab;"">" +
                (Category.meta_title[id].Value == null ? "" : Category.meta_title[id].Value + " - " + Global.shopSelected.name)
                    + @"</h3><span style=""font-size: 14px; color: #4d5156; line-height: 1.58;"">" +
                    (Category.meta_description[id].Value == null ? "" : (Category.meta_description[id].Value)
                    + "</span></div></html>");
        }

        public CatalogueSelectedViewModel() { }
        public CatalogueSelectedViewModel(Bukimedia.PrestaSharp.Entities.category catalog) 
        {

            CatalogRepository CatalogPrestaconnect = new CatalogRepository();
            var cataloguePrestaconnect = CatalogPrestaconnect.GetCatalog((long)catalog.id,Global.shopSelected.id);

            this.Category = catalog;
            this.PreId = (long)catalog.id;
            this.SageId = cataloguePrestaconnect == null ? 0 : cataloguePrestaconnect.Sag_Id; 
            this.IdShop = Global.shopSelected.id;
            this.CatSync = cataloguePrestaconnect == null ? false : true;
            this.CatId = cataloguePrestaconnect == null ? 0 : cataloguePrestaconnect.Cat_Id;

            Bukimedia.PrestaSharp.Factories.ImageFactory imageFactory = new ImageFactory(Global.Auth.BaseUrl, Global.Auth.Account, "");
            try
            {
                
                var ImageData = imageFactory.GetCategoryImage((long)this.PreId, "small_default");
                if(ImageData.Length > 0)
                {
                    smallImage = Global.BytesToImage(ImageData);
                }
                else
                {
                    smallImage = Global.NotFoundImage();
                }
         
            }
            catch
            {
                smallImage = Global.NotFoundImage();
            }
        }

        public void AddUpdateSageCatalog( long preId, long ShopId)
        {

            CatalogRepository catalogRepository = new CatalogRepository();

            var catalogSage = catalogRepository.GetCatalog(preId, ShopId);

            this.IdShop = ShopId;
            if (catalogSage == null)
            {
                this.PreId = preId;
                catalogRepository.Add(this);
                //catalogSage = catalogRepository.GetCatalog(preId, ShopId);
                MessageInformation.Show($"Création du catalogue {this.Category.name[0].Value} réussis.");
            }
            else
            {
                this.SageId = catalogSage.Sag_Id;
                catalogRepository.Update(this);
                MessageInformation.Show($"Modification du catalogue {catalogSage.Cat_Name} réussis.");
            }
        }

    }
}
