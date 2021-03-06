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
    public class CategoryController : BaseController
    {
        
        //
        // GET: /Category/
        [LoginAuthorize]
        public ViewResult Index()
        {
            return View(db.Categories.ToList());
        }

        //
        // GET: /Category/Details/5

        [LoginAuthorize]
        public ViewResult Details(int id)
        {
            Category category = db.Categories.Single(c => c.Id == id);
            return View(category);
        }

        //
        // GET: /Category/Create

        [LoginAuthorize]
        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Category/Create

        [LoginAuthorize]
        [HttpPost]
        public ActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {
                db.Categories.Add(category);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(category);
        }
        
        //
        // GET: /Category/Edit/5

        [LoginAuthorize]
        public ActionResult Edit(int id)
        {
            Category category = db.Categories.Single(c => c.Id == id);
            return View(category);
        }

        //
        // POST: /Category/Edit/5

        [LoginAuthorize]
        [HttpPost]
        public ActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                db.Categories.Attach(category);
                db.Entry(category).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(category);
        }

        //
        // GET: /Category/Delete/5

        [LoginAuthorize]
        public ActionResult Delete(int id)
        {
            Category category = db.Categories.Single(c => c.Id == id);
            return View(category);
        }

        //
        // POST: /Category/Delete/5

        [LoginAuthorize]
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            Category category = db.Categories.Single(c => c.Id == id);
            db.Categories.Remove(category);
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