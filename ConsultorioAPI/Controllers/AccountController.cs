using ConsultorioAPI.Data;
using ConsultorioAPI.Database;
using ConsultorioAPI.Database.Repositories;
using ConsultorioAPI.Models;
using ConsultorioAPI.Models.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace ConsultorioAPI.Controllers
{
    [RoutePrefix("conta")]
    public class AccountController : BaseConsultorioController
    {
        AuthRepository _authRepo = new AuthRepository(new ConsultorioDbContext());
        PacienteRepository _pacienteRepo = new PacienteRepository(new ConsultorioDbContext());
        MedicoRepository _medicoRepo = new MedicoRepository(new ConsultorioDbContext());

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

        [Route("getuserdata")]
        [HttpGet]
        [Authorize]
        public async Task<IHttpActionResult> GetUserData()
        {
            Dictionary<string, object> content = new Dictionary<string, object>();
            LoginUsuario user = await _authRepo.FindUser(User.Identity.Name);
            content.Add("LoginData", new DisplayUsuario(user));
            foreach (var papel in user.Papeis)
            {
                switch (papel.Nome)
                {
                    case "paciente":
                        content.Add("Paciente", new DisplayPaciente(_pacienteRepo.GetPacienteFromUsername(User.Identity.Name)));
                        break;
                    //case "admin":
                    //    content.Add("Admin", new DisplayAdmin(_pacienteRepo.GetPacienteFromUsername(User.Identity.Name)));
                    //    break;
                    case "medico":
                        content.Add("Medico", new DisplayMedico(_medicoRepo.GetMedico(User.Identity.Name)));
                        break;
                }
            }

            return Json(content);
        }

        #region Alterar

        // TODO: Modificação de dados e rejeição de tokens

        //[Authorize]
        //[HttpPost]
        //[Route("alterar/username")]
        //public async Task<IHttpActionResult> AlterarUserName([FromBody]string userName)
        //{
        //    if (!ModelState.IsValid)
        //        return BadRequest(ModelState); // Caso o modelo enviado não seja coerente com o exigido

        //    _authRepo.AlterarUserName(User.Identity.Name, userName);

        //    return Ok();
        //}

        #endregion

        protected override void Dispose(bool disposing)
        {
            _authRepo.Dispose();
            _pacienteRepo.Dispose();
            _medicoRepo.Dispose();
            base.Dispose(disposing);
        }
    }
}