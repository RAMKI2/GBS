using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using JPSMVC_Ramya.Models;

namespace JPSMVC_Ramya.Controllers
{
    [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
    public class JobDetailsAdminController : Controller
    {
        private JobPortalSystemContext1 db = new JobPortalSystemContext1();

        // GET: JobDetailsAdmin
        public ActionResult Index()
        {
            return View(db.JobDetails.ToList());
        }

        // GET: JobDetailsAdmin/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JobDetail jobDetail = db.JobDetails.Find(id);
            if (jobDetail == null)
            {
                return HttpNotFound();
            }
            return View(jobDetail);
        }

        // GET: JobDetailsAdmin/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: JobDetailsAdmin/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "JobId,Employer,Address,PhoneNumber,EmailId,SkillsRequired,Qualification,Location,Salary,NoOfVaccancies")] JobDetail jobDetail)
        {
            if (ModelState.IsValid)
            {
                db.JobDetails.Add(jobDetail);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(jobDetail);
        }

        // GET: JobDetailsAdmin/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JobDetail jobDetail = db.JobDetails.Find(id);
            if (jobDetail == null)
            {
                return HttpNotFound();
            }
            return View(jobDetail);
        }

        // POST: JobDetailsAdmin/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "JobId,Employer,Address,PhoneNumber,EmailId,SkillsRequired,Qualification,Location,Salary,NoOfVaccancies")] JobDetail jobDetail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(jobDetail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(jobDetail);
        }

        // GET: JobDetailsAdmin/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JobDetail jobDetail = db.JobDetails.Find(id);
            if (jobDetail == null)
            {
                return HttpNotFound();
            }
            return View(jobDetail);
        }

        // POST: JobDetailsAdmin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            JobDetail jobDetail = db.JobDetails.Find(id);
            db.JobDetails.Remove(jobDetail);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Search(string employer)
        {

            if (string.IsNullOrEmpty(employer))
            {
                var jobDetails = db.JobDetails.ToList();
                return PartialView("Search", jobDetails);
            }
            else
            {
                var jobDetails = db.JobDetails.Where(p => p.Employer.StartsWith(employer.ToLower())).ToList();
                return PartialView("Search", jobDetails);
            }


        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
