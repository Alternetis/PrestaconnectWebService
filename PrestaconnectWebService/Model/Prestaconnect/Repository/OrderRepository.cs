using Bukimedia.PrestaSharp.Factories;
using PrestaconnectWebService.Core;
using PrestaconnectWebService.Model.Prestaconnect.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrestaconnectWebService.Model.Prestaconnect.Repository
{
    public class OrderRepository
    {
        private PrestaconnectDB DBLocal = new PrestaconnectDB();

        public void Add(Order Obj)
        {
            DBLocal.Order.Add(Obj);

        }

        public void Save()
        {
            DBLocal.SaveChanges();
        }
        public void Delete(Order Obj)
        {
            DBLocal.Order.Remove(Obj);
            Save();
        }

        public bool ExistId(int Id)
        {
            return DBLocal.Order.Any(Obj => Obj.Pre_Id == Id);
        }

        public Order ReadId(int Id)
        {
            return DBLocal.Order.FirstOrDefault(Obj => Obj.Pre_Id == Id);
        }

        public List<Order> List()
        {
            return DBLocal.Order.ToList();
        }

    }
}
