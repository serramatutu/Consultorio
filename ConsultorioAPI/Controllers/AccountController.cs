using ConsultorioAPI.Data;
using ConsultorioAPI.Database;
using ConsultorioAPI.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Security;

namespace ConsultorioAPI.Controllers
{
    [RoutePrefix("conta")]
    [EnableCors(Globals.CLIENT_URL,
                    "*",
                    "POST",
                    SupportsCredentials = false)]
    public class AccountController : ApiController
    {

        // POST conta/cadastrar
        [AllowAnonymous]
        [HttpPost]
        [Route("cadastro")]
        public async Task<IHttpActionResult> Cadastrar([FromBody]CadastroUserModel userModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // Caso o modelo enviado não seja coerente com o exigido
            }

            IdentityResult result = null;
            using (var repo = new AuthRepository())
            {
                result = await repo.RegisterUser(userModel, "paciente");
            }
            
            IHttpActionResult errorResult = GetErrorResult(result);

            if (errorResult != null)
                return errorResult;

            return Ok();
        }

        private IHttpActionResult GetErrorResult(IdentityResult result)
        {
            if (result == null)
            {
                return InternalServerError();
            }

            if (!result.Succeeded)
            {
                if (result.Errors != null)
                {
                    foreach (string error in result.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }

                if (ModelState.IsValid)
                {
                    // No ModelState errors are available to send, so just return an empty BadRequest.
                    return BadRequest();
                }

                return BadRequest(ModelState);
            }

            return null;
        }
    }
}