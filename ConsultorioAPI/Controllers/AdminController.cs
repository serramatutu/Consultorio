using ConsultorioAPI.Data;
using ConsultorioAPI.Database;
using ConsultorioAPI.Database.Repositories;
using ConsultorioAPI.Models;
using ConsultorioAPI.Models.ViewModels;
using Microsoft.AspNet.Identity;
using System.Threading.Tasks;
using System.Web.Http;

namespace ConsultorioAPI.Controllers
{
    [Authorize(Roles = "admin")]
    [RoutePrefix("admin")]
    public class AdminController : BaseConsultorioController
    {
        private AuthRepository _authRepo = new AuthRepository(new ConsultorioDbContext());
        private MedicoRepository _medicoRepo = new MedicoRepository(new ConsultorioDbContext());
        private EspecialidadeRepository _especialidadeRepo = new EspecialidadeRepository(new ConsultorioDbContext());

        [Route("cadastrarmedico")]
        [HttpPost]
        public async Task<IHttpActionResult> CadastrarMedico([FromBody]CadastroMedicoModel dados)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState); // Caso o modelo enviado não seja coerente com o exigido

            IdentityResult result = await _authRepo.RegisterUser(dados.UserName,
                                                                 dados.Email,
                                                                 dados.Senha, 
                                                                 "medico");
            IHttpActionResult errorResult = GetErrorResult(result);

            if (errorResult != null)
                return errorResult;

            ResultadoOperacao resultado = await _medicoRepo.CreateAsync(new Medico()
            {
                Nome = dados.NomeCompleto,
                Celular = dados.Celular,
                CRM = dados.CRM,
                DataNasc = dados.DataNasc,
                Telefone = dados.Telefone
            }, dados.UserName, dados.IdEspecialidade);

            // TODO: Envio de email ao médico com seus dados de login

            return Ok();
        }

        [Route("cadastrarespecialidade")]
        [HttpPost]
        public async Task<IHttpActionResult> CadastrarEspecialidade([FromBody]string nomeEspecialidade)
        {
            return GetErrorResult(await _especialidadeRepo.CreateAsync(nomeEspecialidade));
        }

        protected override void Dispose(bool disposing)
        {
            _authRepo.Dispose();
            _medicoRepo.Dispose();
            _especialidadeRepo.Dispose();
            base.Dispose(disposing);
        }

        // TODO: Dispose
    }
}