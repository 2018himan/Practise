using Microsoft.Data.SqlClient;

namespace Practise.Data
{
    public class Connection
    {
        private readonly string constr;
        public SqlConnection conn;
        public Connection()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");
            IConfiguration config = builder.Build();
            constr = config.GetConnectionString("DefaultConnection");
            conn = new SqlConnection(constr);

        }

    }
}
