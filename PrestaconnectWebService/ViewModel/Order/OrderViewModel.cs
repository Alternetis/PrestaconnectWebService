using Bukimedia.PrestaSharp.Factories;
using PrestaconnectWebService.Core;
using PrestaconnectWebService.Model.Prestaconnect.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace PrestaconnectWebService.ViewModel
{
    public class OrderViewModel : OrderRepository
    {
        public string id_order { get; set; }
        public string reference { get; set; }
        public long current_state { get; set; }
        public long id_customer { get; set; }

        public string do_piece
        {
            get
            {
                return Model.Sage.F_DOCENTETE.ExistPieceWeb(id_order);
            }
        }

        public string client { get => lastname + " " + firstname; }
        public string lastname { get; set; }
        public string firstname { get; set; }
        public string company { get; set; }
        public decimal total_paid_tax_incl { get; set; }
        public decimal total_paid_tax_excl { get; set; }
        public string payment { get; set; }
        public string order_state_name { get; set; }
        public DateTime date_add { get; set; }

          public bool sync
        {
            get
            {
                return do_piece != "" ? true : false;
            }
        }

            public OrderViewModel() { }
            public OrderViewModel(Bukimedia.PrestaSharp.Entities.order PSorder)
            {
                Dictionary<string, string> filters = new Dictionary<string, string>();

                id_order = PSorder.id.ToString();
                reference = PSorder.reference;
                total_paid_tax_excl = PSorder.total_paid_tax_excl;
                total_paid_tax_incl = PSorder.total_paid_tax_incl;
                payment = PSorder.payment;
                date_add = DateTime.Parse(PSorder.date_add);

                // Faire une recherche dans Order_state_lang
                current_state = (long)PSorder.current_state;

                Bukimedia.PrestaSharp.Factories.OrderStateFactory orderSateFactory = new OrderStateFactory(Global.Auth.BaseUrl, Global.Auth.Account, "");
                Bukimedia.PrestaSharp.Entities.order_state state = orderSateFactory.Get(current_state);
                order_state_name = state.name[0].Value.ToString();


                // faire une recherche dans Customer
                id_customer = (long)PSorder.id_customer;

                Bukimedia.PrestaSharp.Factories.CustomerFactory customerFactory = new CustomerFactory(Global.Auth.BaseUrl, Global.Auth.Account, "");
                Bukimedia.PrestaSharp.Entities.customer customer = customerFactory.Get(id_customer);
                this.firstname = customer.firstname;
                this.lastname = customer.lastname;
            }

        public List<OrderViewModel> ReadOrderResume(int TOP)
        {
            return Model.Prestashop.OrderRepository.ReadOrderResume(TOP);
        }




    }
}
