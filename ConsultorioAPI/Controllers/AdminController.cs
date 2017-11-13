using ConsultorioAPI.Data;
using ConsultorioAPI.Database;
using ConsultorioAPI.Models;
using ConsultorioAPI.Models.ViewModels;
using Microsoft.AspNet.Identity;
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
    [Authorize(Roles = "admin")]
    [RoutePrefix("admin")]
    [EnableCors(Globals.CLIENT_URL,
                    "*",
                    "POST, GET",
                    SupportsCredentials = false)]
    public class AdminController : ApiController
    {
        private AuthRepository _repo = null;

        public AdminController()
        {
            _repo = new AuthRepository(new ConsultorioDbContext());
        }

        [Route("cadastrarmedico")]
        public async Task<IHttpActionResult> CadastrarMedico([FromBody]CadastroUserModel userModel, [FromBody]Medico medico)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // Caso o modelo enviado não seja coerente com o exigido
            }

            IdentityResult result = await _repo.RegisterUser(userModel, "medico");

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

        [Route("cadastrarespecialidade")]
        public async Task<IHttpActionResult> CadastrarEspecialidade([FromBody]Especialidade esp)
        {
            throw new NotImplementedException();
        }
    }
}