﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    public class InstallController : BaseController
    {
        //
        // GET: /Install/

        [BasicAction]
        public ActionResult Index()
        {

            if (db.Users.Count() == 0)
            {
                return RedirectToAction("SetData");
            }

            return View();
        }

        [BasicAction]
        public ActionResult SetData()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SetData(InstallModel model)
        {

            if (db.Users.Count() != 0)
            {
                return RedirectToAction("Index");
            }
            if (ModelState.IsValid)
            {
                db.Users.Add(new User { Type = (int)UserTypes.SuperAdministrator, Username = model.UserName, Password = Common.CalculateMD5Hash(model.Password), RegistrationDate = DateTime.Now, ActivationCode = "", IsActivated = true, EmailAddress = model.Email });

                db.Settings.Add(new Setting { Key = "Installed", Value = "true" });
                db.Settings.Add(new Setting { Key = "Email", Value = model.Email });

                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(model);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            db.Dispose();
        }
    }
}
