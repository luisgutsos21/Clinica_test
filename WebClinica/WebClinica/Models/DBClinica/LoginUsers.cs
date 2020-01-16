using System;
using System.Collections.Generic;

namespace WebClinica.Models.DBClinica
{
    public partial class LoginUsers
    {
        public int IdLoginUser { get; set; }
        public string LoginUser { get; set; }
        public string UserPassword { get; set; }
        public int? IdUser { get; set; }

        public Users IdUserNavigation { get; set; }
    }
}
