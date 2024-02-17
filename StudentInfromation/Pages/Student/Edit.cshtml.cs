using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StudentInfromation.Pages.student;
using System.Data.SqlClient;

namespace StudentInfromation.Pages.Student
{
    public class EditModel : PageModel
    {
        public StudentInfo student = new StudentInfo();
        public String errorMessage = "";
        public String successMessage = "";
        public void OnGet()
        {
            String id = Request.Query["id"];

            try
            {
                string ConnecttionString = "Data Source=.\\sqlexpress;Integrated Security=True;Encrypt=False";
                using (SqlConnection con = new SqlConnection(ConnecttionString))
                {
                    con.Open();
                    String sql =
                        "SELECT * FROM students WHERE id = @id";
                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                student.id = "" + reader.GetInt32(0);
                                student.name = reader.GetString(1);
                                student.email = reader.GetString(2);
                                student.phone = reader.GetString(3);
                                student.address = reader.GetString(4);

                            }
                        }

                    }


                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }
        }
        public void OnPost()
        {
       student.id = Request.Form["id"];
            student.name = Request.Form["name"];
            student.email = Request.Form["email"];
            student.phone = Request.Form["phone"];
            student.address = Request.Form["address"];
           


            if (student.name.Length == 0 || student.email.Length == 0 || student.phone.Length == 0 || student.address.Length == 0)
            {
                errorMessage = " All fields are required";
                return;
            }
            // save the student to the database

            try
            {
                string ConnecttionString = "Data Source=.\\sqlexpress;Integrated Security=True;Encrypt=False";
                using (SqlConnection con = new SqlConnection(ConnecttionString))
                {
                    con.Open();
                    String sql =
                        "UPDATE students " + "SET name = @name, email = @email, phone = @phone, address = @address WHERE id = @id";
                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        cmd.Parameters.AddWithValue("@name", student.name);
                        cmd.Parameters.AddWithValue("@email", student.email);
                        cmd.Parameters.AddWithValue("@phone", student.phone);
                        cmd.Parameters.AddWithValue("@address", student.address);
                        cmd.Parameters.AddWithValue("@id", student.id);
                        cmd.ExecuteNonQuery();
                    }
                }

            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }
            Response.Redirect("/Student/Index");

        }
    }
}
