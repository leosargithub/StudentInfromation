
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace StudentInfromation.Pages.student
{
    public class IndexModel : PageModel
    {
        public List<StudentInfo> lIiststudent = new List<StudentInfo>();
        public void OnGet()
        {
            try
            {
                String ConnectionString = " Data Source =.\\sqlexpress; Integrated Security = True; Encrypt = False";
                using (SqlConnection con = new SqlConnection(ConnectionString))
                {
                    con.Open();
                    String sql = "select * from students";
                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                StudentInfo student = new StudentInfo();
                                student.id = "" + reader.GetInt32(0);
                                student.name = reader.GetString(1);
                                student.email = reader.GetString(2);
                                student.phone = reader.GetString(3);
                                student.address = reader.GetString(4);
                                student.created_at = reader.GetDateTime(5).ToString();
                                lIiststudent.Add(student);


                            }
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                
            }

        }
    }
    public class StudentInfo
    {
        public String id;
        public String name;
        public String email;
        public String phone;
        public String address;
        public String created_at;

    }
}
