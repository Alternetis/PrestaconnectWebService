using Bukimedia.PrestaSharp.Factories;
using PrestaconnectWebService.Core;
using PrestaconnectWebService.Model.Prestaconnect.Class;
using PrestaconnectWebService.Model.Prestaconnect.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace PrestaconnectWebService.ViewModel
{
    public class CatalogueViewModel : Model.Prestaconnect.Repository.CatalogRepository, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public bool isRoot { get; set; }
        public long cat_id { get; set; }

        private string _cat_name;
        public string cat_name 
        {
            get { return _cat_name; }
            set
            {
                _cat_name = value;
                OnPropertyChanged(nameof(cat_name));
            }
        }
        public string cat_metaDescription { get; set; }
        public string cat_Title { get; set; }
        public int cat_level { get; set; }
        public long cat_parent { get; set; }


        private int _cat_active;
        public int cat_active 
        {
            get { return _cat_active; }
            set
            {
                if (_cat_active != value)
                {
                    _cat_active = value;
                    OnPropertyChanged(nameof(cat_active));
                }
            }
        }

        private bool _cat_sync;
        public bool cat_sync
        {
            get { return _cat_sync; }
            set
            {
                if (_cat_sync != value)
                {
                    _cat_sync = value;
                    OnPropertyChanged(nameof(cat_sync));
                }
            }
        }

        public DateTime cat_Date { get; set; }

        private int? _sag_Id;
        public int? sag_Id 
        {
            get { return _sag_Id; }
            set
            {
                if (_sag_Id != value)
                {
                    _sag_Id = value;
                    _nosynchroniser = _sag_Id == 0; // true si sag_Id est null, sinon false
                    OnPropertyChanged(nameof(sag_Id));
                    OnPropertyChanged(nameof(noSynchroniser));
                }
            }
        }

        private long? _pre_id;
        public long? pre_id
        {
            get { return _pre_id; }
            set
            {
                if (_pre_id != value)
                {
                    _pre_id = value;
                }
            }
        }

        public long? id_shop { get; set; }


        private bool _nosynchroniser = true;
        public bool noSynchroniser
        {
            get { return _nosynchroniser; }
            set
            {
                if (_nosynchroniser != value)
                {
                    _nosynchroniser = value;
                    OnPropertyChanged(nameof(noSynchroniser));
                }
            }
        }

        private ObservableCollection<CatalogueViewModel> _childrens = new ObservableCollection<CatalogueViewModel>();
        public ObservableCollection<CatalogueViewModel> Childrens
        {
            get { return _childrens; }
            set
            {
                _childrens = value;
                OnPropertyChanged(nameof(Childrens));
            }
        }

        public bool Created { get; set; }

   
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public CatalogueViewModel() { }

        public CatalogueViewModel(Bukimedia.PrestaSharp.Entities.category catalog,int catId = 0)
        {

            this.isRoot = catalog.is_root_category == 1 ? true : false;

            this.cat_name = catalog.name[0].Value;
            this.cat_level = catalog.level_depth;
            this.cat_parent = (long)catalog.id_parent;
            this.cat_active = catalog.active;

            this.id_shop = Global.shopSelected.id;


            if(catalog.id != null)
            {
                CatalogRepository CatalogPrestaconnect = new CatalogRepository();

                var cataloguePrestaconnect = CatalogPrestaconnect.GetCatalog((long)catalog.id, Global.shopSelected.id);

                this.cat_id = cataloguePrestaconnect == null ? 0 : cataloguePrestaconnect.Cat_Id;
                this.cat_sync = cataloguePrestaconnect == null ? false : cataloguePrestaconnect.Cat_Sync;
                this.cat_Date = cataloguePrestaconnect == null ? DateTime.Now : cataloguePrestaconnect.Cat_Date;
                this.sag_Id = cataloguePrestaconnect == null ? 0 : cataloguePrestaconnect.Sag_Id;

                this.pre_id = (long)catalog.id;
                PrestaSharp.Factory.CategoryFactory categoryFactory = new PrestaSharp.Factory.CategoryFactory(Global.Auth.BaseUrl, Global.Auth.Account, "");

                List<Bukimedia.PrestaSharp.Entities.category> ChildrensCat;
                if(this.id_shop != null)
                {
                    ChildrensCat = categoryFactory.GetEnfantCatalogForShop(Global.shopSelected.id, (long)this.pre_id);
                }
                else
                {
                    ChildrensCat = categoryFactory.GetEnfantCatalog((long)this.pre_id);
                }

                foreach (var enfant in ChildrensCat)
                {
                    CatalogueViewModel cat = new CatalogueViewModel(enfant);
                    this.Childrens.Add(cat);
                }
            }
            else
            {
                cat_id = catId;
            }
            this.Created = (pre_id != null) && (sag_Id != 0);
        }

        public ObservableCollection<CatalogueViewModel> GetCatalogues()
        {
            ObservableCollection<CatalogueViewModel> catalogues = new ObservableCollection<CatalogueViewModel>();

            PrestaSharp.Factory.CategoryFactory categoryFactory = new PrestaSharp.Factory.CategoryFactory(Global.Auth.BaseUrl, Global.Auth.Account, "");

            List<Bukimedia.PrestaSharp.Entities.category> categories = new List<Bukimedia.PrestaSharp.Entities.category>();

            if(Global.shopSelected != null && Global.shopSelected.id != null)
            {
                categories = categoryFactory.GetRootCatalogForShop(Global.shopSelected.id);

            }
            else
            {
                Bukimedia.PrestaSharp.Factories.CategoryFactory categoryFactory1 = new Bukimedia.PrestaSharp.Factories.CategoryFactory(Global.Auth.BaseUrl, Global.Auth.Account, "");
                Dictionary<string, string> filter = new Dictionary<string, string>
                {
                    { "is_root_category", "1" }
                    
                };
        
                categories = categoryFactory1.GetByFilter(filter,null,null);
            }

            CatalogueViewModel cat = new CatalogueViewModel(categories[0]);
            //var ImageCatalogue = catalogue.CatalogImage;
            catalogues.Add(cat);
            


            return catalogues;
        }

  

    }
}
