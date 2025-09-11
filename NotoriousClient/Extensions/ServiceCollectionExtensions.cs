using Microsoft.Extensions.DependencyInjection;
using NotoriousClient.Clients;
using NotoriousClient.Sender;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotoriousClient.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddClient<TClient, TClientImpl>(this IServiceCollection services) where TClientImpl : BaseClient, TClient where TClient: class
        {
            return services.AddScoped<TClient, TClientImpl>();
        }

        public static IServiceCollection AddDefaultSender(this IServiceCollection services)
        {
            return services.AddScoped<IRequestSender, RequestSender>();
        }
    }
}
