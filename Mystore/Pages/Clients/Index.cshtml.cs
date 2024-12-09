using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Mystore.Pages.Clients
{
    public class IndexModel : PageModel
    {
        public List<CrudInfo> ListCrudInfo = new List<CrudInfo>();
        public void OnGet()
        {
            try
            {
                String Connection_String = "Data Source=DESKTOP-2CCFP7C;Initial Catalog=Mystore;User ID=sa;Password=123;Encrypt=False";
            using (SqlConnection Connection = new SqlConnection(Connection_String))
                {
                    Connection.Open();
                    String SqlQuery = "SELECT * FROM Clients";
                    using (SqlCommand command = new SqlCommand(SqlQuery, Connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                CrudInfo crudInfo = new CrudInfo();
                                crudInfo.Id =  reader.GetInt32(0);
                                crudInfo.Name = reader.GetString(1);
                                crudInfo.Email = reader.GetString(2);
                                crudInfo.Phone = reader.GetString(3);
                                crudInfo.Address = reader.GetString(4);
                                crudInfo.Created_at=reader.GetDateTime(5);
                                ListCrudInfo.Add(crudInfo);
                            }
                        }
                    }
                }
            }
            catch (Exception Ex)
            {

                Console.WriteLine("Exception: " + Ex.ToString);
            }
        }
    }
    public class CrudInfo
    {

        public int Id;
        public string Name;
        public string Email;
        public string Phone;
        public string Address;
        public DateTime Created_at;

    }
}
