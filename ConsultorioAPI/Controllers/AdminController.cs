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
    [RoutePrefix("admin")]
    [EnableCors(Globals.CLIENT_URL,
                    "*",
                    "POST, GET",
                    SupportsCredentials = false)]
    public class AdminController : ApiController
    {
        [Route("cadastrarmedico")]
        public async Task<IHttpActionResult> CadastrarMedico([FromBody]Medico medico)
        {
            throw new NotImplementedException();
        }

        [Route("cadastrarespecialidade")]
        public async Task<IHttpActionResult> CadastrarEspecialidade([FromBody]Especialidade esp)
        {
            throw new NotImplementedException();
        }
    }
}