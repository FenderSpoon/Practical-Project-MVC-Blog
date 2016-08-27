using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace MVC_Blog.Controllers
{
    public class ImageGalleryController : Controller
    {
        private Entities dc = new Entities();
        
        // GET: ImageGallery
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Gallery()
        {
            List<ImageGallery> all = new List<ImageGallery>();
            using (Entities dc = new Entities())
            {
                all = dc.ImageGallery.ToList();
            }
            return View(all);
        }
        [Authorize]
        public ActionResult Upload()
        {
            return View();
        }
        
        [HttpPost]
        public ActionResult Upload(ImageGallery IG)
        {
            if (IG.File.ContentLength > (2*1024*1024))
            {
                ModelState.AddModelError("CustomError", "File size must be less 2 MB");
                return View();
            }
            if (!(IG.File.ContentType == "image/jpeg" || IG.File.ContentType == "image/gif"))
            {
                ModelState.AddModelError("CustomError", "File type allowed : jpeg and gif");
                return View();
            }

            IG.FileName = IG.File.FileName;
            IG.ImageSize = IG.File.ContentLength;

            byte[] data = new byte[IG.File.ContentLength];
            IG.File.InputStream.Read(data, 0, IG.File.ContentLength);

            IG.ImageData = data;
            using (Entities dc = new Entities())
            {
                dc.ImageGallery.Add(IG);
                dc.SaveChanges();
            }

            return RedirectToAction("Gallery");
        }
        // GET: Posts/Delete/5
        [Authorize(Roles = "Administrators")]
        public ActionResult Delete(int? ImageId)
        {
          
            if (ImageId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ImageGallery post = dc.ImageGallery.Find(ImageId);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // POST: Posts/Delete/5
        [Authorize(Roles = "Administrators")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int ImageId)
        {
            ImageGallery post = dc.ImageGallery.Find(ImageId);
            dc.ImageGallery.Remove(post);
            dc.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                dc.Dispose();
            }
            base.Dispose(disposing);
        }

    }
}