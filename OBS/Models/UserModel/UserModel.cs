using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OBS.Models.UserModel
{
    public class UserModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string MatchPassword { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string TCKN { get; set; }
        public string CepNo { get; set; }
        public string position { get; set; }
    }
}