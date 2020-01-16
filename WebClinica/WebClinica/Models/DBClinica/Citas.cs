using System;
using System.Collections.Generic;

namespace WebClinica.Models.DBClinica
{
    public partial class Citas
    {
        public int IdCita { get; set; }
        public string DescripcionCita { get; set; }
        public int? IdTipoCita { get; set; }
        public bool Cancelada { get; set; }
        public DateTime? FechaCita { get; set; }
        public int? IdUser { get; set; }

        public TiposCitas IdTipoCitaNavigation { get; set; }
        public Users IdUserNavigation { get; set; }
    }
}
