using Elasticsearch.Net;
using MassTransit;
using Nest;
using ProjectLothal.ElasticSearch.Domain.Models;
using System.Reflection;

namespace ProjectLothal.ElasticSearch.API.Extensions
{
    public static class Elasticsearch
    {
        public static void AddElastic(this IServiceCollection services, IConfiguration configuration) {
            var elasticPool = new SingleNodeConnectionPool(new Uri(configuration.GetSection("Elastic")["Url"]!));
            var settings = new ConnectionSettings(elasticPool);
            var client = new ElasticClient(settings);
           client.Indices
                .Create("blog", a => a.Map<Blog>(m => m
                    .Properties(ps => ps
                       .Text(s => s.Name(a=>a.Title).Fields(a => a.Keyword(s => s.Name(a=>a.Title))))
                       .Keyword(q=>q.Name(a=>a.UserId))
                       .Keyword(q=>q.Name(a=>a.Content))
                       .Keyword(q=>q.Name(a=>a.Created))
                       .Date(q=>q.Name(a=>a.Created))
                       )

                    ));
            services.AddSingleton(client);
        }

      
    }


  
}
