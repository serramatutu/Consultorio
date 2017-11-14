using ConsultorioAPI.Data;
using ConsultorioAPI.Database;
using ConsultorioAPI.Database.Repositories;
using ConsultorioAPI.Models;
using ConsultorioAPI.Models.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;

namespace ConsultorioAPI.Controllers
{
    [RoutePrefix("conta")]
    [EnableCors(Globals.CLIENT_URL,
                    "*",
                    "POST",
                    SupportsCredentials = false)]
    public class AccountController : ApiController
    {
        AuthRepository _authRepo = new AuthRepository(new ConsultorioDbContext());
        PacienteRepository _pacienteRepo = new PacienteRepository(new ConsultorioDbContext());

        // POST conta/cadastrar
        [AllowAnonymous]
        [HttpPost]
        [Route("cadastro")]
        public async Task<IHttpActionResult> Cadastrar([FromBody]CadastroUserModel userModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState); // Caso o modelo enviado não seja coerente com o exigido

            IdentityResult r1 = await _authRepo.RegisterUser(userModel, "paciente");
            IHttpActionResult e1 = GetErrorResult(r1);
            if (e1 != null)
                return e1;

            ResultadoOperacao r2 = await _pacienteRepo.CreateAsync(
                new Paciente() {
                    DataNasc = userModel.DataNasc,
                    Endereco = userModel.Endereco,
                    Telefone = userModel.Telefone,
                    Id = Guid.NewGuid()
                }, userModel.UserName, userModel.Senha);

            IHttpActionResult e2 = GetErrorResult(r2);
            if (e2 != null)
                return e2;

            return Ok();
        }

        protected IHttpActionResult GetErrorResult(ResultadoOperacao r)
        {
            if (r == null || r.ErroInterno)
                return InternalServerError();

            if (r.Sucesso)
                return Ok();

            return BadRequest(r.Mensagem);
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

        protected override void Dispose(bool disposing)
        {
            _authRepo.Dispose();
            _pacienteRepo.Dispose();
            base.Dispose(disposing);
        }
    }
}