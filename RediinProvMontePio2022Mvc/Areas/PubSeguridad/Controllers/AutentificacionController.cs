using DSEntityNetX.Mvc.Session;
using DSMetodNetX.Comun.Seguridad;
using DSMetodNetX.Entidades;
using DSMetodNetX.Entidades.Config;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Rediin2022.Entidades.PriClientes;
using Rediin2022.Entidades.PriOperacion;
using RediinProvMedix2022Mvc.Areas.PriProveedores.Controllers;
using RediinProvMedix2022Mvc.Models;
using Sisegui2020.Aplicacion.PriUtilerias;
using Sisegui2020.Entidades.PubSeguridad;
using System.Security.Claims;

namespace RediinProvMedix2022Mvc.Areas.PubSeguridad.Controllers
{
    [Area("PubSeguridad")]
    public class AutentificacionController : Controller
    {
        public AutentificacionController(INSeguridad nSeguridad)
        {
            NSeguridad = nSeguridad;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if(EV == null)
                EV = new EVSeguridad(HttpContext);
            base.OnActionExecuting(context);
        }

        public INSeguridad NSeguridad { get; set; }

        private EVSeguridad EV { get; set; }

		public async  Task<IActionResult> Login()
        {
            ViewBag.Reglas = await NSeguridad.AutentificaReglas();

            return View(nameof(Login));
        }

        public async Task<IActionResult> Autentifica(EAutentificar autentificar)
        {
            EAutentificado vAutentificado = await NSeguridad.AutentificaUsuario(new EAutentificar()
            {
                Usuario = MUtilSeguridad.CodificaClave(autentificar.Usuario),
                Contrasenia = MUtilSeguridad.CodificaClave(autentificar.Contrasenia),
                UrlAutentificacionAuto = "Ok"
            });

            //Si es correcta la autenficiacion redirigimos al sistema
            if (NSeguridad.Mensajes.Ok)
            {
                List<Claim> vClaims = new List<Claim>();
                //Agregamos a los claims los datos de MEUsuarioSesion sesion
                MUtilSeguridad.RegistraUsuarioSesion(vClaims, vAutentificado.UsuarioSesion);

                EV.UsuarioId = vAutentificado.UsuarioSesion.UsuarioId;
                EV.EstablecimientoId = vAutentificado.UsuarioSesion.EstablecimientoId;
                EV.EstablecimientoNombre = vAutentificado.UsuarioSesion.EstablecimientoNombre;

				//Agregamos a los claims los token de las apis o stios autentificados
				vClaims.Add(new Claim(MConfigIds.tokenApis, vAutentificado.TokenApis ?? String.Empty));

                //Autentificacion por Cookies
                ClaimsIdentity vClaimsIdentity =
                    new ClaimsIdentity(vClaims, CookieAuthenticationDefaults.AuthenticationScheme);

                AuthenticationProperties vAuthenticationProperties = new AuthenticationProperties()
                {
                    AllowRefresh = true,
                    ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(60),
                };

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(vClaimsIdentity), vAuthenticationProperties);

                return Redirect( $"/PriProveedores/Proveedores/{nameof(ProveedoresController.CapturaProveedorInicia)}");
                //EVDatosPortal.UsuarioSesion = vAutentificado.UsuarioSesion;
                //EVDatosPortal.MenuIdPri = base.MMenuIdPri();
                //EVDatosCtrlPortal.AplicacionesNav = NSeguridad.AplicacionesNavegablesCmb(1); //1 Aplicaciones Web
                //EVDatosPortal.TemaPersonalizado = MUtilPres.CreaClasesEstilo(NTemasPortal.TemaPortalClaseXUsuario(EVDatosPortal.MenuIdPri));
                //EVDatosPortal.TemaIconos = NTemasPortal.TemaPortalIconoXUsuario(EVDatosPortal.MenuIdPri);
                //if (EVDatosPortal.TemaIconos.Count > 0)
                //{
                //    EVDatosPortal.TemaId = XString.XToInt64(EVDatosPortal.TemaIconos[0]);
                //    EVDatosPortal.TemaIconos.RemoveAt(0);
                //}

                ////Url inicial
                //EFuncionInicial vFI =
                //    NPerfiles.FuncionInicialXId(EVDatosPortal.UsuarioSesion.PerfilId,
                //                                EVDatosPortal.MenuIdPri);
                //NPerfiles.Mensajes.Initialize();
                //if (vFI != null && !String.IsNullOrWhiteSpace(vFI.Url))
                //{
                //    //Para que funcione se deben cargar las opciones del menu
                //    EVDatosPortal.OpcionesXMenuXPefil = NSistemas.OpcionXMenu(EVDatosPortal.MenuIdPri);
                //    return Redirect(vFI.Url);
                //}

                //return Redirect(_urlOk);
            }
            else
                ViewBag.MensajeError = "Usuaro o Contraseña incorrecta";
            
            return await Login();
        }

        /// <summary>
        /// Livera los recursos al salir del sistema.
        /// </summary>
        /// <returns></returns>
        public virtual async Task<IActionResult> SalirSistema()
        {
            //LogOut
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            HttpContext.Session.Clear();
            return RedirectToAction(nameof(Login));
            //return Autentifica();
        }
    }
}
