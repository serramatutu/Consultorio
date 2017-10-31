using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Extensions.DependencyInjection;

namespace ConsultorioAPI.App_Start
{
    public partial class Startup
    {

        public void ConfigureServices(IServiceCollection services)
        {
            ConfigurePolicies(services);
        }

        /// <summary>
        /// Configura as políticas de acesso do site
        /// </summary>
        /// <param name="services"></param>
        public void ConfigurePolicies(IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy("Fundadores", policy =>
                                  policy.RequireClaim("UserName", "Davi", "Lucas"));
            });
        }
    }
}