﻿using EmployeeManagementPortal_v0._8.Models;
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
            if (objUser.ImageFile != null)
            {
                objUser.Image = objUser.ImageFile.FileName;
                objUser.ImageFile.SaveAs(Server.MapPath("~//Images") + "/" + objUser.ImageFile.FileName);
                if (Session["UserId"].ToString() == objUser.Id.ToString())
                {
                    Session["Image"] = objUser.Image;
                }
            }

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
            if (objUser.ImageFile != null)
            {
                objUser.Image = objUser.ImageFile.FileName;
                objUser.ImageFile.SaveAs(Server.MapPath("~//Images") + "/" + objUser.ImageFile.FileName);
            }
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
            var objUser_old = db.GetUserbyID(objUser.Id);

            string OldPassword = objUser_old.Password.ToString();

            if (string.IsNullOrEmpty(objUser.Password))
            {
                objUser.ErrorMessage = "Old Password cannot be Empty!";
                return View("ChangePassword", objUser);


            }

            if (string.IsNullOrEmpty(objUser.NewPassword))
            {
                objUser.ErrorMessage = "New Password cannot be Empty!";
                return View("ChangePassword", objUser);

            }

           
            if (OldPassword != objUser.Password)
            {
                objUser.ErrorMessage = "Old Password do not match!";
                return View("ChangePassword", objUser);

            }

            var user = new User
            {
                Id = objUser.Id,
                Password = objUser.NewPassword

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