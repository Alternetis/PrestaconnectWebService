using PrestaconnectWebService.Model.Prestaconnect.Class;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrestaconnectWebService.Model.Prestaconnect.Repository
{
    public class ArticlesRepository
    {
        private PrestaconnectDB DBLocal = new PrestaconnectDB();
        public void Add(Article Obj)
        {
            DBLocal.Article.Add(Obj);
            Save();
        }

        public void Save()
        {
            DBLocal.SaveChanges();
        }

        public void Delete(Article Obj)
        {
            DBLocal.Article.Remove(Obj);
            Save();
        }

        public List<Article> GetArticleCategory( int categoryId)
        {
            List<Article> articles = DBLocal.Article.Where(a => a.Cat_Id == categoryId).ToList();
           // ObservableCollection<Article> articleObservable = new ObservableCollection<Article>(articles);
            return articles;
        }
    }
}
