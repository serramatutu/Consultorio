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
    public class AccountController : BaseConsultorioController<LoginUsuario>
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

        protected override void Dispose(bool disposing)
        {
            _authRepo.Dispose();
            _pacienteRepo.Dispose();
            base.Dispose(disposing);
        }

        protected override LoginUsuario GetUsuarioAtual()
        {
            throw new NotImplementedException();
        }
    }
}