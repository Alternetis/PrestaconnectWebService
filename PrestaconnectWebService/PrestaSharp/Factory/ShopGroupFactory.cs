using Bukimedia.PrestaSharp.Entities;
using Bukimedia.PrestaSharp.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Bukimedia.PrestaSharp.Factories
{

    public class ShopGroupFactory : GenericFactory<ShopGroup>
    {
        protected override string singularEntityName => "shop_group";

        protected override string pluralEntityName => "shop_groups";

        public ShopGroupFactory(string BaseUrl, string Account, string SecretKey)
            : base(BaseUrl, Account, SecretKey)
        {
        }
    }
}
