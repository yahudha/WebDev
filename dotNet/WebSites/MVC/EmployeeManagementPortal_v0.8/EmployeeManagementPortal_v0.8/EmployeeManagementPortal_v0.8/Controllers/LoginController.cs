using EmployeeManagementPortal_v0._8.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EmployeeManagementPortal_v0._8.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            User objUser = new User();
            objUser.ErrorMessage = "";
            return View(objUser);
        }

        public ActionResult Authorize(User objUser)
        {

            using(EmployeeManagementAppEntities db = new EmployeeManagementAppEntities())
            {

                var Users = db.Users.Where(u => u.UserName == objUser.UserName && u.Password == objUser.Password).FirstOrDefault();
                if(Users != null)
                {
                    Session["UserId"] = Users.Id;
                    Session["UserName"] = Users.UserName;
                    Session["Image"] = Users.Image;
                    Session["Role"] = Users.Role;
                }
                else
                {
                    objUser.ErrorMessage = "Invalid Credentials";
                    return View("Index", objUser);
                }
            }

            return RedirectToAction("DashIndex", "Dashboard");
        }
    }
}