using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.SqlClient;
using System.Data;
using EspritLogin.Models;
using System.Net.Mail;

namespace EspritLogin.Controllers
{  
         
    public class LoginController : ApiController
    {
        private string CONNECTION  = @"Data Source=PC-OUSSEMA;Initial Catalog=Esprit;Integrated Security=True";
        private Boolean CONNECTED = false;
        public LoginController()
        {
            
        }
          /* [System.Web.Http.AcceptVerbs("Get")]
           [System.Web.Http.HttpPost]
           [Route("api/login/test")]
           public string test()
           {

               using (SqlConnection database = new SqlConnection(CONNECTION))
               {
                   string getnumber = "Select * from UserRegestration";
                   SqlDataAdapter count = new SqlDataAdapter(getnumber, database);
                   DataTable dtbl = new DataTable();
                   count.Fill(dtbl);
                   int usersCount;
                   usersCount = dtbl.Rows.Count;
                   return usersCount.ToString(). PadLeft(4, '0');
               }

           }
        */
        [System.Web.Http.AcceptVerbs("POST")]
        [System.Web.Http.HttpPost]       
        [Route("api/login/create")]
        public void Create(UserCreds newUser)
        {

            using (SqlConnection database = new SqlConnection(CONNECTION))
            {


                database.Open();
                SqlCommand query = new SqlCommand("USERADD", database);

                string getnumber = "Select * from UserRegestration";
                SqlDataAdapter count = new SqlDataAdapter(getnumber, database);
                DataTable dtbl = new DataTable();
                count.Fill(dtbl);
                int usersCount;
                usersCount = dtbl.Rows.Count +1;


                query.CommandType = CommandType.StoredProcedure;
                query.Parameters.AddWithValue("@NumId", newUser.NumId);
                query.Parameters.AddWithValue("@prenom", newUser.prenom);
                query.Parameters.AddWithValue("@nom", newUser.nom);
                query.Parameters.AddWithValue("@numTel", newUser.numTel);
                query.Parameters.AddWithValue("@email", newUser.email);
                query.Parameters.AddWithValue("@dateNaissance", newUser.dateNaissance);
                query.Parameters.AddWithValue("@notionalité", newUser.notionalité);
                query.Parameters.AddWithValue("@civilité", newUser.civilité);
                query.Parameters.AddWithValue("@moyenne_bac", newUser.moyenne_bac);
                query.Parameters.AddWithValue("@moyenne1ere", newUser.moyenne1ere);
                query.Parameters.AddWithValue("@moyenne2eme", newUser.moyenne2eme);
                query.Parameters.AddWithValue("@moyenneM1", newUser.moyenneM1);
                query.Parameters.AddWithValue("@moyenneM2", newUser.moyenneM2);
                query.Parameters.AddWithValue("@specialiteLicence", newUser.specialiteLicence);
                query.Parameters.AddWithValue("@specialiteMastere", newUser.specialiteMastere);
                query.Parameters.AddWithValue("@sex", newUser.sex);
               
                string userSex = "";
                switch (newUser.sex)
                {
                    case "male":
                        userSex = "m";
                        break;
                    case "female":
                        userSex = "f";
                        break;
                }
                    
                string IDENTIFIANT = "223"+userSex+usersCount.ToString().PadLeft(4,'0');

                query.Parameters.AddWithValue("@password", IDENTIFIANT);


                query.ExecuteNonQuery();
                sendmail(newUser.email,IDENTIFIANT);
               
            }
        }

        public void sendmail(string mail, string idef)
        {
            var FromEmail = new MailAddress("dragnoviv@gmail.com", "Esprit");
            var FromEmailPassword = "rbbxxamtvfqsclct";
            var ToEmail = new MailAddress(mail);
            string subject = "IDENTIFIANT UTILISATEUR";
            string body = "<br/><br/> VOTRE IDENTIFIANT EST LE SUIVANT " +
                "<h1>" + idef+"<h1/> ";
            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(FromEmail.Address, FromEmailPassword)

            };

            using (var message = new MailMessage(FromEmail, ToEmail)
            {

                Subject = subject,
                Body = body,
                IsBodyHtml = true


            })
                smtp.Send(message);
        }

        [System.Web.Http.AcceptVerbs("POST")]
        [System.Web.Http.HttpPost]
        [Route("api/login/tryTologin")]
        public void tryTologin(Credentials credentials)
        {

            using (SqlConnection database = new SqlConnection(CONNECTION))
            {
                database.Open();            

                string getUser = "Select * from UserRegestration where NumId = '" + credentials.userId.Trim()
                    +"' and password = '" + credentials.password.Trim() +"'";
                SqlDataAdapter count = new SqlDataAdapter(getUser, database);
                DataTable dtbl = new DataTable();
                count.Fill(dtbl);
                int usersCount;
                usersCount = dtbl.Rows.Count;

                if (usersCount != 0)
                {
                    CONNECTED = true;
                }
                else
                {
                    CONNECTED = false;
                }
            

            }
        }

        [System.Web.Http.AcceptVerbs("GET")]
        [System.Web.Http.HttpGet]
        [Route("api/login/Results")]
        public Boolean Results()
        {
            return CONNECTED;
        }
    }
}
