using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ZuciQuizMVC.Controllers
{
    public class IndexofAdminController : Controller
    {
        // GET: IndexofAdminController
        public ActionResult Index()
        {
            return View();
        }

        // GET: IndexofAdminController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: IndexofAdminController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: IndexofAdminController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: IndexofAdminController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: IndexofAdminController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: IndexofAdminController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: IndexofAdminController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
