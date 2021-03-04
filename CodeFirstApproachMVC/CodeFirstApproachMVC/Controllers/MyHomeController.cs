using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CodeFirstApproachMVC.Models;

namespace CodeFirstApproachMVC.Controllers
{
    [HandleError()]
    public class MyHomeController : Controller
    {
        // GET: MyHome
        StudentContext db = new StudentContext();
        public ActionResult Index()
        {
          var data =  db.Students.ToList();
            return View(data);
        }
        public ActionResult Create()
        {

            return View();
        }
        [HttpPost]
        public ActionResult Create(Student s)
        {
            if (ModelState.IsValid == true)
            {
                db.Students.Add(s);
                int a = db.SaveChanges();
                if (a > 0)
                {
                    //ViewBag.alertMesssage = "<script>alert('Data Inserted Successfully !!')</script>";
                    TempData["alertMessage"] = "<script>alert('Data Inserted Successfully !!')</script>";
                    // ModelState.Clear();
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.alertMesssage = "<script>alert('Data not Inserted Successfully !!')</script>";
                }
               
            }
            return View();
        }
        public ActionResult Edit(int Id)
        {
           var row =   db.Students.Where(model => model.Id == Id).FirstOrDefault();
            return View(row);
        }
        [HttpPost]
        public ActionResult Edit(Student s)
        {
            if (ModelState.IsValid == true)
            {
                db.Entry(s).State = EntityState.Modified;
                int a = db.SaveChanges();
                if (a > 0)
                {
                   // ViewBag.UpdateMessage = "<script>alert('Data Updated !!')</script>";
                   TempData["updateMessage"]= "<script>alert('Data Updated !!')</script>";
                    ModelState.Clear();
                    return RedirectToAction("Index");

                }
                else
                {
                    ViewBag.UpdateMessage = "<script>alert('Data not Updated !!')</script>";
                }
            }
            return View();
        }
        public ActionResult Delete(int Id)
        {
            var row = db.Students.Where(model => model.Id == Id).FirstOrDefault();
            return View();
        }
        [HttpPost]
        public ActionResult Delete(Student s)
        {
            
                db.Entry(s).State = EntityState.Deleted;
                int a = db.SaveChanges();
                if (a > 0)
                {
                    // ViewBag.DeleteMessage = "<script>alert('Data Deleted !!')</script>";
                    TempData["deleteMessage"] = "<script>alert('Data Deleted!!')</script>";
                   // ModelState.Clear();
                }
                else
                {
                    TempData["deleteMessage"] = "<script>alert('Data not Deleted !!')</script>";
                }
                return RedirectToAction("Index");
            
           
        }
    }
}