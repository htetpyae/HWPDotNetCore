using HWPDotNetCore.Database.Models;
using HWPDotNetCore.RestApi.DataModels;
using HWPDotNetCore.RestApi.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data;

namespace HWPDotNetCore.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogsAdoDotNetController : ControllerBase
    {
        private readonly string _connectionString = "Data Source=.;Initial Catalog=DotNetDB;User ID=sa;Password=sa@123;TrustServerCertificate=True";
        [HttpGet]
        public IActionResult GetBlogs()
        {
            List<BlogViewModel> lst = new List<BlogViewModel>();
            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();

            string query = @"SELECT [BlogId]
      ,[BlogTitle]
      ,[BlogAuthor]
      ,[BlogContent]
      ,[DeleteFlag]
  FROM [dbo].[Tbl_Blog] where DeleteFlag = 0";

            SqlCommand cmd = new SqlCommand(query, connection);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Console.WriteLine(reader["BlogId"]);
                Console.WriteLine(reader["BlogTitle"]);
                Console.WriteLine(reader["BlogAuthor"]);
                Console.WriteLine(reader["BlogContent"]);

                lst.Add(new BlogViewModel
                {
                    Id = Convert.ToInt32(reader["BlogId"]),
                    Title = Convert.ToString(reader["BlogTitle"]),
                    Author = Convert.ToString(reader["BlogAuthor"]),
                    Content = Convert.ToString(reader["BlogContent"]),
                    DeleteFlag = Convert.ToBoolean(reader["DeleteFlag"]),
                });
            }
            connection.Close();
            return Ok(lst);
        }

        [HttpGet("{id}")]
        public IActionResult EditBlogs(int id, BlogViewModel blog)
        {
            List<BlogViewModel > lst = new List<BlogViewModel>();
            SqlConnection connection = new SqlConnection(_connectionString);    
            connection.Open();

            string query = @"SELECT [BlogId]
      ,[BlogTitle]
      ,[BlogAuthor]
      ,[BlogContent]
      ,[DeleteFlag]
  FROM [dbo].[Tbl_Blog] where BlogId = @BlogId";

            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@BlogId", id);
            SqlDataReader dr = cmd.ExecuteReader();
           
            while (dr.Read())
            {

                Console.WriteLine(dr["BlogId"]);
                Console.WriteLine(dr["BlogTitle"]);
                Console.WriteLine(dr["BlogAuthor"]);
                Console.WriteLine(dr["BlogContent"]);
                lst.Add(new BlogViewModel
                {
                    Id = Convert.ToInt32(dr["BlogId"]),
                    Title = Convert.ToString(dr["BlogTitle"]),
                    Author = Convert.ToString(dr["BlogAuthor"]),
                    Content = Convert.ToString(dr["BlogContent"])
                });

            }
            
            //int result = cmd.ExecuteNonQuery();
            connection.Close();
            return Ok(lst);
        }

        [HttpPost]
        public IActionResult CreateBlogs(BlogViewModel blog)
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();

            string query = @"INSERT INTO [dbo].[Tbl_Blog]
           ([BlogTitle]
           ,[BlogAuthor]
           ,[BlogContent]
           ,[DeleteFlag])
     VALUES
           (@BlogTitle
           ,@BlogAuthor
           ,@BlogContent
           ,0)";

            SqlCommand cmd = new SqlCommand(query, connection);
            if(blog != null)
            {
                //cmd.Parameters.AddWithValue("@BlogId", blog.BlogId);
                cmd.Parameters.AddWithValue("@BlogTitle", blog.Title);
                cmd.Parameters.AddWithValue("@BlogAuthor", blog.Author);
                cmd.Parameters.AddWithValue("@BlogContent", blog.Content);
            }
           int result = cmd.ExecuteNonQuery();
            connection.Close();
            return Ok(result > 0 ? "success" : "fail");
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBlogs(int id, BlogViewModel blog)
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();
            string query = @"UPDATE [dbo].[Tbl_Blog]
   SET [BlogTitle] = @BlogTitle
      ,[BlogAuthor] = @BlogAuthor
      ,[BlogContent] = @BlogContent
      ,[DeleteFlag] = 0
 WHERE BlogId= @BlogId";

            SqlCommand cmd = new SqlCommand(query, connection);
            
                cmd.Parameters.AddWithValue("@BlogId", id);
            if(!string.IsNullOrEmpty(blog.Title))
            {
                cmd.Parameters.AddWithValue("@BlogTitle", blog.Title);
            }
            if (!string.IsNullOrEmpty(blog.Author))
            {
                cmd.Parameters.AddWithValue("@BlogAuthor", blog.Author);
            }
            if (!string.IsNullOrEmpty(blog.Content))
            {
                cmd.Parameters.AddWithValue("@BlogContent", blog.Content);
            }
            int result = cmd.ExecuteNonQuery();
            connection.Close();
            return Ok(result > 0 ? "update successful" : "update fail");
        }

        [HttpPatch("{id}")]
        public IActionResult PatchBlogs(int id,  BlogViewModel blog)
        {
            string condition = "";
            if (!string.IsNullOrEmpty(blog.Title))
            {
                condition += " [BlogTitle] = @BlogTitle, ";
            }

            if (!string.IsNullOrEmpty(blog.Author))
            {
                condition += " [BlogAuthor] = @BlogAuthor, ";
            }

            if (!string.IsNullOrEmpty(blog.Content))
            {
                condition += " [BlogContent] = @BlogContent, ";
            }

            if (condition.Length == 0)
            {
                return BadRequest("invalid parameter");
            }

            condition = condition.Substring(0, condition.Length - 2); 

            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();

            string query = $@"UPDATE [dbo].[Tbl_Blog]
   SET {condition}
 WHERE BlogId= @BlogId";

            SqlCommand cmd = new SqlCommand(query, connection);

            
                cmd.Parameters.AddWithValue("@BlogId", id);
            
            if (!string.IsNullOrEmpty(blog.Title))
            {
                cmd.Parameters.AddWithValue("@BlogTitle", blog.Title);
            }
            if (!string.IsNullOrEmpty(blog.Author))
            {
                cmd.Parameters.AddWithValue("@BlogAuthor", blog.Author);
            }
            if (!string.IsNullOrEmpty(blog.Content))
            {
                cmd.Parameters.AddWithValue("@BlogContent", blog.Content);
            }

            int result = cmd.ExecuteNonQuery();
            connection.Close();
            return Ok(result > 0 ? "update success" : "update fail");
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteBlogs(int id)
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();

            string query = @"DELETE FROM [dbo].[Tbl_Blog]
      WHERE BlogId = @BlogId";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@BlogId", id);
            cmd.Parameters.AddWithValue("@DeleteFlag",true);
            int result = cmd.ExecuteNonQuery();
            return Ok(result > 0 ? "deleted" : "fail to delete");
        }
    }
}
