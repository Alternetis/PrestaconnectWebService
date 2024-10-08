using Bukimedia.PrestaSharp.Entities;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Windows;

namespace PrestaconnectWebService.PrestaSharp.Factory
{

    public class GenericFactory<T> : Bukimedia.PrestaSharp.Factories.GenericFactory<T> where T : PrestaShopEntity, IPrestaShopFactoryEntity, new()
    {
        protected  override string singularEntityName { get; }

        protected override string pluralEntityName { get; }

        public GenericFactory(string BaseUrl, string Account, string Password)
            : base(BaseUrl, Account, Password)
        {
        }



        private void AddBody(RestRequest request, IEnumerable<PrestaShopEntity> entities)
        {
            request.RequestFormat = RestSharp.DataFormat.Xml;
            request.XmlSerializer = new Converters.PrestaSharpSerializer();
            string text = string.Empty;
            foreach (PrestaShopEntity entity in entities)
            {
                text += ((Converters.PrestaSharpSerializer)request.XmlSerializer).PrestaSharpSerialize(entity);
            }

            text = "<prestashop>\n" + text + "\n</prestashop>";
            request.AddParameter("application/xml", text, ParameterType.RequestBody);
        }

        private void AddBody(RestRequest request, PrestaShopEntity entity)
        {
            AddBody(request, new List<PrestaShopEntity> { entity });
        }

        #region Pour Un Shop spécifique
        protected RestRequest RequestForGet(string resource, long? id,long id_Shop, string rootElement)
        {
            RestRequest restRequest = new RestRequest();
            long? num = id;
            restRequest.Resource = resource + "/" + num;
            restRequest.AddParameter("id_shop", id_Shop);
            restRequest.RootElement = rootElement;
            return restRequest;
        }

        protected RestRequest RequestForFilterForShop(string resource, string display, Dictionary<string, string> filter, string sort, string limit, string rootElement)
        {
            RestRequest restRequest = new RestRequest
            {
                Resource = resource,
                RootElement = rootElement
            };
            if (display != null)
            {
                restRequest.AddParameter("display", display);
            }

            if (filter != null)
            {
                foreach (string key in filter.Keys)
                {
                    restRequest.AddParameter("filter[" + key + "]", filter[key]);
                }
            }

            if (!string.IsNullOrEmpty(sort))
            {
                restRequest.AddParameter("sort", sort);
            }

            if (!string.IsNullOrEmpty(limit))
            {
                restRequest.AddParameter("limit", limit);
            }

            restRequest.AddParameter("date", "1");
            return restRequest;
        }

        protected RestRequest RequestUpdateForShop(string resource, long? id, Dictionary<string, string> filter, PrestaShopEntity prestashopEntity)
        {
            if (!id.HasValue)
            {
                throw new ApplicationException("Id is required to update something.");
            }

            RestRequest restRequest = new RestRequest
            {
                RootElement = "prestashop",
                Resource = resource,
                Method = Method.PUT
            };
            restRequest.AddParameter("id", id, ParameterType.UrlSegment);

            if (filter != null)
            {
                foreach (string key in filter.Keys)
                {
                    restRequest.AddParameter(key ,filter[key], ParameterType.QueryString);
                }
            }
            AddBody(restRequest, prestashopEntity);
            return restRequest;
        }
        protected RestRequest RequestAddForShop(string resource, long? id, Dictionary<string, string> filter, PrestaShopEntity prestashopEntity)
        {

            RestRequest restRequest = new RestRequest
            {
                Resource = resource,
                Method = Method.POST
                
            };

            if (filter != null)
            {
                foreach (string key in filter.Keys)
                {
                    restRequest.AddParameter( key, filter[key], ParameterType.QueryString);
                }
            }

            AddBody(restRequest, prestashopEntity);
            return restRequest;

        }

        public T GetForShop(long id, long? idShop)
        {
            RestRequest request = RequestForGet(pluralEntityName, id, (long)idShop, singularEntityName);
            return Execute<T>(request);
        }

        public void UpdateForShop(T Entity,long idShop)
        {
            Dictionary<string, string> filter = new Dictionary<string, string>
            {
                { "id_shop", $"{idShop}" }
            };

            RestRequest request = RequestUpdateForShop(pluralEntityName, Entity.id, filter, Entity);
            Execute<T>(request);
        }

        public T AddForShop(T Entity, long idShop)
        {
            Dictionary<string, string> filter = new Dictionary<string, string>
            {
                { "id_shop", $"{idShop}"},
            };

            RestRequest request = RequestAddForShop(pluralEntityName, Entity.id, filter, Entity);
            return Execute<T>(request);
        }

        public List<T> GetByFilterForShop(long id_shop,Dictionary<string, string> Filter, string Sort, string Limit)
        {
            RestRequest request = RequestForFilterForShop(pluralEntityName, "full", Filter, Sort, Limit, pluralEntityName);
            request.AddParameter("id_shop", id_shop);
            return ExecuteForFilter<List<T>>(request);
        }


        public List<T> GetAllForShop(long? idShop)
        {
            return GetByFilterForShop((long)idShop,null, null, null);
        }

        public List<T> GetRootCatalogForShop(long? idShop)
        {
            Dictionary<string, string> filter = new Dictionary<string, string>
            {
                { "is_root_category", "1" }
            };
            return GetByFilterForShop((long)idShop, filter, null, null);
        }

        public List<T> GetEnfantCatalogForShop(long? idShop, long pre_Id)
        {
            Dictionary<string, string> filter = new Dictionary<string, string>
            {
                { "id_parent", $"{pre_Id}" }
            };
            return GetByFilterForShop((long)idShop, filter, null, null);
        }
        #endregion

        public List<T> GetEnfantCatalog(long pre_Id)
        {
            Dictionary<string, string> filter = new Dictionary<string, string>
            {
                { "id_parent", $"{pre_Id}" }
            };
            return GetByFilter(filter, null, null);
        }
    }
}
