using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ConsultorioAPI.Controllers
{
    public class TroleiController : ApiController
    {
        // GET api/<controller>
        //[Authorize]
        [Route("/")]
        public IEnumerable<string> Get()
        {
            return new string[] { "oi", "ola" };
        }
    }
}