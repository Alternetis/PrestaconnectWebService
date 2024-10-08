using Bukimedia.PrestaSharp.Factories;
using PrestaconnectWebService.Core;
using PrestaconnectWebService.Model.Prestaconnect.Repository;
using PrestaconnectWebService.Model.Prestashop;
using PrestaconnectWebService.Model.Sage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace PrestaconnectWebService.ViewModel
{
    public class CustomerViewModel : Model.Prestaconnect.Repository.CustomerRepository
    {
            public string id_customer { get; set; }
            public string client { get => lastname + " " + firstname; }
            public string firstname { get; set; }
            public string lastname { get; set; }
            public string email { get; set; }
            public string company { get; set; }
            public string SageAccount { get; set; }
            public DateTime date_add { get; set; }
            public bool visualiserEnCours { get; set; }
            public bool visualiserFactures { get; set; }

            public CustomerViewModel() { }

            public CustomerViewModel(Bukimedia.PrestaSharp.Entities.customer customer)
            {
                this.id_customer = customer.id.ToString();
                this.firstname = customer.firstname;
                this.lastname = customer.lastname;
                this.email = customer.email;
                this.company = customer.company;
                this.date_add = DateTime.Parse(customer.date_add);
                this.SageAccount = Model.Prestashop.CustomerRepository.SearchClientPCById(int.Parse(customer.id.ToString()));
                //TODO le visualiser
            }

        /* Récuperation client Prestashop*/
        public List<CustomerViewModel> GetClient(int Top)
        {
            return Model.Prestashop.CustomerRepository.GetClient(Top);
        }


    }
    
}
