using System.Data;
using System.Data.SqlClient;

namespace AdoTest.Models
{
    public class Db
    {
        //private readonly string ConnectionString = "Data Source=LAPTOP-P576FSR6\\SQLEXPRESS;Initial Catalog=db301221;uid=sa;pwd=sql;";
        private readonly IConfiguration _configuration;

        private SqlConnection con;
        public Db(IConfiguration configuration)
        {
            _configuration = configuration;
            con = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
        }

        public async Task<bool> ExecuteAsync(Employee emp, int type)
        {

          //  string sql = _configuration.GetConnectionString("DefaultConnection");
            bool result = false;
            try
            {
                using (SqlCommand cmd = new SqlCommand("sp_tblEmployee_update", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@inType", type);
                    cmd.Parameters.AddWithValue("@id", emp.id);
                    cmd.Parameters.AddWithValue("@name", emp.name);
                    cmd.Parameters.AddWithValue("@mobile", emp.mobile);
                    cmd.Parameters.AddWithValue("@email", emp.email);
                    await con.OpenAsync();
                    int ires = await cmd.ExecuteNonQueryAsync();
                    result= ires > 0;
                    await con.CloseAsync();
                }
            }
            finally
            {
                if(con.State == ConnectionState.Open)
                {
                    await con.CloseAsync();
                }
            }
           return result;
        }

        public async Task<IEnumerable<Dictionary<string, object>>> getEmployeed(int type, int id=0)
        {
            List<Dictionary<string, object>> empList = new List<Dictionary<string, object>>();
            try
            {
                using (SqlCommand cmd = new SqlCommand("sp_tblEmployee_details", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@inType", type);
                    cmd.Parameters.AddWithValue("@id", id);
                    await con.OpenAsync();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Dictionary<string, object> emp = new Dictionary<string, object>();
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            emp.Add(reader.GetName(i), reader.GetValue(i));
                        }
                        empList.Add(emp);
                    }
                    await reader.CloseAsync();
                    await con.CloseAsync();
                }
            }
            catch (Exception ex)
            {
                
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    await con.CloseAsync();
                }
            }
            return empList;
        }

        public async Task<IEnumerable<Employee>> getEmployee(int type, int id = 0)
        {
            List<Employee> empList = new List<Employee>();
            try
            {
                using (SqlCommand cmd = new SqlCommand("sp_tblEmployee_details", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@inType", type);
                    cmd.Parameters.AddWithValue("@id", id);
                    await con.OpenAsync();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Employee emp = new Employee();
                        emp.id = reader.GetInt32("id");
                        emp.name = reader.GetString("name");
                        emp.mobile = reader.GetString("mobile");
                        emp.email = reader.GetString("email");
                        empList.Add(emp);
                    }
                    await reader.CloseAsync();
                    await con.CloseAsync();
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    await con.CloseAsync();
                }
            }
            return empList;
        }
    }
}
