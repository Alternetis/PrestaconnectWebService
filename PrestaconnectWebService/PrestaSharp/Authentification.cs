using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PrestaconnectWebService.PrestaSharp;

namespace PrestaconnectWebService.PrestaSharp
{
    public class Authentification
    {
        public string BaseUrl { get => Properties.Settings.Default.UrlPrestashop+"/api";}
        public string Account { get => Properties.Settings.Default.PrestashopAccount;}
    }
}
