using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using JPSMVC_Ramya.Models;

namespace JPSMVC_Ramya.Controllers
{
    [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
    public class UserInformationsController : Controller
    {
        private JobPortalSystemContext1 db = new JobPortalSystemContext1();

        // GET: UserInformations
        public ActionResult Index()
        {
            return View(db.UserInformations.ToList());
        }
        public ActionResult Splash()//Splash screen
        {
            return View();
        }

        // GET: UserInformations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserInformation userInformation = db.UserInformations.Find(id);
            if (userInformation == null)
            {
                return HttpNotFound();
            }
            return View(userInformation);
        }

        // GET: UserInformations/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserInformations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserId,Password,FirstName,LastName,Age,Gender,Address,PhoneNumber,UserType")] UserInformation userInformation)
        {

            try
            {
                using (JobPortalSystemContext1 db = new JobPortalSystemContext1())
                {
                    
                    db.UserInformations.Add(userInformation);

                    db.SaveChanges();

                    Session["PhoneNumber"] = userInformation.PhoneNumber.ToString();
                    Session["Password"] = userInformation.Password.ToString();
                    Session["UserType"] = userInformation.UserType.ToString();
                    Session["FirstName"] = userInformation.FirstName.ToString();
                    return RedirectToAction("Index", "JobDetails");


                }
            }
            catch (Exception )
            {
                ModelState.AddModelError("", "Failed to Register details");
                return RedirectToAction("Create");
            }
        }

        // GET: UserInformations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserInformation userInformation = db.UserInformations.Find(id);
            if (userInformation == null)
            {
                return HttpNotFound();
            }
            return View(userInformation);
        }

        // POST: UserInformations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserId,Password,FirstName,LastName,Age,Gender,Address,PhoneNumber,UserType")] UserInformation userInformation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userInformation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(userInformation);
        }

        // GET: UserInformations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserInformation userInformation = db.UserInformations.Find(id);
            if (userInformation == null)
            {
                return HttpNotFound();
            }
            return View(userInformation);
        }

        // POST: UserInformations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UserInformation userInformation = db.UserInformations.Find(id);
            db.UserInformations.Remove(userInformation);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }


       


        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login([Bind(Include = "PhoneNumber,Password")] UserInformation reg)//Login to validate the User Credentials

        {
            try
            {

                using (JobPortalSystemContext1 db = new JobPortalSystemContext1())

                {

                    var obj = db.UserInformations.Where(a => a.PhoneNumber.Equals(reg.PhoneNumber) && a.Password.Equals(reg.Password)).FirstOrDefault();

                    if (obj != null)

                    {
                        if (obj.UserType=="Admin")
                        {
                            Session["PhoneNumber"] = obj.PhoneNumber.ToString();  

                            Session["Password"] = obj.Password.ToString();
                            Session["UserType"] = obj.UserType.ToString();
                            Session["FirstName"] = obj.FirstName.ToString();
                            return RedirectToAction("Index", "JobDetailsAdmin");
                        }
                        else if(obj.UserType == "User")
                        {
                            Session["PhoneNumber"] = obj.PhoneNumber.ToString();
                            Session["UserType"] = obj.UserType.ToString();
                            Session["Password"] = obj.Password.ToString();
                            Session["FirstName"] = obj.FirstName.ToString();

                            return RedirectToAction("Index", "JobDetails");
                        }
                        else
                        {
                            return RedirectToAction("Login");
                        }
                        

                    }
                    
                    else
                    {
                        return RedirectToAction("Login");
                    }
                }
            }

            catch (Exception e)
            {
                ModelState.AddModelError("", "Failed to Login");
                return RedirectToAction("Login");
            }

        }

        public ActionResult LogOut()//To Logout and end the user session

        {
            FormsAuthentication.SignOut();
            Session.Clear();
            Session.RemoveAll();
            Session.Abandon();
            this.Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1));
            this.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            this.Response.Cache.SetNoStore();
            Session["PhoneNumber"] = null;
            Session["UserType"] = null;
            return RedirectToAction("Login");
        }
    }
}
