﻿using ConsultorioAPI.Data;
using ConsultorioAPI.Database;
using ConsultorioAPI.Database.Repositories;
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
    public class AdminController : BaseConsultorioController
    {
        private AuthRepository _authRepo = new AuthRepository(new ConsultorioDbContext());
        private EspecialidadeRepository _especialidadeRepo = new EspecialidadeRepository(new ConsultorioDbContext());

        [Route("cadastrarmedico")]
        [HttpPost]
        public async Task<IHttpActionResult> CadastrarMedico([FromBody]CadastroUserModel userModel, [FromBody]Medico medico)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // Caso o modelo enviado não seja coerente com o exigido
            }

            IdentityResult result = await _authRepo.RegisterUser(userModel, "medico");

            IHttpActionResult errorResult = GetErrorResult(result);

            if (errorResult != null)
                return errorResult;

            return Ok();
        }

        [Route("cadastrarespecialidade")]
        [HttpPost]
        public async Task<IHttpActionResult> CadastrarEspecialidade([FromBody]string nomeEspecialidade)
        {
            return GetErrorResult(await _especialidadeRepo.CreateAsync(nomeEspecialidade));
        }
    }
}