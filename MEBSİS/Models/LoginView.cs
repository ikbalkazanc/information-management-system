using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MEBSİS.Models
{
    public class LoginView
    {

        public int id { get; set; }
        public string password { get; set; }
        public bool graduate { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
    }
}