using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyDemoApp.Data;

namespace MyDemoApp.Models
{
    public class EmployeeRepo : IEmployeeRepo
    {
        private readonly AppDbContext appDbContext;

        public EmployeeRepo(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;            //DI
        }

        public Employees AddEmployee(Employees employee)
        {
            appDbContext.Employees.Add(employee);
            appDbContext.SaveChanges();
            return employee;
        }

        public async Task<IEnumerable<SelectListItem>> Countrylist()
        {
            var data = appDbContext.Countries.Select(c => new { Name = c.CountryName, id = c.Id });
            var res = await data.Select(x => new SelectListItem { Text = x.Name, Value = x.id.ToString() }).ToListAsync();
            return res;
        }
        public async Task<Country> CountryName(int id)
        {
            var data= await appDbContext.Countries.FirstOrDefaultAsync(x => x.Id == id);
            return  data;
            
        }
        public Employees DeleteEmployee(int id)
        {
            Employees employees = appDbContext.Employees.Find(id);
            if(employees!=null)
            {
                appDbContext.Employees.Remove(employees);
                appDbContext.SaveChanges();
            }
            return employees;
        }

        public Employees GetEmployee(int id)
        {
            return appDbContext.Employees.FirstOrDefault(x => x.Id == id);
        }

        public List<Employees> GetEmployees()
        {
            return appDbContext.Employees.ToList();
        }

        public async Task<IEnumerable<SelectListItem>> statesList(int id)
        {
            var data = appDbContext.States.Where(x => x.CountryId == id).Select(s => new { Name = s.StateName, id = s.StateId });
            var res = await data.Select(x => new SelectListItem { Text = x.Name, Value = x.Name }).ToListAsync();
            return res;
        }

        public Employees UpdateEmployee(Employees employee)
        {
           var emp = appDbContext.Employees.Attach(employee);
           emp.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
           appDbContext.SaveChanges();
           return employee;
        }
    }
}

