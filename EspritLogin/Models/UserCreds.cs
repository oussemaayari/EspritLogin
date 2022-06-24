using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EspritLogin.Models
{
    public class UserCreds
    {
       
       public int NumId { get; set; }
        public string prenom { get; set; }
        public string nom { get; set; }
        public string numTel { get; set; }
        public string email { get; set; }
        public string dateNaissance { get; set; }
        public string notionalité { get; set; }
        public string civilité { get; set; }
        public int moyenne_bac { get; set; }
        public int moyenne1ere { get; set; }
        public int moyenne2eme { get; set; }
        public int moyenneM1 { get; set; }
        public int moyenneM2 { get; set; }
        public string specialiteLicence { get; set; }
        public string specialiteMastere { get; set; }
        public string sex { get; set; }
        public string password { get; set; }


    }
}