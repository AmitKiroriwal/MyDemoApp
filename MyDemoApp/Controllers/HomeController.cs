using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MyDemoApp.Models;
using System.Diagnostics;

namespace MyDemoApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IEmployeeRepo employeeRepo;
        public HomeController(ILogger<HomeController> logger, IEmployeeRepo employeeRepo)
        {
            _logger = logger;
            this.employeeRepo = employeeRepo;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.PageTitle = "Register Employee";
            ViewBag.Country = await employeeRepo.Countrylist();
            return View();
        }
        
       
        [HttpPost]
        public async Task<IActionResult> Registration(Employees employees)
        {
            if (ModelState.IsValid)
            {
                var x = Convert.ToInt32(employees.Country);
              var data  = await employeeRepo.CountryName(x);
               employees.Country= data.CountryName;
             
            employeeRepo.AddEmployee(employees);
            return RedirectToAction("EmployeeData");
            }
            return View(employees);
        }
        public IActionResult EmployeeData()
        {
            List<Employees> employees= employeeRepo.GetEmployees();
            return View(employees);
        }

        public IActionResult EmployeeDetails(int id)
        {
            if(id==null || id==0)
            {
                return NotFound();
            }
            var emp=employeeRepo.GetEmployee(id);
            if(emp==null)
            {
                return BadRequest();
            }
            return View(emp);
        }

        public async Task<IActionResult> FetchState(int id)
        {
            var state = await employeeRepo.statesList(id);
            return Json(state);
        }
     
     
        
        public IActionResult Delete(int id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var delId = employeeRepo.GetEmployee(id);
            if (delId != null)
            {
                employeeRepo.DeleteEmployee(delId.Id);
                return RedirectToAction("EmployeeData");
            }
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var emp = employeeRepo.GetEmployee(id);
            ViewBag.Country = await employeeRepo.Countrylist();
            ViewBag.PageTitle = "Update Employee";
            return View(emp);
        }
        [HttpPost]
        public async Task< IActionResult> Update(Employees employees)
        {
            if(ModelState.IsValid)
            {
                var emp= employeeRepo.GetEmployee(employees.Id);
                if(emp!=null)
                {
                    var x = Convert.ToInt32(employees.Country);
                    var data = await employeeRepo.CountryName(x);
                    employees.Country = data.CountryName;
                    emp.Name = employees.Name;
                    emp.Email = employees.Email;
                    emp.State = employees.State;
                    emp.Country = employees.Country;
                }
               
                employeeRepo.UpdateEmployee(emp);
                return RedirectToAction("EmployeeData");
            }
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}