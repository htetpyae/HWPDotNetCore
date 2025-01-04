using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HWPDotNetCore.ConsoleApp
{
    public class AdoDotNetExample
    {
        private readonly string _connectionString = "Data Source=.;Initial Catalog=DotNetDB;User ID=sa;Password=sa@123;";
        public void Read()
        {
           
            Console.WriteLine("Connection string: " + _connectionString);
            SqlConnection connection = new SqlConnection(_connectionString);

            Console.WriteLine("connection opening: ");
            connection.Open();
            Console.WriteLine("connection opened");

            string query = @"SELECT [BlogId]
      ,[BlogTitle]
      ,[BlogAuthor]
      ,[BlogContent]
      ,[DeleteFlag]
  FROM [dbo].[Tbl_Blog] where DeleteFlag=0";

            SqlCommand cmd = new SqlCommand(cmdText: query, connection);
            //SqlDataAdapter adapter = new SqlDataAdapter(selectCommand: cmd);
            //DataTable dt = new DataTable();
            //adapter.Fill(dataTable: dt);

            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Console.WriteLine(value: reader[name: "BlogId"]);
                Console.WriteLine(value: reader[name: "BlogId"]);
                Console.WriteLine(value: reader[name: "BlogId"]);
                Console.WriteLine(value: reader[name: "BlogContent"]);
                //Console.WriteLine(value: dr[columnName: "DeleteFlag"]);
            }
            Console.WriteLine("connection closing..");
            connection.Close();
            Console.WriteLine("connection closed");

            //foreach (DataRow dr in dt.Rows)
            //{
            //  Console.WriteLine(value: dr[columnName: "BlogId"]);
            //Console.WriteLine(value: dr[columnName: "BlogTitle"]);
            //Console.WriteLine(value: dr[columnName: "BlogAuthor"]);
            //Console.WriteLine(value: dr[columnName: "BlogContent"]);
            //Console.WriteLine(value: dr[columnName: "DeleteFlag"]);
            //}
        }

        public void Create()
        {
            Console.WriteLine("Blog Title");
            string title = Console.ReadLine();

            Console.WriteLine("Blog Author: ");
            string author = Console.ReadLine();

            Console.WriteLine("Blog Content");
            string content = Console.ReadLine();

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

            cmd.Parameters.AddWithValue("@BlogTitle", title);
            cmd.Parameters.AddWithValue("@BlogAuthor", author);
            cmd.Parameters.AddWithValue("@BlogContent", content);

            int result = cmd.ExecuteNonQuery();
            // SqlDataAdapter adapter = new SqlDataAdapter(cmd2);
            // DataTable dt = new DataTable();
            // adapter.Fill(dt);
            connection.Close();

            Console.WriteLine(result == 1 ? "success" : "fail");
        }

        public void Edit()
        {
            Console.Write("BlogId: ");
            string id= Console.ReadLine();

            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();
            string query = @"SELECT [BlogId]
      ,[BlogTitle]
      ,[BlogAuthor]
      ,[BlogContent]
      ,[DeleteFlag]
  FROM [dbo].[Tbl_Blog] where BlogId = @BlogId";

            SqlCommand cmd = new SqlCommand(@query, connection);
            cmd.Parameters.AddWithValue("@BlogId", id);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);  
            DataTable dt = new DataTable(); 
            adapter.Fill(dt);

            connection.Close();

            if (dt.Rows.Count == 0)
            {
                Console.WriteLine("no data found");
                return;
            }

            DataRow dr = dt.Rows[0];
            Console.WriteLine(dr["BlogId"]);
            Console.WriteLine(dr["BlogTitle"]);
            Console.WriteLine(dr["BlogAuthor"]);
            Console.WriteLine(dr["BlogContent"]);
            
        }

        public void Update()
        {
            Console.WriteLine("Blog Id");
            string id = Console.ReadLine();

            Console.WriteLine("Blog Title");
            string title = Console.ReadLine();

            Console.WriteLine("Blog Author: ");
            string author = Console.ReadLine();

            Console.WriteLine("Blog Content");
            string content = Console.ReadLine();

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
            cmd.Parameters.AddWithValue("@BlogTitle", title);
            cmd.Parameters.AddWithValue("@BlogAuthor", author);
            cmd.Parameters.AddWithValue("@BlogContent", content);

            int result = cmd.ExecuteNonQuery();
            
            connection.Close();

            Console.WriteLine(result == 1 ? "success" : "fail");
        }

       
    }
}
