using Microsoft.Data.SqlClient;
using Practise.Data;
using System.Data;

namespace Practise.Models
{
    public class Employee
    {
        public int id { get; set; }
        public string  name { get; set; }
        public string mobile { get; set; }
        public string empCode { get; set; }
        public string position { get; set; }
        public string country { get; set; }
        public string location { get; set; }
      
        public string gender { get; set; }
        public string skills { get; set; }
        public int opCode { get; set; }
        public DataSet DS { get; set; }
        public Boolean isException { get; set; }
        public string exceptionMessage { get; set; }
        public string qualList { get; set; }
    }
    public class DAL_Employee
    {
        public static void returnTable(Employee pobj)
        {
            Connection connn = new Connection();
            using (SqlCommand cmd = new SqlCommand("proc_Employee", connn.conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@name", pobj.name);
                cmd.Parameters.AddWithValue("@mobile", pobj.mobile);
                cmd.Parameters.AddWithValue("@empCode", pobj.empCode);
                cmd.Parameters.AddWithValue("@position", pobj.position);
                cmd.Parameters.AddWithValue("@country", pobj.country);
                cmd.Parameters.AddWithValue("@location", pobj.location);
                cmd.Parameters.AddWithValue("@gender", pobj.gender);
                cmd.Parameters.AddWithValue("@skills", pobj.skills);
                cmd.Parameters.AddWithValue("@opCode", pobj.opCode);
                cmd.Parameters.AddWithValue("@id", pobj.id);
                cmd.Parameters.AddWithValue("@qualList", pobj.qualList);
                cmd.Parameters.Add("@isException", SqlDbType.Bit);
                cmd.Parameters["@isException"].Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@exceptionMessage", SqlDbType.VarChar, -1);
                cmd.Parameters["@exceptionMessage"].Direction = ParameterDirection.Output;
                using (SqlDataAdapter adp = new SqlDataAdapter(cmd))
                {
                    pobj.DS = new DataSet();
                    adp.Fill(pobj.DS);
                    pobj.isException = Convert.ToBoolean(cmd.Parameters["@isException"].Value);
                    pobj.exceptionMessage = cmd.Parameters["@exceptionMessage"].Value.ToString();
                }
            }
        }
    }
    public class PAL_Employee
    {
        public static void saveEmployee(Employee pobj)
        {
            pobj.opCode = 11;
            DAL_Employee.returnTable(pobj);
        }
        public static void deleteEmployee(Employee pobj)
        {
            pobj.opCode = 31;
            DAL_Employee.returnTable(pobj);
        }
        public static void getEmployees(Employee pobj)
        {
            pobj.opCode = 41;
            DAL_Employee.returnTable(pobj);
        }
    }
}
