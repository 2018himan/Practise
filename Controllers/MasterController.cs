using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Practise.Models;
using System.Data;
using System.Net;

namespace Practise.Controllers
{
    public class MasterController : Controller
    {
        [HttpGet]
        public IActionResult GetQualification()
        {
            Qualification qual = new Qualification();
            PAL_Qualification.getQualification(qual);

            if (qual.DS != null && qual.DS.Tables.Count > 0)
            {
                List<object> qualifications = new List<object>();

                foreach (DataTable dt in qual.DS.Tables)
                {
                    foreach (DataRow row in dt.Rows)
                    {

                        
                        var qualification = new
                        {
                            id = row["id"],
                            qualificationName = row["qualificationName"]
                        };
                        qualifications.Add(qualification);
                    }
                }

                return StatusCode((int)HttpStatusCode.OK, new { success = true, data = qualifications });
            }

            return StatusCode((int)HttpStatusCode.NoContent, new { success = false, message = "No data found." });
        }
        
        }
}
