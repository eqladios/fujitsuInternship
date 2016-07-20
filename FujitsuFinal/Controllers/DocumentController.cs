using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FujitsuFinal.Models;
using System.IO;

namespace FujitsuFinal.Controllers
{
    public class DocumentController : Controller
    {
        private DocumentDBContext db = new DocumentDBContext();

        // GET: /Document/
        public ActionResult Index()
        {
            return View(db.Documents.ToList());
        }

        // GET: /Document/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Document document = db.Documents.Find(id);
            if (document == null)
            {
                return HttpNotFound();
            }
            return View(document);
        }

        // GET: /Document/Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Document document)
        {
            if (ModelState.IsValid)
            {
                for (int i = 0; i < Request.Files.Count; i++)
                {
                    var file = Request.Files[i];
                    if (file != null && file.ContentLength > 0)
                    {
                        var fileName = Path.GetFileName(file.FileName);
                        Document newDocument = new Document()
                        {
                            documentTitle = fileName,
                            documentExtension = Path.GetExtension(fileName),
                            documentPath = Path.Combine(Server.MapPath("~/App_Data/"), fileName),
                            documentUploadedAt = DateTime.Now,
                        };
                        file.SaveAs(newDocument.documentPath);
                        newDocument.documentSize =BytesToString( new FileInfo(newDocument.documentPath).Length);
                        db.Documents.Add(newDocument);
                        db.SaveChanges();
                    }
                }

                return RedirectToAction("Index");
            }

            return View(db.Documents.ToList());
        }

        static String BytesToString(long byteCount)
        {
            string[] suf = { "B", "KB", "MB", "GB", "TB", "PB", "EB" }; //Longs run out around EB
            if (byteCount == 0)
                return "0" + suf[0];
            long bytes = Math.Abs(byteCount);
            int place = Convert.ToInt32(Math.Floor(Math.Log(bytes, 1024)));
            double num = Math.Round(bytes / Math.Pow(1024, place), 1);
            return (Math.Sign(byteCount) * num).ToString() + " " + suf[place];
        }

        // POST: /Document/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include="documentID,documentTitle,documentExtension,documentPath,documentSize,documentUploadedAt")] Document document)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Documents.Add(document);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    return View(document);
        //}

        // GET: /Document/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Document document = db.Documents.Find(id);
            if (document == null)
            {
                return HttpNotFound();
            }
            return View(document);
        }

        // POST: /Document/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="documentID,documentTitle,documentExtension,documentPath,documentSize,documentUploadedAt")] Document document)
        {
            if (ModelState.IsValid)
            {
                db.Entry(document).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(document);
        }

        // GET: /Document/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Document document = db.Documents.Find(id);
            if (document == null)
            {
                return HttpNotFound();
            }
            return View(document);
        }

        // POST: /Document/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Document document = db.Documents.Find(id);
            System.IO.File.Delete(document.documentPath);
            db.Documents.Remove(document);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        //public ActionResult Delete(int[] documentsIDs)
        //{
        //        for(int i =0; i < documentsIDs.Length; i++)
        //        {
        //            Document obj = db.Documents.Find(documentsIDs[i]);
        //            db.Documents.Remove(obj);
        //        }
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //}

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
