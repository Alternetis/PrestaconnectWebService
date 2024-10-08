using PrestaconnectWebService.Core;
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
using static System.Net.Mime.MediaTypeNames;
using System.Xml.Linq;

namespace PrestaconnectWebService.View.Articles
{
    /// <summary>
    /// Logique d'interaction pour Articles.xaml
    /// </summary>
    public partial class ArticlesView : Window
    {

        public ObservableCollection<CatalogueViewModel> CatalogueItems { get; set; }
        public ViewModel.Articles.ArticlesViewModel ArticlesViewModel = new ViewModel.Articles.ArticlesViewModel();

        public ArticlesView()
        {
            InitializeComponent();
            this.DataContext = ArticlesViewModel;
            LoadShopCombo();
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
            ShopSelected.SelectedItem = Global.shopSelected;
        }
        private void ShopSelected_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Global.ChangeShop((Bukimedia.PrestaSharp.Entities.shop)ShopSelected.SelectedItem);
            CatalogueViewModel catalogue = new CatalogueViewModel();

            CatalogueItems = catalogue.GetCatalogues();
            TreeViewCatalogue.ItemsSource = null;      

            TreeViewCatalogue.ItemsSource = CatalogueItems;
        }
        #endregion

        private void Catalogue_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            
            var catalogueView = ((TreeView)sender).SelectedItem as CatalogueViewModel;
            ArticlesViewModel.SelectedCatalogue = catalogueView;

            if (ArticlesViewModel.SelectedCatalogue != null)
            {
                Model.Prestaconnect.Repository.ArticlesRepository articleRepository = new Model.Prestaconnect.Repository.ArticlesRepository();
                ArticlesGrid.ItemsSource = articleRepository.GetArticleCategory((int)ArticlesViewModel.SelectedCatalogue.cat_id);
            }
            else
            {
                ArticlesGrid.ItemsSource = null;
            }
        }

        private void Catalogue_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
