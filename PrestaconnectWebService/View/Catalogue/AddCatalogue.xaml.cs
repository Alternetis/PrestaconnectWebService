using PrestaconnectWebService.Core;
using PrestaconnectWebService.Model.Prestaconnect.Class;
using PrestaconnectWebService.Model.Sage;
using PrestaconnectWebService.ViewModel;
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

namespace PrestaconnectWebService.View.Catalogue
{
    /// <summary>
    /// Logique d'interaction pour AddCatalogue.xaml
    /// </summary>
    public partial class AddCatalogue : Window
    {

        public CatalogueSelectedViewModel catalogueSelected { get; set; }
        public Bukimedia.PrestaSharp.Entities.language languagePs { get; set; }
        public F_CATALOGUE SelectedCatalogLink { get; set; }
        public AddCatalogue(CatalogueSelectedViewModel catalogue)
        {
            InitializeComponent();

            catalogueSelected = catalogue;

            Bukimedia.PrestaSharp.Factories.LanguageFactory languageFactory = new Bukimedia.PrestaSharp.Factories.LanguageFactory(Global.Auth.BaseUrl, Global.Auth.Account, "");
            var AllLang = languageFactory.GetAll();
            languageCombobox.ItemsSource = AllLang;
            languagePs = AllLang[0];


            foreach (var lang in AllLang)
            {
                Bukimedia.PrestaSharp.Entities.AuxEntities.language langage = new Bukimedia.PrestaSharp.Entities.AuxEntities.language();
                langage.id = (long)lang.id;
                langage.Value = "New Catalogue";
                catalogueSelected.Category.AddName(langage);
                catalogueSelected.Category.AddDescription(langage);
                catalogueSelected.Category.AddMetaTitle(langage);
                catalogueSelected.Category.AddMetaDescription(langage);
                catalogueSelected.Category.AddMetaKeywords(langage);
                catalogueSelected.Category.AddLinkRewrite(langage);

            }

            languageCombobox.SelectedItem = AllLang[0];

            //List<F_CATALOGUE> catalogueSage = F_CATALOGUE.GetCatalogue();
            TreeViewComboBox.ItemsSource = F_CATALOGUE.GetCatalogue();
        }

        public static CatalogueSelectedViewModel Show(CatalogueSelectedViewModel catalogue)
        {
            AddCatalogue CatalogueView = new AddCatalogue(catalogue);
            CatalogueView.ShowDialog();

            return CatalogueView.catalogueSelected;
        }

        private void PrestashopName_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (catalogueSelected != null)
            {
                var Name = catalogueSelected.Category.name.FirstOrDefault(l => l.id == languagePs.id);
                Name.Value = PrestashopName.Text;
            }
        }

        private void languageCombobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            languagePs = ((ComboBox)sender).SelectedItem as Bukimedia.PrestaSharp.Entities.language;

            loadName();
        }

        private void loadName()
        {
            PrestashopName.Text = catalogueSelected.Category.name.FirstOrDefault(l => l.id == languagePs.id)?.Value;
        }

        private void valider_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void annuler_Click(object sender, RoutedEventArgs e)
        {
            catalogueSelected.Category = null;
            Close();
        }

        private void CopyAllLangue_Click(object sender, RoutedEventArgs e)
        {
            int variable = 0;
            foreach(Bukimedia.PrestaSharp.Entities.language langue in languageCombobox.Items)
            {
                catalogueSelected.Category.name[variable].Value = PrestashopName.Text;
                catalogueSelected.Category.description[variable].Value = PrestashopName.Text;
                catalogueSelected.Category.meta_title[variable].Value = PrestashopName.Text;
                catalogueSelected.Category.meta_description[variable].Value = PrestashopName.Text;
                catalogueSelected.Category.meta_keywords[variable].Value = PrestashopName.Text;
                catalogueSelected.Category.link_rewrite[variable].Value= PrestashopName.Text;
                variable++;
            }
        }

        private void CopyNameCatalogue_Click(object sender, RoutedEventArgs e)
        {
            var Name = catalogueSelected.Category.name.FirstOrDefault(l => l.id == languagePs.id);
            Name.Value = SelectedCatalogLink.Intitule;
            var Descirption = catalogueSelected.Category.description.FirstOrDefault(l => l.id == languagePs.id);
            Descirption.Value = SelectedCatalogLink.Intitule;
            var meta_title = catalogueSelected.Category.meta_title.FirstOrDefault(l => l.id == languagePs.id);
            meta_title.Value = SelectedCatalogLink.Intitule;
            var meta_description = catalogueSelected.Category.meta_description.FirstOrDefault(l => l.id == languagePs.id);
            meta_description.Value = SelectedCatalogLink.Intitule;
            var meta_keywords = catalogueSelected.Category.meta_keywords.FirstOrDefault(l => l.id == languagePs.id);
            meta_keywords.Value = SelectedCatalogLink.Intitule;
            var link_rewrite = catalogueSelected.Category.link_rewrite.FirstOrDefault(l => l.id == languagePs.id);
            link_rewrite.Value = SelectedCatalogLink.Intitule;

            loadName();
        }

        private void TreeViewComboBox_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            var catalogueSage = ((TreeView)sender).SelectedItem as F_CATALOGUE;
            btnShowTreeView.Content = catalogueSage.Intitule;
            SelectedCatalogLink = catalogueSage;
            catalogueSelected.SageId = SelectedCatalogLink.CbMarq;
        }

        private void btnShowTreeView_Click(object sender, RoutedEventArgs e)
        {
            popup.IsOpen = !popup.IsOpen;
        }
    }
}
