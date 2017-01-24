using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ArtProject.DAL;
using ArtProject.Models;
using PagedList;

namespace ArtProject.Controllers
{
    public class PaintersController : Controller
    {
        private LibraryContext db = new LibraryContext();

        // GET: Painters
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "Name_desc" : "";
            ViewBag.CurrentSort = sortOrder;

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var painters = from s in db.Painters
                           select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                painters = painters.Where(s => s.LastName.ToUpper().Contains(searchString.ToUpper())
                                    || s.FirstName.ToUpper().Contains(searchString.ToUpper()));
            }
            switch (sortOrder)
            {
                case "Name_desc":
                    painters = painters.OrderByDescending(s => s.LastName);
                    break;
                default:
                    painters = painters.OrderBy(s => s.LastName);
                    break;
            }

            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(painters.ToPagedList(pageNumber,pageSize));
        }

        // GET: Painters/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Painter painter = db.Painters.Find(id);
            if (painter == null)
            {
                return HttpNotFound();
            }
            return View(painter);
        }

        // GET: Painters/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Painters/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PainterID,FirstName,LastName")] Painter painter)
        {
            try
            {
               if (ModelState.IsValid)
               {
                   db.Painters.Add(painter);
                   db.SaveChanges();
                   return RedirectToAction("Index");
               }
            }
            catch (DataException /* dex */)
            {
                //Log the error (uncomment dex variable name after DataException and add a line here to write a log.
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            return View(painter);
        }

        // GET: Painters/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Painter painter = db.Painters.Find(id);
            if (painter == null)
            {
                return HttpNotFound();
            }
            return View(painter);
        }

        // POST: Painters/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PainterID,FirstName,LastName")] Painter painter)
        {
            if (ModelState.IsValid)
            {
                db.Entry(painter).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(painter);
        }

        // GET: Painters/Delete/5
        public ActionResult Delete(bool? saveChangesError = false, int id = 0)
        {
            if (saveChangesError.GetValueOrDefault())
            {
                ViewBag.ErrorMessage = "Delete failed. Try again, and if the problem persists see your system administrator.";
            }
            Painter painter = db.Painters.Find(id);
            if (painter == null)
            {
                return HttpNotFound();
            }
            return View(painter);
        }

        // POST: Painters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                Painter painter = db.Painters.Find(id);
                db.Painters.Remove(painter);
                db.SaveChanges();
            }
            catch (DataException/* dex */)
            {
                // uncomment dex and log error. 
                return RedirectToAction("Delete", new { id = id, saveChangesError = true });
            }
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
    }
}
