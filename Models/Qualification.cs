using Microsoft.Data.SqlClient;
using Practise.Data;
using System.Data;

namespace Practise.Models
{
    public class Qualification
    {
        public int id { get; set; }
        public string qualificationName { get; set; }
        public int opCode { get; set; }
        public Boolean isException { get; set; }
        public string exceptionMessage { set; get; }
        public DataSet DS { get; set; }
    }
    public class DAL_Qualification
    {
        public static void returnTable(Qualification pobj)
        {
            Connection connn = new Connection();
            using (SqlCommand cmd = new SqlCommand("proc_Qualification", connn.conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@opCode", pobj.opCode);
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
    public class PAL_Qualification
    {
        public static void getQualification(Qualification pobj)
        {
            pobj.opCode = 41;
            DAL_Qualification.returnTable(pobj);

        }
    }
}
