using PrestaconnectWebService.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Xceed.Wpf.Toolkit;

namespace PrestaconnectWebService.View.Gamme
{
    /// <summary>
    /// Logique d'interaction pour GammeAttributView.xaml
    /// </summary>
    public partial class GammeAttributView : Window
    {

        private Bukimedia.PrestaSharp.Entities.product_option_value PsAttribut = new Bukimedia.PrestaSharp.Entities.product_option_value();
        private Bukimedia.PrestaSharp.Entities.language languagePs = new Bukimedia.PrestaSharp.Entities.language();

        public GammeAttributView(Bukimedia.PrestaSharp.Entities.product_option SelectedGroupAttribut, int Position, bool Color)
        {
            InitializeComponent();
            this.Title = "Ajout d'un attribut pour le groupe d'attribut " + SelectedGroupAttribut.name[Position].Value;
            PsAttribut.id_attribute_group = SelectedGroupAttribut.id;

            Bukimedia.PrestaSharp.Factories.LanguageFactory languageFactory = new Bukimedia.PrestaSharp.Factories.LanguageFactory(Global.Auth.BaseUrl, Global.Auth.Account, "");
            var AllLangue = languageFactory.GetAll();

            Update.Visibility = Visibility.Hidden;

            languagePs = AllLangue[0];
            SelectionLangue.ItemsSource = AllLangue;
            SelectionLangue.SelectedItem = languagePs;


            foreach (Bukimedia.PrestaSharp.Entities.language language in AllLangue)
            {
                Bukimedia.PrestaSharp.Entities.AuxEntities.language languageAux = new Bukimedia.PrestaSharp.Entities.AuxEntities.language();
                languageAux.id = (long)language.id;
                languageAux.Value = "";
                PsAttribut.name.Add(languageAux);
            }

            if (!Color)
            {
                stackPanelColor.Visibility = Visibility.Collapsed;
                this.Height -= 50;
            }
        }

        public GammeAttributView(Bukimedia.PrestaSharp.Entities.product_option_value SelectedAttribut, int Position, bool Color)
        {
            InitializeComponent();
            this.Title = $"Modification de l'attribut {SelectedAttribut.name[Position].Value}";

            Bukimedia.PrestaSharp.Factories.LanguageFactory languageFactory = new Bukimedia.PrestaSharp.Factories.LanguageFactory(Global.Auth.BaseUrl, Global.Auth.Account, "");
            var AllLangue = languageFactory.GetAll();

            Create.Visibility = Visibility.Hidden;

            languagePs = AllLangue[0];
            SelectionLangue.ItemsSource = AllLangue;
            SelectionLangue.SelectedItem = languagePs;

            PsAttribut = SelectedAttribut;

            TbName.Text = PsAttribut.name.FirstOrDefault(l => l.id == languagePs.id)?.Value;

            if (!Color)
            {
                stackPanelColor.Visibility = Visibility.Collapsed;
                this.Height -= 50;
            }
            else
            {
                TbHexColor.Text = PsAttribut.color.ToString();
                colorPicker.SelectedColor = (Color)ColorConverter.ConvertFromString(TbHexColor.Text);
            }
        }

        private void ColorPicker_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            if (e.NewValue.HasValue)
            {
                Color selectedColor = e.NewValue.Value;
                TbHexColor.Text = selectedColor.ToString(); // Met à jour le TextBox avec la valeur hexadécimale de la couleur sélectionnée
            }
        }

        private void ApplyColorButton_Click(object sender, RoutedEventArgs e)
        {
            // Récupère la valeur hexadécimale saisie dans le TextBox
            string hexValue = TbHexColor.Text.Trim();

            // Convertit la valeur hexadécimale en couleur
            Color color;
            try
            {
                color = (Color)ColorConverter.ConvertFromString(hexValue);
            }
            catch
            {
                System.Windows.MessageBox.Show("Valeur hexadécimale invalide.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Applique la couleur sélectionnée dans le ColorPicker
            colorPicker.SelectedColor = color;
        }

        private void CreateAttribut_Click(object sender, RoutedEventArgs e)
        {
            PsAttribut.color = TbHexColor.Text;
            try
            {
                Bukimedia.PrestaSharp.Factories.ProductOptionValueFactory productFeatureValueFactory = new Bukimedia.PrestaSharp.Factories.ProductOptionValueFactory(Global.Auth.BaseUrl, Global.Auth.Account, "");
                productFeatureValueFactory.Add(PsAttribut);
            }
            catch
            {
                MessageInformation.Show("Lors de l'ajout de l'attribut","Erreur");
            }

        }

        private void UpdateAttribut_Click(object sender, RoutedEventArgs e)
        {
            PsAttribut.color = TbHexColor.Text;
            try
            {
                Bukimedia.PrestaSharp.Factories.ProductOptionValueFactory productFeatureValueFactory = new Bukimedia.PrestaSharp.Factories.ProductOptionValueFactory(Global.Auth.BaseUrl, Global.Auth.Account, "");
                productFeatureValueFactory.Update(PsAttribut);
            }
            catch
            {
                MessageInformation.Show("Lors de la modification de l'attribut", "Erreur");
            }

        }


        private void SelectionLangue_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(sender != null)
            {
                languagePs = ((System.Windows.Controls.ComboBox)sender).SelectedItem as Bukimedia.PrestaSharp.Entities.language;

                TbName.Text = PsAttribut.name.FirstOrDefault(l => l.id == languagePs.id)?.Value;
            }    
        }

        private void TbName_TextChanged(object sender, TextChangedEventArgs e)
        {
            var Name = PsAttribut.name.FirstOrDefault(l => l.id == languagePs.id);
            Name.Value = TbName.Text;
        }
    }
}
