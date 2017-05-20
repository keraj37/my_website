using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using my_website.Models;
using System.Web.Security;
using my_website.Helpers;

namespace my_website.Controllers
{    
    public class BlogController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index(int? page, int? pageSize)
        {
            int finalPageSize = pageSize ?? 2;

            if (finalPageSize < 1)
                finalPageSize = 1;

            DataCollection.DataCollection.Save("Blog GET", "Someone is looking at Blog page\nPage nr: " + (page ?? 1).ToString() + "\n\n" + new UserClient(Request).ToString());

            PaginatedList<Blog> paginated = new PaginatedList<Blog>(db.Blogs.OrderByDescending(x => x.ID), page ?? 1, finalPageSize);
            return View(paginated);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Blog blog = db.Blogs.Find(id);
            if (blog == null)
            {
                return HttpNotFound();
            }
            return View(blog);
        }

        [Authorize(Roles = Users.Users.Roles.ADMIN)]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = Users.Users.Roles.ADMIN)]
        [ValidateInput(false)]
        public ActionResult Create([Bind(Include = "ID,Title,Time,User,Content")] Blog blog)
        {
            if (ModelState.IsValid)
            {
                SetupCurrentToBlog(blog);
                db.Blogs.Add(blog);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(blog);
        }

        [Authorize(Roles = Users.Users.Roles.ADMIN)]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Blog blog = db.Blogs.Find(id);
            if (blog == null)
            {
                return HttpNotFound();
            }
            return View(blog);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = Users.Users.Roles.ADMIN)]
        [ValidateInput(false)]
        public ActionResult Edit([Bind(Include = "ID,Title,Time,User,Content")] Blog blog)
        {
            if (ModelState.IsValid)
            {
                db.Entry(blog).State = EntityState.Modified;
                SetupCurrentToBlog(blog);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(blog);
        }

        [Authorize(Roles = Users.Users.Roles.ADMIN)]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Blog blog = db.Blogs.Find(id);
            if (blog == null)
            {
                return HttpNotFound();
            }
            return View(blog);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = Users.Users.Roles.ADMIN)]
        public ActionResult DeleteConfirmed(int id)
        {
            Blog blog = db.Blogs.Find(id);
            db.Blogs.Remove(blog);
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

        [NonAction]
        private void SetupCurrentToBlog(Blog blog)
        {
            blog.User = User.Identity.Name;
            blog.Time = DateTime.Now;
        }
    }
}
