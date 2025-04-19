using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Mvc;
using Practise.Models;
using System.Data;
using System.Net;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Practise.Controllers
{
    public class EmployeeController : Controller
    {
        public IActionResult Employee()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddEmployee([FromBody] Employee pobj)
        {
            PAL_Employee.saveEmployee(pobj);
            if (pobj.isException)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new { success = false, data = "Not Saved" });

            }
            return StatusCode((int)HttpStatusCode.OK, new { success = true, data = "Saved Successfully" });

        }
        [HttpGet]
        public IActionResult GetEmployees()
        {
            Employee pobj = new Employee();
            PAL_Employee.getEmployees(pobj);
            List<Dictionary<string, object>> employees = new List<Dictionary<string, object>>();
            if (pobj.DS != null && pobj.DS.Tables.Count > 0)
            {

                foreach (DataTable dt in pobj.DS.Tables)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        Dictionary<string, object> employee = new Dictionary<string, object>();

                        foreach (DataColumn column in dt.Columns)
                        {
                            employee[column.ColumnName] = row[column];
                        }

                        employees.Add(employee);
                    }
                }
            }
            if (pobj.isException)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new { success = false, data = "Not Saved" });

            }
            return StatusCode((int)HttpStatusCode.OK, new { success = true, data = employees });

        }
        [HttpPost]
        public IActionResult DeleteEmployee([FromBody] Employee pobj)
        {
            PAL_Employee.deleteEmployee(pobj);
            if (pobj.isException)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new { success = false, data = "Not Deleted" });
            }
            return StatusCode((int)HttpStatusCode.OK, new { success = true, data = "Delete Succesfully" });
        }
    }
}
