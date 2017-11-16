using ConsultorioAPI.Database.Repositories;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace ConsultorioAPI.Controllers
{
    public abstract class BaseConsultorioController : ApiController
    {
        protected IHttpActionResult GetErrorResult(ResultadoOperacao r)
        {
            if (r == null || r.ErroInterno)
                return InternalServerError();

            if (r.Sucesso)
                return Ok();

            return BadRequest(r.Mensagem);
        }

        protected IHttpActionResult GetErrorResult(IdentityResult result)
        {
            if (result == null)
            {
                return InternalServerError();
            }

            if (!result.Succeeded)
            {
                if (result.Errors != null)
                    foreach (string error in result.Errors)
                        ModelState.AddModelError("", error);

                if (ModelState.IsValid)
                    return BadRequest();

                return BadRequest(ModelState);
            }

            return null;
        }
    }
}