﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication;

namespace WebApplication.Controllers
{ 
    public class PageController : Controller
    {
        private ModelContainer db = new ModelContainer();

        //
        // GET: /Page/

        [LoginAuthorize]
        public ViewResult Index()
        {
            var pages = db.Pages.Include("Creator");
            return View(pages.ToList());
        }

        //
        // GET: /Page/Publish/5

        [LoginAuthorize]
        public ActionResult Publish(int id)
        {
            Page page = db.Pages.Single(e => e.Id == id);
            page.Published = true;

            db.ObjectStateManager.ChangeObjectState(page, EntityState.Modified);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        //
        // GET: /Page/UnPublish/5

        [LoginAuthorize]
        public ActionResult UnPublish(int id)
        {
            Page page = db.Pages.Single(e => e.Id == id);
            page.Published = false;

            db.ObjectStateManager.ChangeObjectState(page, EntityState.Modified);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        //
        // GET: /Page/Details/5

        [LoginAuthorize]
        public ViewResult Details(int id)
        {
            Page page = db.Pages.Single(p => p.Id == id);
            return View(page);
        }

        //
        // GET: /Page/Create
    
        [LoginAuthorize]
        public ActionResult Create()
        {
            ViewBag.UserId = new SelectList(db.Users, "Id", "Username");
            return View();
        } 

        //
        // POST: /Page/Create

        [LoginAuthorize]
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(Page page)
        {
            if (ModelState.IsValid)
            {
                db.Pages.AddObject(page);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.UserId = new SelectList(db.Users, "Id", "Username", page.UserId);
            return View(page);
        }
        
        //
        // GET: /Page/Edit/5
        
        [LoginAuthorize]
        public ActionResult Edit(int id)
        {
            Page page = db.Pages.Single(p => p.Id == id);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Username", page.UserId);
            return View(page);
        }

        //
        // POST: /Page/Edit/5

        [LoginAuthorize]
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(Page page)
        {
            if (ModelState.IsValid)
            {
                db.Pages.Attach(page);
                db.ObjectStateManager.ChangeObjectState(page, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserId = new SelectList(db.Users, "Id", "Username", page.UserId);
            return View(page);
        }

        //
        // GET: /Page/Delete/5
 
        [LoginAuthorize]
        public ActionResult Delete(int id)
        {
            Page page = db.Pages.Single(p => p.Id == id);
            return View(page);
        }

        //
        // POST: /Page/Delete/5

        [LoginAuthorize]
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            Page page = db.Pages.Single(p => p.Id == id);
            db.Pages.DeleteObject(page);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}