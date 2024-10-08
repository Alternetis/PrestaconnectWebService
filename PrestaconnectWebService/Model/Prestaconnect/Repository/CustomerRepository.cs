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
    public class CustomerRepository
    {

        private PrestaconnectDB DBLocal = new PrestaconnectDB();

        public void Add(Customer Obj)
        {
            DBLocal.Customer.Add(Obj);
           
        }

        public void Save()
        {
            DBLocal.SaveChanges();
        }
        public void Delete(Customer Obj)
        {
            DBLocal.Customer.Remove(Obj);
            Save();
        }

        public bool ExistPrestashop(int Prestashop)
        {
            return DBLocal.Customer.Any(Obj => Obj.Pre_Id == Prestashop);
        }

        public bool ExistSage(int Sage)
        {
            return DBLocal.Customer.Any(Obj => Obj.Sag_Id == Sage);
        }

        public Customer ReadPrestashop(int Prestashop)
        {
            return DBLocal.Customer.FirstOrDefault(Obj => Obj.Pre_Id == Prestashop);
        }

        public Customer ReadSage(int Sage)
        {
            return DBLocal.Customer.FirstOrDefault(Obj => Obj.Sag_Id == Sage);
        }

        public List<Customer> ReadListSage(int Sage)
        {
            return DBLocal.Customer.Where(Obj => Obj.Sag_Id == Sage).ToList();
        }

        public bool ExistPrestashopSage(int Prestashop, int Sage)
        {
            return DBLocal.Customer.Any(Obj => Obj.Pre_Id == Prestashop && Obj.Sag_Id == Sage);
        }

        public Customer ReadPrestashopSage(int Prestashop, int Sage)
        {
            return DBLocal.Customer.FirstOrDefault(Obj => Obj.Pre_Id == Prestashop && Obj.Sag_Id == Sage);
        }

        public List<Customer> List()
        {
            return DBLocal.Customer.ToList();
        }

        public List<Customer> ListSansCentralisateur(int? Centralisateur)
        {
            return DBLocal.Customer.Where(obj => obj.Sag_Id != (Centralisateur ?? 0) && obj.VisualiserEnCours).ToList();
        }

        //public HashSet<int> ListPreIds()
        //{
        //    return DBLocal.Customer.Select(c => c.Pre_Id).ToHashSet();
        //}

    }
}
