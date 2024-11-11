using DSEntityNetX.Entities.Rules;
using DSMetodNetX.Comun;
using DSMetodNetX.Entidades;
using DSMetodNetX.Mvc;
using DSMetodNetX.Mvc.TagHelpers.Filters;
using DSMetodNetX.Web;
using Rediin2022.Aplicacion.PriCatalogos;
using Rediin2022.Aplicacion.PriClientes;
using Rediin2022.Aplicacion.PriOperacion;
using Rediin2022.Entidades.PriCatalogos;
using Rediin2022.Entidades.PriClientes;
using Rediin2022.Entidades.PriOperacion;
using Sisegui2020.Aplicacion.PriCatalogos;
using Sisegui2020.Aplicacion.PriSeguridad;
using Sisegui2020.Aplicacion.PubSeguridad;
using Sisegui2020.Entidades.PriCatalogos;
using Sisegui2020.Entidades.PriSeguridad;
using Sisegui2020.Entidades.PubSeguridad;

namespace RediinProvMedix2024Mvc
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
            //Adi
            Boolean debug = true;

            StartUpGen.ConfigServIdioma();

            MStartUpMvcGen.ConfigServIdioma(services);
            MStartUpMvcGen.ConfigServSesion(services);
            MStartUpMvcGen.ConfigServAutentificacion(services, "/PubSeguridad/Autentificacion/Login", "/PubSeguridad/Autentificacion/SalirSistema");

            MStartUpMvcGen.ConfigServControllersWithViews(services);
            //services.AddControllersWithViews()
            //.AddApplicationPart(typeof(DSEntityNetX.Mvc.TagHelpers.XUtilTH).Assembly) //Para obtener los recursos desde una libreria
            //.AddControllersAsServices();

            //Metodologia 3.1 (cambio del anterior): Es para obtener los recursos incrustados como hojas de estilo y javaScript;
            //Assembly vAssembly = typeof(DSEntityNetX.Mvc.TagHelpers.XUtilTH).GetTypeInfo().Assembly;
            //EmbeddedFileProvider vEmbeddedFileProvider = new EmbeddedFileProvider(vAssembly, nameof(DSEntityNetX.Mvc));
            //services.AddSingleton<IFileProvider>(vEmbeddedFileProvider);

            //************************************************************************
            // Configuracion personalizada
            //************************************************************************
            ////General
            services.AddScoped<IMMensajes, MMensajes>();
            services.AddScoped<IXBusinessRule, XEBusinessRule>();

            MStartUpMvcGen.ConfigServApiSisegui(services, Configuration, debug);
            MStartUpMvcGen.ConfigServApiCliente(services, Configuration, debug);
            MStartUpMvcGen.ConfigServApiClienteNvo(services, Configuration, debug);

            //Inyeccion Negocios
            services.AddScoped<INExpedientes, NRExpedientes>();
            services.AddScoped<INExpedientesProveedor, NRExpedientesProveedor>();
            services.AddScoped<INSeguridad, NRSeguridad>();
            services.AddScoped<INParametrosSistema, NRParametrosSistema>();
            services.AddScoped<INConExpedientes, NRConExpedientes>();
            services.AddScoped<INBancos, NRBancos>();
            services.AddScoped<INIdentificaciones, NRIdentificaciones>();
            services.AddScoped<INPaises, NRPaises>();
            services.AddScoped<INEstablecimientos, NREstablecimientos>();
            services.AddScoped<INRegimenesFiscales, NRRegimenesFiscales>();
            services.AddScoped<INModelos, NRModelos>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            MStartUpMvcGen.ConfiguraExcepciones(app, env);
            MStartUpMvcGen.ConfiguraLocalizacion(app);

            //Metodologia: Sesion
            app.UseSession();

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();

            MStartUpMvcGen.ConfiguraAutentificacion(app);

            //Metodologia: Es para obtener los recursos incrustados como hojas de estilo y javaScript;
            app.UseMiddleware<MMvcResourcesMiddleware>();
            //app.UseMiddleware<MMensajesMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute("default", "{area=PubSeguridad}/{controller=Autentificacion}/{action=Login}");
                endpoints.MapControllerRoute("otro", "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
