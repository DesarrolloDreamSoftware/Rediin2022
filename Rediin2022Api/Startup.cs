using DSMetodNetX.Api.Seguridad;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Rediin2022.Entidades.PriCatalogos;
using Rediin2022.Negocio.PriCatalogos;
using Rediin2022.Entidades.PriOperacion;
using Rediin2022.Negocio.PriOperacion;
using Rediin2022.Entidades.PriClientes;
using Rediin2022.Negocio.PriClientes;
using Sisegui2020.Entidades.PriSeguridad;

namespace Rediin2022Api
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
            MStartUpApi.ConfiguraServicios(services, Configuration);

            services.AddSwaggerGen();

            //Inyeccion Negocios
            //services.AddScoped<INPaises, NPaises>();
            //Catalogos
            services.AddScoped<INCatalogos, NCatalogos>();
            services.AddScoped<INProcesosOperativos, NProcesosOperativos>();
            services.AddScoped<INAutorizaciones, NAutorizaciones>();
            services.AddScoped<INBancos, NBancos>();
            services.AddScoped<INIdentificaciones, NIdentificaciones>();

            services.AddScoped<INSapCondicionesPago, NSapCondicionesPago>();
            services.AddScoped<INSapCuentasAsociadas, NSapCuentasAsociadas>();
            services.AddScoped<INSapGrupoCuentas, NSapGrupoCuentas>();
            services.AddScoped<INSapGruposTesoreria, NSapGruposTesoreria>();
            services.AddScoped<INSapGruposTolerancia, NSapGruposTolerancia>();
            services.AddScoped<INSapOrganizacionesCompra, NSapOrganizacionesCompra>();
            services.AddScoped<INSapSociedades, NSapSociedades>();
            services.AddScoped<INSapSociedadesGL, NSapSociedadesGL>();
            services.AddScoped<INSapTratamientos, NSapTratamientos>();
            services.AddScoped<INSapViasPago, NSapViasPago>();
            //Operacion
            services.AddScoped<INConExpedientes, NConExpedientes>();
            //Clientes
            services.AddScoped<INExpedientes, NExpedientes>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            //Configuracion personalizada
            MStartUpApi.Configura(app, env);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute("default", "{area=ModuloPrueba}/{controller=PruebaCtrl}/{action=Valida}/{id?}");
            });
        }
    }
}
