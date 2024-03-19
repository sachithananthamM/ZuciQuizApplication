using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ZuciQuizMVC.Controllers
{
    public class IndexofUserController : Controller
    {
        // GET: IndexofUserController
        public ActionResult Index()
        {
            return View();
        }

        // GET: IndexofUserController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: IndexofUserController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: IndexofUserController/Create
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

        // GET: IndexofUserController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: IndexofUserController/Edit/5
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

        // GET: IndexofUserController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: IndexofUserController/Delete/5
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
