using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using REDCapExporter;

namespace REDCapWeb.Controllers
{
    public class UserStudiesController : Controller
    {
        private UserStudyContext db = new UserStudyContext();

        public async Task<ActionResult> Process(int? id)
        {
            if(id==null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            UserStudy selectedStudy = await db.UserStudies.FindAsync(id);

            if(selectedStudy == null)
            {
                return HttpNotFound();
            }

            ViewBag.Title = "Process";
            ViewBag.Branding = "PCF Innovation";
            REDCapExporter.DataManager dataManager = new DataManager();
            Process view = new Process();

            view.UserStudy = selectedStudy;
            view.FormMetadata = await dataManager.GetStudyFormData(selectedStudy.RedCapUrl, selectedStudy.ApiKey);

            return View(view);
        }

        // GET: UserStudies
        public async Task<ActionResult> Index()
        {
            ViewBag.Title = "Studies";
            ViewBag.Branding = "PCF Innovation";

            return View(await db.UserStudies.OrderBy(p => p.StudyName).ToListAsync());
        }

        // GET: UserStudies/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            ViewBag.Title = "Details";
            ViewBag.Branding = "PCF Innovation";

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserStudy userStudy = await db.UserStudies.FindAsync(id);
            if (userStudy == null)
            {
                return HttpNotFound();
            }
            return View(userStudy);
        }

        // GET: UserStudies/Create
        public ActionResult Create()
        {
            ViewBag.Title = "Create";
            ViewBag.Branding = "PCF Innovation";

            return View();
        }

        // POST: UserStudies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,KeyFieldName,KeyFormName,UserName,LastUpdated,ApiKey,StudyName,RedCapUrl")] UserStudy userStudy)
        {
            if (ModelState.IsValid)
            {
                userStudy.LastUpdated = DateTime.Now;
                userStudy.Created = DateTime.Now;
                userStudy.UserName = User.Identity.Name;

                db.UserStudies.Add(userStudy);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(userStudy);
        }

        // GET: UserStudies/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            ViewBag.Title = "Edit";
            ViewBag.Branding = "PCF Innovation";

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserStudy userStudy = await db.UserStudies.FindAsync(id);
            if (userStudy == null)
            {
                return HttpNotFound();
            }
            return View(userStudy);
        }

        // POST: UserStudies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,KeyFieldName,KeyFormName,UserName,LastUpdated,ApiKey,StudyName,RedCapUrl")] UserStudy userStudy)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userStudy).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(userStudy);
        }

        // GET: UserStudies/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserStudy userStudy = await db.UserStudies.FindAsync(id);
            if (userStudy == null)
            {
                return HttpNotFound();
            }
            return View(userStudy);
        }

        // POST: UserStudies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            UserStudy userStudy = await db.UserStudies.FindAsync(id);
            db.UserStudies.Remove(userStudy);
            await db.SaveChangesAsync();
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
