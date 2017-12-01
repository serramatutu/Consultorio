using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConsultorioAPI.Models.ViewModels
{
    public class EstatisticaMesConsulta
    {
        public int Mes { get; set; }

        public int ConsultasCanceladasNoMes { get; set; }

        public int ConsultasNoMes { get; set; }
    }
}