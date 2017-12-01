using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConsultorioAPI.Models.ViewModels
{
    public class EstatisticaPaciente
    {
        public int ConsultasNoMes { get; set; }

        public Paciente Paciente { get; set; }
    }
}