using Elasticsearch.Net;
using MassTransit;
using Nest;
using System.Reflection;

namespace ProjectLothal.ElasticSearch.API.Extensions
{
    public static class Elasticsearch
    {
        public static void AddElastic(this IServiceCollection services, IConfiguration configuration) {
            var elasticPool = new SingleNodeConnectionPool(new Uri(configuration.GetSection("Elastic")["Url"]!));
            var settings = new ConnectionSettings(elasticPool);
            var client = new ElasticClient(settings);
            services.AddSingleton(client);
        }

      
    }


  
}
