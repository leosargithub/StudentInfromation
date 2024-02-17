using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StudentInfromation.Pages.student;
using System.Data.SqlClient;

namespace StudentInfromation.Pages.Student
{
    public class CreateModel : PageModel
    {
        public StudentInfo student = new StudentInfo();
        public String errorMessage = "";
        public String successMessage = "";

        public void OnGet()
        {
        }
        public void OnPost() { 
            
            student.name = Request.Form["name"];
            student.email = Request.Form["email"];
            student.phone = Request.Form["phone"];
            student.address = Request.Form["address"];
            student.created_at = System.DateTime.Now.ToString();

            if(student.name.Length ==0 || student.email.Length ==0 || student.phone.Length==0 || student.address.Length == 0)
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
                        "INSERT INTO students " + "(name, email, phone, address) VALUES" + "(@name, @email, @phone, @address);";
                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        cmd.Parameters.AddWithValue("@name", student.name);
                        cmd.Parameters.AddWithValue("@email", student.email);
                        cmd.Parameters.AddWithValue("@phone", student.phone);
                        cmd.Parameters.AddWithValue("@address", student.address);
                        cmd.ExecuteNonQuery();
                    }
                }

            } catch (Exception ex)
            {
                errorMessage = ex.Message;
                
            }

            student.name = ""; student.email = ""; student.phone = ""; student.address = "";
            successMessage = "Student has been added successfully";
         Response.Redirect("/Student/Index");
        
        }
    }
}
