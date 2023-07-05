using Microsoft.AspNetCore.Mvc.Rendering;

namespace MyDemoApp.Models
{
    public interface IEmployeeRepo
    {
        public List<Employees> GetEmployees();
        public Employees GetEmployee(int id);
        public Employees AddEmployee(Employees employee);
        public Employees UpdateEmployee(Employees employee);
        public Employees DeleteEmployee(int id);
        Task<IEnumerable<SelectListItem>> Countrylist();
        Task<IEnumerable<SelectListItem>> statesList(int id);
        public Task <Country> CountryName(int id);
    }
}
