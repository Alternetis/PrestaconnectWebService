using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrestaconnectWebService.ViewModel.Articles
{
    public class ArticlesViewModel: INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private ObservableCollection<ArticlesViewModel> _articlesCatalogue;
        private ObservableCollection<ArticlesViewModel> ArticlesCatalogue
        {
            get => _articlesCatalogue;
            set
            {
                if (_articlesCatalogue != value)
                {
                    _articlesCatalogue = value;
                    OnPropertyChanged(nameof(ArticlesCatalogue));
                }
            }
        }

        private CatalogueViewModel _selectedCatalogue;
        public CatalogueViewModel SelectedCatalogue
        {
            get => _selectedCatalogue;
            set
            {
                if (_selectedCatalogue != value)
                {
                    _selectedCatalogue = value;
                    OnPropertyChanged(nameof(SelectedCatalogue));
                }
            }
        }



        public ArticlesViewModel() { }
    }
}
