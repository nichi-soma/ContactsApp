using ContactsApp.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ContactsApp.Controllers
{
    public class SignUpController : Controller
    {
        public string connectionString = @"data source=NIS-PC0075\SQLEXPRESS;initial catalog=ContactsDB;user id=sa;password=Test@123;MultipleActiveResultSets=True;Integrated Security=False;App=EntityFramework";
        // GET: SignUp
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(SignUpModel model)
        {

            string query = "insert into tblUser values ('" + model.Email + "','" + model.Password + "','"+model.Secret+"')";

            SqlConnection cn = new SqlConnection(connectionString);
            cn.Open();

            SqlCommand cmd = new SqlCommand(query, cn);
            int result=cmd.ExecuteNonQuery();

            if (result>0)
            {
                cn.Close();
                ViewBag.Error = "User SignUp Successfully!!";
                return View();
            }
            else
            {
                cn.Close();
                ViewBag.Error = "Invalid data, try again!";
                return View();
            }
        }
    }
}