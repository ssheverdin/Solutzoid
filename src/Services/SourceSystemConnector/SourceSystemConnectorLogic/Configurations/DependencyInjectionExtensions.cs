using System;
using System.Collections.Generic;
using System.Text;
using AzureClient.AzureKeyVault;
using DataUnitOfWork;
using MetadataUtility;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SourceSystemConnectorDataAccess;
using SourceSystemConnectorLogic.Services;
using SourceSystemConnectorLogic.Services.Implementations;

namespace SourceSystemConnectorLogic.Configurations
{
    public static class DependencyInjectionExtensions
    {
        public static void AddSourceSystemConnectorModule(this IServiceCollection services, string connectionToMetadataStore)
        {
            services.AddDbContext<SourceSystemConnectorContext>(options => options.UseSqlServer(connectionToMetadataStore));
            services.AddUnitOfWork<SourceSystemConnectorContext>();
            services.AddScoped<ISourceSystemConnectorService, SourceSystemConnectorService>();
            services.AddScoped<IMetadataUtilityService,MetadataUtilityService>();
            services.AddScoped<IAzureKeyVaultClient, AzureKeyVaultClient>();

        }
    }
}
