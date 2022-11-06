using DSMetodNetX.Mvc.Seguridad;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Threading.Tasks;
using Rediin2022.Entidades.PriCatalogos;
using Rediin2022.Aplicacion.PriCatalogos;
using Rediin2022.Entidades.PriOperacion;
using Rediin2022.Aplicacion.PriOperacion;

namespace Rediin2022Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //Configuracion personalizada
            MStartUpMvc.ConfiguraServicios(services, Configuration, true);
            services.AddScoped<ResourceManager>(delegate (IServiceProvider p)
            {
                return Rediin2022.Entidades.Idioma.MensajesXId.ResourceManager;
            });
            //services.AddScoped<APLCatalogosRep, APLCatalogosRep>();

            //Inyeccion Negocios
            //services.AddScoped<INPaises, APLPaises>();
            //Catalogos
            services.AddScoped<INCatalogos, APLCatalogos>();
            services.AddScoped<INProcesosOperativos, APLProcesosOperativos>();
            services.AddScoped<INAutorizaciones, APLAutorizaciones>();
            //Operacion
            services.AddScoped<INConExpedientes, APLConExpedientes>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //Configuracion personalizada
            MStartUpMvc.Configura(app, env);
        }
    }
}