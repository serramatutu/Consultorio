using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConsultorioAPI.Models.ViewModels
{
    public class EstatisticaEspecialidade
    {
        public Especialidade Especialidade { get; set; }

        public int ConsultasNoMes { get; set; }
    }
}