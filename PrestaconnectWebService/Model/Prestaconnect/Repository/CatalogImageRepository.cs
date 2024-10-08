using PrestaconnectWebService.Model.Prestaconnect.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrestaconnectWebService.Model.Prestaconnect.Repository
{
    public class CatalogImageRepository
    {
        private PrestaconnectDB DBLocal = new PrestaconnectDB();

        public void Add(CatalogImage Obj)
        {
            DBLocal.CatalogImage.Add(Obj);
            Save();
        }

        public void Save()
        {
            DBLocal.SaveChanges();
        }

        public void Delete(CatalogImage Obj)
        {
            DBLocal.CatalogImage.Remove(Obj);
            Save();
        }

        public CatalogImage Get(int id)
        {
            CatalogImage catalogImage = DBLocal.CatalogImage.Where(Ci => Ci.Cat_Id == id).FirstOrDefault();

            return catalogImage;
        }
    }
}
