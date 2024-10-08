using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace PrestaconnectWebService.View.Annexes
{
    /// <summary>
    /// Logique d'interaction pour HtmlContent.xaml
    /// </summary>
	public partial class HTMLContent : UserControl, INotifyPropertyChanged
    {
        public HTMLContent()
        {
            InitializeComponent();
            //if (Core.Global.GetConfig().UIDisabledWYSIWYG)
            //{
            TabItemWYSIWYG.Visibility = Visibility.Visible;
            //buttonInsertHTML.Visibility = Visibility.Collapsed;
            //buttonLoadHTML.Visibility = Visibility.Collapsed;
            //buttonLoadDbHTML.Visibility = Visibility.Collapsed;
            //}
            //else
            //{
            //    TabItemWYSIWYG.IsSelected = true;
            //}
        }

        private string _BDDHtmlContent;
        public string BDDHtmlContent
        {
            get
            {
                return _BDDHtmlContent;
            }
            set
            {
                _BDDHtmlContent = value;
                HtmlContent = _BDDHtmlContent;
                //if (TabItemWYSIWYG.Visibility == Visibility.Visible)
                    EditeurHtml.DocumentHTML = HtmlContent;
                //else
                    TextBoxInsertHTML.Text = HtmlContent;
            }
        }

        private string _htmlContent;
        public string HtmlContent
        {
            get
            {
                return _htmlContent;
            }
            set
            {
                _htmlContent = value;
                OnPropertyChanged("HtmlContent");
            }
        }

        public string HtmlContentNoP
        {
            get
            {
                string r = HtmlContent;
                if (r.StartsWith("<p>"))
                    r = r.Remove(0, 3);
                if (r.EndsWith("</p>"))
                    r = r.Remove(r.Length - 4);
                return r;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        #region Fonctions

        private void TabItemHTMLEdit_LostFocus(object sender, RoutedEventArgs e)
        {
            HtmlContent = TextBoxInsertHTML.Text;
            if (TabItemWYSIWYG.Visibility == Visibility.Visible)
                EditeurHtml.DocumentHTML = HtmlContent;
        }

        private void TabItemWYSIWYG_LostFocus(object sender, RoutedEventArgs e)
        {
            HtmlContent = EditeurHtml.DocumentHTML;
        }

        private void TabItemWYSIWYG_GotFocus(object sender, RoutedEventArgs e)
        {
            EditeurHtml.DocumentHTML = HtmlContent;
        }

        private void TabItem_GotFocus(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(HtmlContent))
            {
                ViewWebBrowser.NavigateToString(HtmlContent);
            }
            else
            {
                ViewWebBrowser.NavigateToString("<html></html>");
            }
        }
        #endregion

        private void TabItemHTMLEdit_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBoxInsertHTML.Text = HtmlContent;
        }
    }
}
