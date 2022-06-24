using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace EspritLogin.Controllers
{
    public class HomeController : Controller
    {
        private string CONNECTION = @"Data Source=PC-OUSSEMA;Initial Catalog=Esprit;Integrated Security=True";
        public HomeController()
        {
           
            

              
           

        }

       [System.Web.Http.AcceptVerbs("Get")]
          [System.Web.Http.HttpPost]
          [Route("api/login/count")]
          public string count()
          {

              using (SqlConnection database = new SqlConnection(CONNECTION))
              {
                  string getnumber = "Select * from UserRegestration";
                  SqlDataAdapter count = new SqlDataAdapter(getnumber, database);
                  DataTable dtbl = new DataTable();
                  count.Fill(dtbl);
                  int usersCount;
                  usersCount = dtbl.Rows.Count;
                  return usersCount.ToString();
              }

          }
       
    }
}
