using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Mystore.Pages.Clients
{
    public class CreateModel : PageModel
    {

        public CrudInfo crudInfo=new CrudInfo();
        public string ErrorMessage = "";
        public string SuccessMessage = "";
        public void OnGet()
        {
        }
        public void OnPost() 
        {
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
                using(SqlConnection Connection = new SqlConnection(Connection_String))
                {
                    Connection.Open();
                    String SqlQuery = "Insert into Clients(Name,Email,Phone,Address) " +
                    " Values(@name,@email,@phone,@address)";
                    using (SqlCommand Command = new SqlCommand(SqlQuery, Connection))
                    {
                        Command.Parameters.AddWithValue("@name", crudInfo.Name);
                        Command.Parameters.AddWithValue ("@email", crudInfo.Email);
                        Command.Parameters.AddWithValue("@phone", crudInfo.Phone);
                        Command.Parameters.AddWithValue("@address", crudInfo.Address);
                        Command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception Ex)
            {
                ErrorMessage = Ex.Message; //throw;
            }
            crudInfo.Name = ""; crudInfo.Email = ""; crudInfo.Phone = ""; crudInfo.Address = "";
            SuccessMessage = "Client saved Successfully!";
            Response.Redirect("/Clients/Index");
        }
    }
}
