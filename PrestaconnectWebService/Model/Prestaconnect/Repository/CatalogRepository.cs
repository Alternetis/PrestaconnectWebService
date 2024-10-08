using PrestaconnectWebService.Core;
using PrestaconnectWebService.Model.Prestaconnect.Class;
using PrestaconnectWebService.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrestaconnectWebService.Model.Prestaconnect.Repository
{
    public class CatalogRepository
    {
        private PrestaconnectDB DBLocal = new PrestaconnectDB();

        public void Add(Catalog Obj)
        {
            DBLocal.Catalog.Add(Obj);
            Save();
        }

        public void Add(CatalogueSelectedViewModel Obj)
        {
            Catalog catalog = new Catalog(Obj);
            DBLocal.Catalog.Add(catalog);
            Save();
        }


        public void Save()
        {
            DBLocal.SaveChanges();
        }

        public void Delete(Catalog Obj)
        {
            DBLocal.Catalog.Remove(Obj);
            Save();
        }

        public void Update(CatalogueSelectedViewModel Obj)
        {
            Catalog catalog = new Catalog(Obj);
            DBLocal.Catalog.AddOrUpdate(catalog);
            Save();
        }

        public List<Catalog> ListRoot()
        {
            return (List<Catalog>)DBLocal.Catalog.Where(C => C.Cat_Level == 2).ToList();
        }

        public List<Catalog> ListEnfant(int catParent)
        {
            return (List<Catalog>)DBLocal.Catalog.Where(C => C.Cat_Parent == catParent).ToList() ;
        }

        public Catalog GetCatalog(long preId,long? shopId)
        {
                return DBLocal.Catalog.FirstOrDefault(C => C.Pre_Id == preId && ( C.Id_Shop == shopId || C.Id_Shop == null));
        }

    }
}
