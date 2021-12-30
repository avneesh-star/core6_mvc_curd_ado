using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AdoTest.Models;

namespace AdoTest.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly Db _db;

        public EmployeeController(Db db)
        {
            _db = db;
        }

        // GET: EmployeeController
        public async Task<ActionResult> Index()
        {
           
            var data = await _db.getEmployee(1);
            return View(data);
        }

        // GET: EmployeeController/Details/5
        public async Task<ActionResult> Details(int id)
        {
           
            var data = (await _db.getEmployee(1,id)).FirstOrDefault();
            return View(data);
        }

        // GET: EmployeeController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EmployeeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(IFormCollection collection)
        {
            try
            {
                Employee employee = new Employee() 
                {
                    id =0,
                    name = collection["name"],
                    mobile= collection["mobile"],
                    email = collection["email"]
                };

               
                await _db.ExecuteAsync(employee,1);
                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                ViewBag.msg = ex.Message;
                return View();
            }
        }

        // GET: EmployeeController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
           
            var data = (await _db.getEmployee(1, id)).FirstOrDefault();
            return View(data);
        }

        // POST: EmployeeController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, IFormCollection collection)
        {
            try
            {
                Employee employee = new Employee()
                {
                    id = id,
                    name = collection["name"],
                    mobile = collection["mobile"],
                    email = collection["email"]
                };
               
                await _db.ExecuteAsync(employee, 2);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: EmployeeController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
           
            var data = (await _db.getEmployee(1, id)).FirstOrDefault();
            return View(data);
        }

        // POST: EmployeeController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(IFormCollection collection)
        {
            try
            {
                Employee employee = new Employee()
                {
                    id = Convert.ToInt32(collection["id"]),
                    name = String.Empty,
                    mobile = String.Empty,
                    email = String.Empty
                };
               
                await _db.ExecuteAsync(employee, 3);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
