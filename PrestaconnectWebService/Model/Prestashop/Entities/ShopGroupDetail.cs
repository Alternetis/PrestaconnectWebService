using Bukimedia.PrestaSharp.Factories;
using PrestaconnectWebService.Core;
using System.Collections.Generic;

namespace PrestaconnectWebService.Model.Prestashop.Entities
{
    public class ShopGroupDetail
    {
        public long? id { get; set; }
        public string name { get; set; }
        public List<Bukimedia.PrestaSharp.Entities.shop> shops { get; set; } = new List<Bukimedia.PrestaSharp.Entities.shop> { };

        public ShopGroupDetail() { }
        public ShopGroupDetail(Bukimedia.PrestaSharp.Entities.ShopGroup shopGroupPrestashop)
        {
            this.id = shopGroupPrestashop.id;
            this.name = shopGroupPrestashop.name;

            ShopFactory shop = new ShopFactory(Global.Auth.BaseUrl, Global.Auth.Account, "");

            Dictionary<string, string> filters = new Dictionary<string, string>();
            filters.Add("id_shop_group", this.id.ToString());
            var ShopsPrestashop = shop.GetByFilter(filters, null, null);

            Bukimedia.PrestaSharp.Entities.shop shopDefault = new Bukimedia.PrestaSharp.Entities.shop();
            shopDefault.id = null;
            shopDefault.name = "Tous";
            shopDefault.id_shop_group = this.id;

            shops.Add(shopDefault);
            shops.AddRange(ShopsPrestashop);
        }

        public List<ShopGroupDetail> GetShopGroupDetails()
        {
            List<ShopGroupDetail> shopGroupDetails = new List<ShopGroupDetail>();

            ShopGroupFactory shopGroupAuth = new ShopGroupFactory(Global.Auth.BaseUrl, Global.Auth.Account, "");
            var shopsGroup = shopGroupAuth.GetAll();

            foreach(var shopGroup in shopsGroup)
            {
                ShopGroupDetail shopGroupDetail = new ShopGroupDetail(shopGroup);
                shopGroupDetails.Add(shopGroupDetail);
            }

            return shopGroupDetails;
        }
    }
}
