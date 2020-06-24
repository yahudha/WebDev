using EmployeeManagementApp.Models;
using System;
using System.Collections.Generic;
using System.EnterpriseServices.Internal;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EmployeeManagementApp.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            EmployeeManagementApp.Models.User objUser = new User();
            objUser.ErrorMessage = "";
            return View("Index", objUser);
        }

        [HttpPost]
        public ActionResult Authorize(EmployeeManagementApp.Models.User objUser)
        {
            using (EmployeeManagementAppEntities db = new EmployeeManagementAppEntities())
            {
                var users = db.Users.Where(u => u.UserName == objUser.UserName && u.Password == objUser.Password).FirstOrDefault();
                if (users != null)
                {
                    Session["UserID"] = users.Id;
                    Session["Role"] = users.Role;
                    Session["UserName"] = users.UserName;
                    Session["photoChoice"] =  users.Image == "" ? null : users.Image;
                    return RedirectToAction("DashIndex", "Dashboard");
                }
                else
                {
                    objUser.ErrorMessage = "Invalid credentials";
                    return View("Index", objUser);
                }
            }
        }

        public ActionResult Logout()
        {
            Session.Abandon();
            Session.RemoveAll();
            EmployeeManagementApp.Models.User objUser = new User();
            objUser.ErrorMessage = "";
            return View("Index", objUser);
        }


    }
}