using ConsultorioAPI.Data;
using ConsultorioAPI.Database;
using ConsultorioAPI.Database.Repositories;
using ConsultorioAPI.Models;
using ConsultorioAPI.Models.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Threading.Tasks;
using System.Web.Http;

namespace ConsultorioAPI.Controllers
{
    [RoutePrefix("conta")]
    public class AccountController : BaseConsultorioController
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
                    Nome = userModel.NomeCompleto,
                    Id = Guid.NewGuid()
                }, userModel.UserName, userModel.Senha);

            IHttpActionResult e2 = GetErrorResult(r2);
            if (e2 != null)
                return e2;

            return Ok();
        }

        // POST conta/cadastrar
        [Authorize]
        [HttpPost]
        [Route("alterar")]
        public async Task<IHttpActionResult> Alterar([FromBody]CadastroUserModel userModel)
        {
            throw new NotImplementedException();
        }

        protected override void Dispose(bool disposing)
        {
            _authRepo.Dispose();
            _pacienteRepo.Dispose();
            base.Dispose(disposing);
        }
    }
}