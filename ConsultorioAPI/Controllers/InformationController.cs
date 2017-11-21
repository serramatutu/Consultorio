using ConsultorioAPI.Data;
using ConsultorioAPI.Database;
using ConsultorioAPI.Database.Repositories;
using ConsultorioAPI.Models.ViewModels;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;

namespace ConsultorioAPI.Controllers
{
    [RoutePrefix("info")]
    [Authorize/*(Roles = "paciente, medico, admin")*/]
    public class InformationController : BaseConsultorioController
    {
        private AuthRepository _authRepo = new AuthRepository(new ConsultorioDbContext());
        private EspecialidadeRepository _especialidadeRepo = new EspecialidadeRepository(new ConsultorioDbContext());
        private ConsultaRepository _consultaRepo = new ConsultaRepository(new ConsultorioDbContext());
        private PacienteRepository _pacienteRepo = new PacienteRepository(new ConsultorioDbContext());
        private MedicoRepository _medicoRepo = new MedicoRepository(new ConsultorioDbContext());

        [Route("getmedicos")]
        [HttpPost]
        public async Task<IHttpActionResult> GetMedicos([FromBody]string[] especialidades)
        {
            return Ok(_medicoRepo.GetMedicos(especialidades).Select(x => new DisplayMedico(x)));
        }

        [Route("getespecialidades")]
        [HttpGet]
        public async Task<IHttpActionResult> GetEspecialidades()
        {
            return Ok(_especialidadeRepo.GetAll());
        }

        protected override void Dispose(bool disposing)
        {
            _authRepo.Dispose();
            _especialidadeRepo.Dispose();
            _consultaRepo.Dispose();
            _pacienteRepo.Dispose();
            _medicoRepo.Dispose();

            base.Dispose(disposing);
        }
    }
}