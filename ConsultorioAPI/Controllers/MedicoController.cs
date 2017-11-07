using ConsultorioAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;

namespace ConsultorioAPI.Controllers
{
    [RoutePrefix("medico")]
    [EnableCors(Globals.CLIENT_URL,
                    "*",
                    "POST, GET",
                    SupportsCredentials = false)]
    [Authorize(Roles = "medico")]
    public class MedicoController : ApiController
    {
        [Route("comentarconsulta")]
        public async Task<IHttpActionResult> ComentarConsulta([FromBody]Consulta comentario)
        {
            throw new NotImplementedException();
        }
    }
}