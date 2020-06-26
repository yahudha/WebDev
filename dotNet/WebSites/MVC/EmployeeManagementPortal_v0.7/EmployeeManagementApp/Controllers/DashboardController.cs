using EmployeeManagementApp.Models;
using EmployeeManagementApp.Models.DAL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Linq;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebGrease.Configuration;

namespace EmployeeManagementApp.Controllers
{
    public class DashboardController : Controller
    {

        private IUserRepository userRepository;

        public DashboardController()
        {
            this.userRepository = new UserRepository(new EmployeeManagementAppEntities());
        }

        public ActionResult DashIndex()
        {

            var userList = userRepository.GetUsers();
            return View(userList);
        }

        public ActionResult Create()
        {
            ViewBag.Roles = new SelectList(userRepository.GetRoles(), "RoleName", "RoleName");
            return View();
        }


        [HttpPost]
        public ActionResult Create(User objuser)
        {
            string image_filename = string.Empty;

            if (objuser.ImageFile != null)
            {
                objuser.ImageFile.SaveAs(Server.MapPath("~//Images") + "/" + objuser.ImageFile.FileName);
                objuser.Image = objuser.ImageFile.FileName;
            }
            userRepository.InsertUser(objuser);
            userRepository.Save();
            return RedirectToAction("DashIndex");
        }

        public ActionResult Edit(int Id)
        {
            User objUser = userRepository.GetUserByID(Id);
            ViewBag.Roles = new SelectList(userRepository.GetRoles(), "RoleName", "RoleName");
            return View(objUser);
        }


        [HttpPost]
        public ActionResult Edit(int Id, User objUser)
        {


            string image_filename = string.Empty;

            if (objUser.ImageFile == null)
            {
                image_filename = "";
            }
            else
            {
                image_filename = objUser.ImageFile.FileName;
                objUser.ImageFile.SaveAs(Server.MapPath("~//Images") + "/" + objUser.ImageFile.FileName);
                Session["photoChoice"] = objUser.ImageFile.FileName == "" ? null : objUser.ImageFile.FileName;

            }

            var user = new User()
            {
                Id = objUser.Id,
                UserName = objUser.UserName,
                Role = objUser.Role,
                Image = image_filename
            };
            using (var db = new EmployeeManagementAppEntities())
            {
                db.Users.Attach(user);
                db.Entry(user).Property(x => x.UserName).IsModified = true;
                db.Entry(user).Property(x => x.Role).IsModified = true;
                db.Entry(user).Property(x => x.Image).IsModified = true;
                db.SaveChanges();
            }

            Session["Role"] = objUser.Role;
            Session["UserName"] = objUser.UserName;
            return RedirectToAction("DashIndex");

        }

        public ActionResult Details(int Id)
        {
            var userList = userRepository.GetUserByID(Id);
            return View(userList);
        }


        public ActionResult Delete(int Id)
        {
            return View();
        }

        [HttpPost]
        public ActionResult Delete(int Id, User objUser)
        {
            userRepository.DeleteUser(Id);
            userRepository.Save();
            return RedirectToAction("DashIndex");
        }

        //EmployeeManagementAppEntities db = new EmployeeManagementAppEntities();
        // GET: Dashboard
        //public ActionResult DashIndex()
        //{


        //    return View(db.Users.ToList());
        //}




        public ActionResult ChangePassword()
        {
            int id = Convert.ToInt32(Session["UserID"]);

            User objUser = userRepository.GetUserByID(id);
            return View(objUser);
            //return View();


        }

        [HttpPost]
        public ActionResult ChangePassword(int Id, User objUser)
        {



            var user = new User() { Id = objUser.Id, Password = objUser.Password };
            using (var db = new EmployeeManagementAppEntities())
            {
                db.Users.Attach(user);
                db.Entry(user).Property(x => x.Password).IsModified = true;
                db.SaveChanges();
            }

            return RedirectToAction("DashIndex");

        }
    }
}