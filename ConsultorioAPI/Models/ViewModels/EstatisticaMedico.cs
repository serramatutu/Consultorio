using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConsultorioAPI.Models.ViewModels
{
    public class EstatisticaMedico
    {
        public Medico Medico { get; set; }

        public int ConsultasNoMes { get; set; }

        public int? AvaliacaoMedia { get; set; }
    }
}