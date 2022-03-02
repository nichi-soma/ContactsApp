using ContactsApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;

namespace ContactsApp.Controllers
{
    public class LoginController : Controller
    {
        public string connectionString = @"data source=NIS-PC0075\SQLEXPRESS;initial catalog=ContactsDB;user id=sa;password=Test@123;MultipleActiveResultSets=True;Integrated Security=False;App=EntityFramework";
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(LoginModel model)
        {

            string query = "select * from tblUser where Email='" + model.Email + "' and Password='" + model.Password + "'";

            SqlConnection cn = new SqlConnection(connectionString);
            cn.Open();

            SqlCommand cmd = new SqlCommand(query, cn);
            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                var id = dr["ID"].ToString();
                cn.Close();
                
                return RedirectToAction("Index", "Home", new { @id = id  });
            }
            else
            {
                cn.Close();
                ViewBag.Error = "Invalid user credentials!";
                return View();
            }
        }
    }
}