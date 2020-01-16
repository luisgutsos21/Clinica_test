using System;
using System.Collections.Generic;

namespace WebClinica.Models.DBClinica
{
    public partial class UsersType
    {
        public UsersType()
        {
            Users = new HashSet<Users>();
        }

        public int IduserType { get; set; }
        public string UserTypeDesc { get; set; }

        public ICollection<Users> Users { get; set; }
    }
}
