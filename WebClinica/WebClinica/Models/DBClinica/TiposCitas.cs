using System;
using System.Collections.Generic;

namespace WebClinica.Models.DBClinica
{
    public partial class TiposCitas
    {
        public TiposCitas()
        {
            Citas = new HashSet<Citas>();
        }

        public int IdTipoCita { get; set; }
        public string DescripcionTipo { get; set; }

        public ICollection<Citas> Citas { get; set; }
    }
}
