using PrestaconnectWebService.Converters;
using PrestaconnectWebService.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Shapes;
using Binding = System.Windows.Data.Binding;

namespace PrestaconnectWebService.View.Gamme
{
    /// <summary>
    /// Logique d'interaction pour GammeView.xaml
    /// </summary>
    public partial class GammeView : Window
    {

        public Bukimedia.PrestaSharp.Entities.product_option SelectedPsAttributeGroup = null;
        public Bukimedia.PrestaSharp.Entities.product_option_value SelectedPsAttribute = null;
        public int Position =0;

        public GammeView()
        {
            InitializeComponent();

            Bukimedia.PrestaSharp.Factories.ProductOptionFactory productOptionFactory = new Bukimedia.PrestaSharp.Factories.ProductOptionFactory(Global.Auth.BaseUrl, Global.Auth.Account, "");
            List<Bukimedia.PrestaSharp.Entities.product_option> productOptions = productOptionFactory.GetAll();


            Bukimedia.PrestaSharp.Factories.LanguageFactory language = new Bukimedia.PrestaSharp.Factories.LanguageFactory(Global.Auth.BaseUrl, Global.Auth.Account, "");
            var lang = language.GetAll();

            Bukimedia.PrestaSharp.Entities.language languagePs = lang[0];

            listBoxPsAttributeGroup.ItemsSource = productOptions;

            SelectionLangue.ItemsSource = lang;
            SelectionLangue.SelectedItem = languagePs;
        }

        private void SelectionLangue_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            Bukimedia.PrestaSharp.Entities.language languagePs = (Bukimedia.PrestaSharp.Entities.language)SelectionLangue.SelectedItem;
            if(listBoxPsAttributeGroup != null)
            {
                Bukimedia.PrestaSharp.Entities.product_option OptionGroupeAttribut = (Bukimedia.PrestaSharp.Entities.product_option)listBoxPsAttributeGroup.Items[0];

                for (int i = 0; i <= OptionGroupeAttribut.name.Count; i++)
                {
                    if (OptionGroupeAttribut.name[i].id == languagePs.id)
                    {
                        Position = i; // La position est définie sur l'index actuel si l'ID correspond
                        break; // Sortir de la boucle une fois que l'ID est trouvé
                    }
                }

                listBoxPsAttributeGroup.DisplayMemberPath = $"name[{Position}].Value";
                //ListBoxPsAttribute.DisplayMemberPath = $"name[{Position}].Value";
                if(listBoxPsAttributeGroup.SelectedItem != null)
                {
                    listBoxPsAttributeGroup_SelectionChanged(null, null);
                }



            }
            if (SelectedPsAttributeGroup != null)
            {
                LabelGroupAttribut.Content = SelectedPsAttributeGroup.name[Position].Value;
            }

        }

        private void listBoxPsAttributeGroup_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(listBoxPsAttributeGroup.SelectedItems != null)
            {
                Bukimedia.PrestaSharp.Entities.product_option product_Option = (Bukimedia.PrestaSharp.Entities.product_option)listBoxPsAttributeGroup.SelectedItems[0];
                SelectedPsAttributeGroup = product_Option;
                SelectedPsAttribute = null;

                GroupBoxAttribut.IsEnabled = true;
                LabelGroupAttribut.Content = SelectedPsAttributeGroup.name[Position].Value;

                Dictionary<string, string> filter = new Dictionary<string, string>
                {
                    { "id_attribute_group", $"{product_Option.id}" }
                };
                Bukimedia.PrestaSharp.Factories.ProductOptionValueFactory productOptionValueFactory = new Bukimedia.PrestaSharp.Factories.ProductOptionValueFactory(Global.Auth.BaseUrl, Global.Auth.Account, "");
                List<Bukimedia.PrestaSharp.Entities.product_option_value> productOptionsValue = productOptionValueFactory.GetByFilter(filter,null,null);


                DataTemplate dataTemplate = new DataTemplate();

                // Création du Border pour encapsuler le StackPanel
                FrameworkElementFactory borderFactory = new FrameworkElementFactory(typeof(Border));
                borderFactory.SetValue(Border.BorderThicknessProperty, new Thickness(0));
                borderFactory.SetValue(Border.BorderBrushProperty, System.Windows.Media.Brushes.Transparent);
                borderFactory.SetValue(Border.BackgroundProperty, System.Windows.Media.Brushes.Transparent);


                // Définition du modèle de l'élément ListBox

                FrameworkElementFactory stackPanelFactory = new FrameworkElementFactory(typeof(StackPanel));
                stackPanelFactory.SetValue(StackPanel.OrientationProperty, System.Windows.Controls.Orientation.Horizontal);
                stackPanelFactory.SetValue(StackPanel.HorizontalAlignmentProperty, HorizontalAlignment.Stretch);
                stackPanelFactory.SetValue(StackPanel.VerticalAlignmentProperty, VerticalAlignment.Stretch);


                // Ajout du rectangle pour afficher la couleur
                FrameworkElementFactory rectangleFactory = new FrameworkElementFactory(typeof(Rectangle));
                rectangleFactory.SetBinding(Rectangle.FillProperty, new System.Windows.Data.Binding("color") { Converter = new StringToBrushConverter() }); // Converter à créer
                rectangleFactory.SetValue(Rectangle.WidthProperty, 20.0);
                rectangleFactory.SetValue(Rectangle.HeightProperty, 20.0);
                rectangleFactory.SetValue(Rectangle.MarginProperty, new Thickness(5));
                rectangleFactory.SetBinding(Rectangle.VisibilityProperty, new System.Windows.Data.Binding("color") { Converter = new StringToVisibilityConverter() }); // Converter à créer
                stackPanelFactory.AppendChild(rectangleFactory);

                // Ajout du TextBlock pour afficher le nom
                FrameworkElementFactory textBlockFactory = new FrameworkElementFactory(typeof(TextBlock));
                textBlockFactory.SetBinding(TextBlock.TextProperty, new System.Windows.Data.Binding($"name[{Position}].Value"));
                textBlockFactory.SetValue(TextBlock.VerticalAlignmentProperty, VerticalAlignment.Center);
                stackPanelFactory.AppendChild(textBlockFactory);



                // Ajouter le StackPanel au Border
                borderFactory.AppendChild(stackPanelFactory);

                dataTemplate.VisualTree = borderFactory;
                ListBoxPsAttribute.ItemTemplate = dataTemplate;

                ListBoxPsAttribute.ItemsSource = productOptionsValue;

            }
        }

        private void ListBoxPsAttribute_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(ListBoxPsAttribute.SelectedItem != null)
            {
                SelectedPsAttribute = (Bukimedia.PrestaSharp.Entities.product_option_value)ListBoxPsAttribute.SelectedItem;
                GroupBoxUpdateAttribut.IsEnabled = true;
            }
            else
            {
                SelectedPsAttribute = null;
                GroupBoxUpdateAttribut.IsEnabled = false;
            }
        }

        private void BtnCreatePsAttribute_Click(object sender, RoutedEventArgs e)
        {
            View.Gamme.GammeAttributView gammeAttributView = new View.Gamme.GammeAttributView(SelectedPsAttributeGroup, Position,SelectedPsAttributeGroup.is_color_group == 1);
            gammeAttributView.ShowDialog();
        }

        private void BtnUpdatePsAttributeGroup_Click(object sender, RoutedEventArgs e)
        {
            View.Gamme.GammeAttributView gammeAttributView = new View.Gamme.GammeAttributView(SelectedPsAttribute, Position, SelectedPsAttributeGroup.is_color_group == 1);
            gammeAttributView.ShowDialog();
        }
    }
}
