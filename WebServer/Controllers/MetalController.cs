using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebServer.Classes;

namespace WebServer.Controllers
{
    [Route("metal")]
    public class MetalController : Controller
    {
        
        // GET: MetalController
        public ActionResult Index()
        {
            return View();
        }

        // GET: MetalController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: MetalController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MetalController/Create
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

        // GET: MetalController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: MetalController/Structures
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Structures(MetalStructure metalStructure)
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

        // GET: MetalController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: MetalController/Delete/5
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
