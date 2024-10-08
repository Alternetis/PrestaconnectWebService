using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrestaconnectWebService.PrestaSharp.Factory
{
    public class CategoryFactory : GenericFactory<Bukimedia.PrestaSharp.Entities.category>
    {
        protected override string singularEntityName => "category";

        protected override string pluralEntityName => "categories";

        public CategoryFactory(string BaseUrl, string Account, string SecretKey)
            : base(BaseUrl, Account, SecretKey)
        {
        }

          
    }
}
