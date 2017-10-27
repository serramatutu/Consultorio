using System.Web.Http;

namespace ConsultorioAPI.Controllers
{
    [RoutePrefix("trolei")]
    public class TroleiController : ApiController
    {
        // GET api/trolei/ola
        [Route("ola")]
        public IHttpActionResult Get()
        {
            return Ok(new string[] { "Ola", "Oi" });
        }
    }
}