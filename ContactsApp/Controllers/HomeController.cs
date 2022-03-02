using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using ContactsApp.Models;
using System.Data;

namespace ContactsApp.Controllers
{
    public class HomeController : Controller
    {
        public string connectionString = @"data source=NIS-PC0075\SQLEXPRESS;initial catalog=ContactsDB;user id=sa;password=Test@123;MultipleActiveResultSets=True;Integrated Security=False;App=EntityFramework";
        public ActionResult Index(string id)
        {
            ContactsViewModel contacts = new ContactsViewModel();
            contacts.Data= GetAllContacts(id);
            contacts.LoggedInUserID = id;
            return View(contacts);
        }
        public List<ContactModel> GetAllContacts(string  UserID)
        {
            var result = new List<ContactModel>();

            try
            {
                DataTable dt = new DataTable();
                SqlConnection cn = new SqlConnection(connectionString);
                cn.Open();

                string query = "select * from tblContacts where UserID=" + UserID;
                SqlCommand cmd = new SqlCommand(query, cn);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);

                if(dt.Rows.Count>0)
                {
                    foreach(DataRow row in dt.Rows)
                    {
                        var contactObj = new ContactModel()
                        {
                            Email = row["Email"].ToString(),
                            Name = row["Name"].ToString(),
                            PhNo = row["PhNo"].ToString()
                        };
                        result.Add(contactObj);
                    }
                }
            }
            catch (Exception)
            {
                return null;
            }
            return result;
        }

        [HttpPost]
        public ActionResult Index(ContactsViewModel model)
        {

            string query = "insert into tblContacts values ('" +model.LoggedInUserID + "','" + model.Name + "','" + model.PhNo + "','" + model.Email + "')";

            SqlConnection cn = new SqlConnection(connectionString);
            cn.Open();

            SqlCommand cmd = new SqlCommand(query, cn);
            int result = cmd.ExecuteNonQuery();

            if (result > 0)
            {
                cn.Close();
                ViewBag.Error = "Contact Saved Successfully!!";
                ContactsViewModel contacts = new ContactsViewModel();
                contacts.Data = GetAllContacts(model.LoggedInUserID);
                return View(contacts);
            }
            else
            {
                cn.Close();
                ViewBag.Error = "Invalid data, try again!";
                ContactsViewModel contacts = new ContactsViewModel();
                contacts.Data = GetAllContacts(model.LoggedInUserID);
                return View(contacts);
            }
        }
    }
}