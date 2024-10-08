using Bukimedia.PrestaSharp.Factories;
using PrestaconnectWebService.Core;
using PrestaconnectWebService.Model.Prestaconnect.Repository;
using PrestaconnectWebService.Model.Sage;
using PrestaconnectWebService.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrestaconnectWebService.Model.Prestashop
{
    public class CustomerRepository
    {
        public static Bukimedia.PrestaSharp.Factories.CustomerFactory customerFactory = new CustomerFactory(Global.Auth.BaseUrl, Global.Auth.Account, "");

        public static List<CustomerViewModel> GetClient(int TOP)
        {

            Dictionary<string, string> filters = new Dictionary<string, string>();
            //filters.Add("id_lang", "1");
            filters.Add("id_shop_group", Global.shopGroupSelected.id.ToString());
            if (Global.shopSelected.id != null)
            {
                filters.Add("id_shop", Global.shopSelected.id.ToString());
            }


            List<Bukimedia.PrestaSharp.Entities.customer> Pscustomers = customerFactory.GetByFilter(filters, "date_add_DESC", TOP.ToString());

            List<CustomerViewModel> customers = new List<CustomerViewModel>();
            foreach (var Pscustomer in Pscustomers)
            {
                CustomerViewModel customer = new CustomerViewModel(Pscustomer);
                customers.Add(customer);

            }
            return customers;
        }

        public static string SearchClientPCById(int id)
        {
            Prestaconnect.Repository.CustomerRepository customerRepository = new Prestaconnect.Repository.CustomerRepository();
            if (customerRepository.ExistPrestashop(id))
            {
                Prestaconnect.Class.Customer customer = customerRepository.ReadPrestashop(id);
                return F_COMPTET.SearchComboTextByCbmarq(customer.Sag_Id); ;
            }
            else
            {
                return "";
            }

        }
    }
}
