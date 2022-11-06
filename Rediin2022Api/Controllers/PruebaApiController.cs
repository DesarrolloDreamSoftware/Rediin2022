using DSMetodNetX.Api.Seguridad;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rediin2022Api.Controllers
{
    [Route("ApiV1/[controller]/[action]")]
    public class PruebaApiController : MPruebaApiController
    {
        public PruebaApiController(IConfiguration configuration, IServiceProvider serviceProvider)
            : base(configuration, serviceProvider)
        {
        }
    }
}
