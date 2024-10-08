using Bukimedia.PrestaSharp.Factories;
using PrestaconnectWebService.Core;
using PrestaconnectWebService.Model.Prestaconnect.Class;
using PrestaconnectWebService.Model.Prestaconnect.Repository;
using PrestaconnectWebService.Model.Prestashop.Entities;
using PrestaconnectWebService.Model.Sage;
using PrestaconnectWebService.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Xml.Linq;

namespace PrestaconnectWebService.View.Catalogue
{
    /// <summary>
    /// Logique d'interaction pour CatalogueView.xaml
    /// </summary>
    public partial class CatalogueView : Window
    {

        public ObservableCollection<CatalogueViewModel> CatalogueItems { get; set; }
        public CatalogueSelectedViewModel SelectedCatalog { get; set; }
        public F_CATALOGUE SelectedCatalogLink { get; set; }

        public List<CatalogueSelectedViewModel> CreatedPendingCatalogue = new List<CatalogueSelectedViewModel>();
        
        public Bukimedia.PrestaSharp.Entities.category categoryPs { get; set; }
        public Bukimedia.PrestaSharp.Entities.language languagePs { get; set; }

        private bool AllSync = true;


        public CatalogueView()
        {
            InitializeComponent();

            CatalogueViewModel catalogue = new CatalogueViewModel();

            CatalogueItems = catalogue.GetCatalogues();

            Bukimedia.PrestaSharp.Factories.LanguageFactory language = new Bukimedia.PrestaSharp.Factories.LanguageFactory(Global.Auth.BaseUrl, Global.Auth.Account, "");
            var lang = language.GetAll();

            LangueComboBox.ItemsSource = lang;
            LangueComboBox.SelectedItem = lang[0];
            languagePs = lang[0];

            TreeViewComboBox.ItemsSource = F_CATALOGUE.GetCatalogue();

            LoadShopCombo();
        }


        private void LoadCategoryLangue()
        {
            if (SelectedCatalog != null)
            {


                if (SelectedCatalog.SageId != 0)
                {
                    SelectedCatalogLink = F_CATALOGUE.GetCatalogue(SelectedCatalog.SageId);

                    btnShowTreeView.Content = SelectedCatalogLink.Intitule;

                }
                else
                {
                    btnShowTreeView.Content = "Aucun";
                    //TreeViewComboBox.Items.MoveCurrentTo(null);

                }

                ThumbmailCategory.Source = SelectedCatalog.smallImage;

                InfoCatalog.IsEnabled = true;
                PrestashopName.Text = SelectedCatalog.Category.name.FirstOrDefault(l => l.id == languagePs.id)?.Value;


                TinyMceDescription.BDDHtmlContent = SelectedCatalog.Category.description.FirstOrDefault(l => l.id == languagePs.id)?.Value;

                CheckBoxActif.IsChecked = SelectedCatalog.Category.active == 1 ? true : false;

                PrestashopBaliseTitle.Text = SelectedCatalog.Category.meta_title.FirstOrDefault(l => l.id == languagePs.id)?.Value;
                PrestashopMetaDescription.Text = SelectedCatalog.Category.meta_description.FirstOrDefault(l => l.id == languagePs.id)?.Value;
                PrestashopMetaMotClef.Text = SelectedCatalog.Category.meta_keywords.FirstOrDefault(l => l.id == languagePs.id)?.Value;
                PrestashopLinkRewrite.Text = SelectedCatalog.Category.link_rewrite.FirstOrDefault(l => l.id == languagePs.id)?.Value;
            }
            else
            {
                SelectedCatalog = new CatalogueSelectedViewModel();
                Bukimedia.PrestaSharp.Entities.category categoryEntity = new Bukimedia.PrestaSharp.Entities.category();
                SelectedCatalog.Category = categoryEntity;
            }
        }

        #region Shop Gestion
        public void LoadShopCombo()
        {
            ShopGroupSelected.ItemsSource = Global.shopGroup;
            ShopGroupSelected.SelectedItem = Global.shopGroupSelected;

            ShopSelected.ItemsSource = Global.shopGroupSelected.shops;
            ShopSelected.SelectedItem = Global.shopSelected;
        }
        private void ShopGroupSelected_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ShopGroupDetail shopGroup = ShopGroupSelected.SelectedItem as ShopGroupDetail;
            Global.ChangeShopGroup(shopGroup);
            ShopSelected.ItemsSource = shopGroup.shops;
            ShopSelected.SelectedItem = shopGroup.shops[0];
        }
        private void ShopSelected_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Global.ChangeShop((Bukimedia.PrestaSharp.Entities.shop)ShopSelected.SelectedItem);
            CatalogueViewModel catalogue = new CatalogueViewModel();

            TextAvertismentNoShop.Visibility = Visibility.Collapsed;
            CatalogueItems = catalogue.GetCatalogues();
            TreeViewCatalogue.ItemsSource = null;            //{

            TreeViewCatalogue.ItemsSource = CatalogueItems;
        }

        #endregion

        #region Changed Selected Catalog Contenus
        private void MetaDescription_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (SelectedCatalog != null)
            {
                var MetaDescription = SelectedCatalog.Category.meta_description.FirstOrDefault(l => l.id == languagePs.id);
                MetaDescription.Value = PrestashopMetaDescription.Text;
                ViewWebBrowser.NavigateToString(SelectedCatalog.ViewGoogle((int)languagePs.id));
            }
        }

        private void MetaTitle_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (SelectedCatalog != null)
            {
                var Metatitle= SelectedCatalog.Category.meta_title.FirstOrDefault(l => l.id == languagePs.id);
                Metatitle.Value = PrestashopBaliseTitle.Text;
                ViewWebBrowser.NavigateToString(SelectedCatalog.ViewGoogle((int)languagePs.id));
            }
        }

        private void PrestashopName_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (SelectedCatalog != null)
            {
                var Name = SelectedCatalog.Category.name.FirstOrDefault(l => l.id == languagePs.id);
                Name.Value = PrestashopName.Text;
                if(TreeViewCatalogue.SelectedItem != null)
                {
                    CatalogueViewModel cat = (CatalogueViewModel)TreeViewCatalogue.SelectedItem;
                    cat.cat_name = Name.Value;
                }
            }
        }

        private void PrestashopMetaMotClef_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (SelectedCatalog != null)
            {
                var Keyword = SelectedCatalog.Category.meta_keywords.FirstOrDefault(l => l.id == languagePs.id);
                Keyword.Value = PrestashopMetaMotClef.Text;
            }
        }

        private void PrestashopLinkRewrite_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (SelectedCatalog != null)
            {
                var LinkRewrite = SelectedCatalog.Category.link_rewrite.FirstOrDefault(l => l.id == languagePs.id);
                LinkRewrite.Value = PrestashopLinkRewrite.Text;
            }
        }

        private void ImageParcourir_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedCatalog.Category != null)
            {
                Microsoft.Win32.OpenFileDialog OFD = new Microsoft.Win32.OpenFileDialog()
                {
                    Filter = "Images (*.jpg, *.png, *.gif) | *.jpg;*.png;*.gif",
                    Multiselect = false
                };
                bool? result = OFD.ShowDialog();

                if (result.HasValue && result.Value)
                {
                    Bukimedia.PrestaSharp.Factories.ImageFactory imageFactory = new Bukimedia.PrestaSharp.Factories.ImageFactory(Global.Auth.BaseUrl, Global.Auth.Account, "");
                    imageFactory.UpdateCategoryImage((long)SelectedCatalog.Category.id, OFD.FileName);

                    var ImageData = imageFactory.GetCategoryImage((long)SelectedCatalog.Category.id, "small_default");
                    ThumbmailCategory.Source = Global.BytesToImage(ImageData);
                    MessageInformation.Show($"Modification image du catalogue réussis.");

    
                }
                   
            }
        }
        #endregion

        #region Gestion Evenement


        private bool isSelectionChanging = false;

        private void Catalogs_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            try
            {
                if (!isSelectionChanging)
                {
                    try
                    {
                        isSelectionChanging = true;

                        if (sender != null && ((TreeView)sender).SelectedItem != null)
                        {
                            var catalogueView = ((TreeView)sender).SelectedItem as CatalogueViewModel;
                            var oldSelectedItem = e.OldValue as CatalogueViewModel;

                            if (catalogueView != oldSelectedItem)
                            {
                                // Votre logique de traitement ici
                                if (catalogueView.pre_id != null)
                                {
                                    PrestaSharp.Factory.CategoryFactory categoryFactory = new PrestaSharp.Factory.CategoryFactory(Global.Auth.BaseUrl, Global.Auth.Account, "");
                                    if(Global.shopSelected.id != null)
                                    {
                                        categoryPs = categoryFactory.GetForShop((long)catalogueView.pre_id, Global.shopSelected.id);
                                    }
                                    else
                                    {
                                        categoryPs = categoryFactory.Get((long)catalogueView.pre_id);
                                    }

                                    SelectedCatalog = new CatalogueSelectedViewModel(categoryPs);
                                }
                                else
                                {
                                    SelectedCatalog = CreatedPendingCatalogue.Where(c => c.CatId == catalogueView.cat_id && c.IdShop == Global.shopSelected.id).FirstOrDefault();
                                }

                                if (SelectedCatalog != null)
                                {

                                    InfoCatalog.IsEnabled = true;
                                    InfoCatalog.DataContext = SelectedCatalog;

                                    if (SelectedCatalog.IdShop == Global.shopSelected.id)
                                    {
                                        LoadCategoryLangue();
                                    }
                                }
                            }
                        }
                        else
                        {
                            InfoCatalog.IsEnabled = false;
                            InfoCatalog.DataContext = new CatalogueViewModel();
                        }
                    }
                    catch (Exception ex)
                    {
                        Core.Log.WriteLog(ex.ToString());
                    }
                    finally
                    {
                        isSelectionChanging = false;
                    }
                }


            }catch(Exception ex)
            {
                Core.Log.WriteLog(ex.ToString());
            }
       
        }
        private void CheckBoxActif_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedCatalog.PreId != null)
            {
                SelectedCatalog.Category.active = (bool)CheckBoxActif.IsChecked ? 1 : 0;
                CatalogRepository catalogRepository = new CatalogRepository();
                if (catalogRepository.GetCatalog((long)SelectedCatalog.PreId,Global.shopSelected.id) != null)
                {
                    catalogRepository.Update(SelectedCatalog);

                    PrestaSharp.Factory.CategoryFactory categoryFactory = new PrestaSharp.Factory.CategoryFactory(Global.Auth.BaseUrl, Global.Auth.Account, "");

                    if (Global.shopSelected.id != null)
                    {
                        categoryFactory.UpdateForShop(SelectedCatalog.Category, (long)Global.shopSelected.id);
                    }
                    else
                    {
                        categoryFactory.Update(SelectedCatalog.Category);
                    }
                }
                else
                {
                    PrestaSharp.Factory.CategoryFactory categoryFactory = new PrestaSharp.Factory.CategoryFactory(Global.Auth.BaseUrl, Global.Auth.Account, "");
                    if(Global.shopSelected.id != null)
                    {
                        categoryFactory.UpdateForShop(SelectedCatalog.Category, (long)Global.shopSelected.id);
                    }
                    else
                    {
                        categoryFactory.Update(SelectedCatalog.Category);
                    }

                }
            }
        }

        private void LangueComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            languagePs = ((ComboBox)sender).SelectedItem as Bukimedia.PrestaSharp.Entities.language;

            LoadCategoryLangue();
        }

        private void ValidateCatalogModification_Click(object sender, RoutedEventArgs e)
        {
            PrestaSharp.Factory.CategoryFactory categoryFactory = new PrestaSharp.Factory.CategoryFactory(Global.Auth.BaseUrl, Global.Auth.Account, "");
            if (Global.shopSelected.id != null)
            {
                if (SelectedCatalog.PreId != null)
                {
                    categoryFactory.UpdateForShop(SelectedCatalog.Category, (long)Global.shopSelected.id);
                    SelectedCatalog.AddUpdateSageCatalog((long)SelectedCatalog.PreId, (long)Global.shopSelected.id);
                }
                else
                {
                    Bukimedia.PrestaSharp.Entities.category Result = categoryFactory.AddForShop(SelectedCatalog.Category, (long)Global.shopSelected.id);
                    SelectedCatalog.AddUpdateSageCatalog((long)Result.id, (long)Global.shopSelected.id);
                }
            }
            else
            {
                if (SelectedCatalog.PreId != null)
                {
                    foreach (Bukimedia.PrestaSharp.Entities.shop shop in ShopSelected.ItemsSource)
                    {
                        if (shop.id != null)
                        {
                            categoryFactory.UpdateForShop(SelectedCatalog.Category, (long)shop.id);
                            SelectedCatalog.AddUpdateSageCatalog((long)SelectedCatalog.PreId, (long)shop.id);
                        }
                    }
                }
                else
                {
                    bool First = true;
                    foreach (Bukimedia.PrestaSharp.Entities.shop shop in ShopSelected.ItemsSource)
                    {   
                        if (shop.id != null)
                        {
                            if (First)
                            {
                                Bukimedia.PrestaSharp.Entities.category Result = categoryFactory.AddForShop(SelectedCatalog.Category, (long)shop.id);
                                SelectedCatalog.PreId = Result.id;
                                SelectedCatalog.Category = Result;
                                SelectedCatalog.AddUpdateSageCatalog((long)Result.id,(long)shop.id);
                                First = false;
                            }
                            else
                            {
                               categoryFactory.UpdateForShop(SelectedCatalog.Category, (long)shop.id);
                               SelectedCatalog.AddUpdateSageCatalog((long)SelectedCatalog.PreId, (long)shop.id);
                            }
                        }
                    }
                }
            }

        }

        private void NewCatalogue_Click(object sender, RoutedEventArgs e)
        {
            CreateCatalogue();
        }

        private void CreateCatalogue()
        {
            CatalogueSelectedViewModel RetroCatalogue = SelectedCatalog;

            long idparent = 0;
            if (SelectedCatalog.PreId != null)
            {
                idparent = (long)SelectedCatalog.PreId;
            }
            else
            {
                idparent = (long)CatalogueItems[0].pre_id;
            }


            SelectedCatalog = new CatalogueSelectedViewModel();
            Bukimedia.PrestaSharp.Entities.category categoryEntity = new Bukimedia.PrestaSharp.Entities.category();
            SelectedCatalog.Category = categoryEntity;
            SelectedCatalog.Category.id_parent = idparent;
            SelectedCatalog.Category.is_root_category = 0;
            SelectedCatalog.Category.active = 1;

            SelectedCatalog.IdShop = Global.shopSelected.id;

            SelectedCatalog.CatId = CreatedPendingCatalogue.Count();

            AddCatalogue.Show(SelectedCatalog);
            


            if (SelectedCatalog.Category != null)
            {
                CreatedPendingCatalogue.Add(SelectedCatalog);

                CatalogueViewModel catalogueCreated = new CatalogueViewModel(SelectedCatalog.Category, (int)SelectedCatalog.CatId);

                if (TreeViewCatalogue.SelectedItem != null)
                {
                    CatalogueViewModel cat = (CatalogueViewModel)TreeViewCatalogue.SelectedItem;

                    cat.Childrens.Add(catalogueCreated);
                }
                else
                {
                    CatalogueItems[0].Childrens.Add(catalogueCreated);
                }


                LoadCategoryLangue();
            }
            else
            {
                SelectedCatalog = RetroCatalogue;
            }
        }
        private void btnShowTreeView_Click(object sender, RoutedEventArgs e)
        {
            popup.IsOpen = !popup.IsOpen;
        }

        private void TreeViewComboBox_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            var catalogueSage = ((TreeView)sender).SelectedItem as F_CATALOGUE;
            btnShowTreeView.Content = catalogueSage.Intitule;
            SelectedCatalogLink = catalogueSage;
            SelectedCatalog.SageId = catalogueSage.CbMarq;
        }

        private void TextBlock_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {

            CatalogueViewModel catalog = ((TextBlock)sender).DataContext as CatalogueViewModel;
            // Créer un context menu
            ContextMenu contextMenu = new ContextMenu();

            // Ajouter un item au context menu
            MenuItem AddCatalog = new MenuItem();
            AddCatalog.Header = $"Ajouter catalogue dans : {catalog.cat_name}";
            AddCatalog.Tag = catalog;
            AddCatalog.Click += AddCatalog_Click;
            contextMenu.Items.Add(AddCatalog);

            // Afficher le context menu au point du clic
            contextMenu.IsOpen = true;
            e.Handled = true; // Pour éviter que le menu contextuel par défaut ne s'ouvre
        }

        private void AddCatalog_Click(object sender, RoutedEventArgs e)
        {
            // Accéder à la valeur de catalog.Cat_Name à partir du Tag de l'item
            MenuItem menuItem = sender as MenuItem;
            if (menuItem != null)
            {
                CreateCatalogue();
            }
        }
        #endregion


    }
}
