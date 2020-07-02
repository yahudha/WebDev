using EmployeeManagementPortal_v0._8.Models;
using EmployeeManagementPortal_v0._8.Models.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EmployeeManagementPortal_v0._8.Controllers
{
    public class DashboardController : Controller
    {
        private EmployeeManagementPortal_v0._8.Models.DAL.IModelRepository db;

        public DashboardController()
        {
            this.db = new ModelRepository(new Models.EmployeeManagementAppEntities());
        }





        // GET: Dashboard
        public ActionResult DashIndex()
        {
            var objUser = db.GetUsers();
            return View(objUser);
        }


        public ActionResult Edit(int Id)
        {
            var objUser = db.GetUserbyID(Id);
            ViewBag.Roles = new SelectList(db.GetRoles(), "RoleName", "RoleName");
            return View(objUser);
        }

        [HttpPost]
        public ActionResult Edit(User objUser)
        {

            var user = new User
            {
                Id = objUser.Id,
                Role = objUser.Role,
                UserName = objUser.UserName,
                Image = objUser.Image

            };

            using (var db = new EmployeeManagementAppEntities())
            {
                db.Users.Attach(user);
                db.Entry(user).Property(x => x.UserName).IsModified = true;
                db.Entry(user).Property(x => x.Role).IsModified = true;
                db.Entry(user).Property(x => x.Image).IsModified = true;
                db.SaveChanges();
            }

            return RedirectToAction("DashIndex");
        }

        public ActionResult Details(int Id)
        {
            var objUser = db.GetUserbyID(Id);
            return View(objUser);
        }


        public ActionResult Delete(int Id)
        {
            var objUser = db.GetUserbyID(Id);
            return View(objUser);
        }

        [HttpPost]
        public ActionResult Delete(User objUser)
        {
            db.DeleteUser(objUser.Id);
            db.Save();
            return RedirectToAction("DashIndex");

        }

        public ActionResult Create()
        {
            ViewBag.Roles = new SelectList(db.GetRoles(), "RoleName", "RoleName");
            return View();
        }

        [HttpPost]
        public ActionResult Create(User objUser)
        {
            db.InsertUser(objUser);
            db.Save();
            return RedirectToAction("DashIndex");
        }

        public ActionResult ChangePassword()
        {
            int Id = Convert.ToInt32(Session["UserId"]);
            var objUser = db.GetUserbyID(Id);
            return View(objUser);
        }

        [HttpPost]
        public ActionResult ChangePassword(User objUser)
        {

            var user = new User
            {
                Id = objUser.Id,
                Password = objUser.Password

            };

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