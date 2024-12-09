using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using System.Reflection.PortableExecutable;

namespace Mystore.Pages.Clients
{
    public class EditModel : PageModel
    {
        public CrudInfo crudInfo = new CrudInfo();
        public string ErrorMessage = "";
        public string SuccessMessage = "";

        public void OnGet()
        {
            string id = Request.Query["id"];
            try
            {
                String Connection_string = "Data Source=DESKTOP-2CCFP7C;Initial Catalog=Mystore;User ID=sa;Password=123;Encrypt=False";
                
                using(SqlConnection Connection = new SqlConnection(Connection_string))
                {
                    Connection.Open();
                    String SqlQuery = "Select * from Clients where ID=@id";
                    using(SqlCommand command=new SqlCommand(SqlQuery, Connection))
                    {
                        command.Parameters.AddWithValue("@Id", id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                crudInfo.Id =  reader.GetInt32(0);
                                crudInfo.Name = reader.GetString(1);
                                crudInfo.Email = reader.GetString(2);
                                crudInfo.Phone = reader.GetString(3);
                                crudInfo.Address  = reader.GetString(4);

                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                ErrorMessage = ex.Message; //throw;
            }
        }
        public void OnPost()
        {
            crudInfo.Id = int.Parse(Request.Form["id"]);
            crudInfo.Name = Request.Form["name"];
            crudInfo.Email = Request.Form["email"];
            crudInfo.Phone = Request.Form["phone"];
            crudInfo.Address = Request.Form["address"];

            if (crudInfo.Name.Length == 0 || crudInfo.Email.Length == 0 ||
               crudInfo.Phone.Length == 0 || crudInfo.Address.Length == 0)
            {
                ErrorMessage = "All field are required";
                return;
            }

            try
            {
                String Connection_String = "Data Source=DESKTOP-2CCFP7C;Initial Catalog=Mystore;User ID=sa;Password=123;Encrypt=False";
                using (SqlConnection Connection = new SqlConnection(Connection_String))
                {
                    Connection.Open();
                    String SqlQuery = "Update Clients set Name = @name, Email = @email," + 
                        " Phone = @phone, Address = @address where Id = @id";
                    using (SqlCommand command = new SqlCommand(SqlQuery, Connection))
                    {
                        command.Parameters.AddWithValue("@id", crudInfo.Id);
                        command.Parameters.AddWithValue("@name", crudInfo.Name);
                        command.Parameters.AddWithValue("@email", crudInfo.Email);
                        command.Parameters.AddWithValue("@phone", crudInfo.Phone);
                        command.Parameters.AddWithValue("@address", crudInfo.Address);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {

                ErrorMessage = ex.Message; //throw;
            }
            Response.Redirect("/Clients/Index");
        }

    }
}
