using LoginRegistrationAppWebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;

namespace LoginRegistrationAppWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public RegistrationController(IConfiguration configuration)
        {
            _configuration = configuration;

        }

        [HttpPost]
        [Route("registration")]
        public string registration(Registration registration)
        {

            int i = 0;
            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("ToysCon").ToString());
            SqlCommand cmd = new SqlCommand("INSERT INTO Registration(UserName,Password,Email,ISActive) VALUES('" + registration.UserName + "', '" + registration.Password + "','" + registration.Email + "','" + registration.ISActive + "')");
            con.Open();
            i = cmd.ExecuteNonQuery();
            con.Close();
            if (i > 0)
            {

                return "Data inserted";
            }
            else
            {


                return "Error";
            }



        }

        [HttpPost]
        [Route("login")]
        public string login(Registration registration)
        {


            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("ToyCon").ToString());
            SqlDataAdapter da = new SqlDataAdapter("SELECT* FROM Registration WHERE Email='" + registration.Email + "' AND Password ='" + registration.Password + "' AND ISActive=1", con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                return "Valid User";
            }
            else
            {
                return "Invalid User";
              
            }
        }
    }


}




