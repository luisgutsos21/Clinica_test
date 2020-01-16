using System;
using System.Collections.Generic;

namespace WebClinica.Models.DBClinica
{
    public partial class Users
    {
        public Users()
        {
            Citas = new HashSet<Citas>();
            LoginUsers = new HashSet<LoginUsers>();
        }

        public int IdUser { get; set; }
        public string UserName { get; set; }
        public int? UserType { get; set; }
        public string IdCitizen { get; set; }
        public string UserLastname1 { get; set; }
        public string UserLastname2 { get; set; }
        public string UserDescription { get; set; }

        public UsersType UserTypeNavigation { get; set; }
        public ICollection<Citas> Citas { get; set; }
        public ICollection<LoginUsers> LoginUsers { get; set; }
    }
}
