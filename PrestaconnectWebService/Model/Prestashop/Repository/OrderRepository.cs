using Bukimedia.PrestaSharp.Factories;
using PrestaconnectWebService.Core;
using PrestaconnectWebService.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrestaconnectWebService.Model.Prestashop
{
    public class OrderRepository
    {

        public static Bukimedia.PrestaSharp.Factories.OrderFactory orderFactory = new OrderFactory(Global.Auth.BaseUrl, Global.Auth.Account, "");
        public static List<OrderViewModel> ReadOrderResume(int TOP)
        {

            Dictionary<string, string> filters = new Dictionary<string, string>();
            //filters.Add("id_lang", "1");
            filters.Add("id_shop_group", Global.shopGroupSelected.id.ToString());
            if (Global.shopSelected.id != null)
            {
                filters.Add("id_shop", Global.shopSelected.id.ToString());
            }


            List<Bukimedia.PrestaSharp.Entities.order> PSorders = orderFactory.GetByFilter(filters, null, TOP.ToString());

            List<OrderViewModel> orderResumes = new List<OrderViewModel>();
            foreach (var PSorder in PSorders)
            {
                OrderViewModel orderResume = new OrderViewModel(PSorder);
                orderResumes.Add(orderResume);
            }

            return orderResumes;
        }
    }
}
