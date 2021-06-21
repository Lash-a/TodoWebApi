using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using TodoWebApi.Models;

namespace TodoWebApi.Repositories
{
    public class TodoRepository
    {
        //es cvladi gansazgvravs romel monacemta bazas uertdeba es klasi
        private SqlConnection conn;

        public TodoRepository()
        {
            //გადმოვიდა TodoController კლასიდან სადაც შეიქმნა TodoRepository-ს ახალი ობიექტი და გაუშვა ეს კონსტრუქტორი
            //კონსტრუქტორში დამყარდა კავშირი მონაცემთა ბაზასთან. ეს კავშირი განისაზღვრა web.config ფაილის კონფიგურაციიდან და მიენიჭა conn ცვლადს
            //რომელსაც შემდეგ გამოვიყენებთ კავშირის დასახურად.
            string connStr = ConfigurationManager.ConnectionStrings["TodoConnStr"].ConnectionString;
            conn = new SqlConnection(connStr);
        }

        public void SaveTodo(Todo todo)
        {
            // კონსტრუქტორში შეიძლება მივუთითოთ cmd.CommandText და ასევე cmd.Connection ცვლადების მნიშვნელობები
            //თუმცა უმჯობესია დაკავშირება დავიწყოთ გვიან
            var cmd = new SqlCommand();
            // cmd.CommandText = "dbo.SaveTodo";
            //განსაზღვრავს პროცედურას რომლის გაშვებაც გვინდა სქლ-ში
            cmd.CommandText = "dbo.Todo_SaveTodo";
            // cmd.Parameters.Add("@Name", SqlDbType.NVarChar).Value = todo.Description;
            //განსაზღვრავს ცვლადებს ანიჭებს datatypes და შემდეგ უტოლებს c#-ის ცვლადს
            cmd.Parameters.Add("@Description", SqlDbType.NVarChar).Value = todo.Description;
            // cmd.Parameters.Add("@Status", SqlDbType.Int).Value = todo.Done;
            cmd.Parameters.Add("@Done", SqlDbType.Int).Value = todo.Done;
            //განსაზღვრავს ბრძანების ტიპს
            cmd.CommandType = CommandType.StoredProcedure;
            //მიუთითებს თუ რომელ ბაზასთან გვსურს დაკავშირება
            cmd.Connection = conn;
            //ამყარებს ამ ბაზასთან კავშირს
            cmd.Connection.Open(); // conn.Open();
            //უშვებს პროცედურას 
            cmd.ExecuteNonQuery();
            //წყვეტს ბაზასთან კავშირს
            conn.Close();
        }


        public void ChangeDoneOrNot(Todo todo)
        {
            var cmd = new SqlCommand();
            cmd.CommandText = "dbo.Todo_ChangeDoneOrNot";
            cmd.Parameters.Add("@Done", SqlDbType.Bit).Value = todo.Done;
            cmd.Parameters.Add("@ID", SqlDbType.Int).Value = todo.ID;

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = conn;

            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }



        public void DeleteTodo(int id)
        {
            var cmd = new SqlCommand("dbo.Todo_DeleteTodo", conn);
            cmd.Parameters.Add("@ID", SqlDbType.Int).Value = id;

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            conn.Close();


            //var cmd = new SqlCommand();
            //cmd.CommandText = "dbo.Todo_DeleteTodo";

            //cmd.Parameters.Add("@ID", SqlDbType.Int).Value = todo.ID;

            //cmd.CommandType = CommandType.StoredProcedure;
            //cmd.Connection = conn;

            //cmd.Connection.Open(); // conn.Open();
            //cmd.ExecuteNonQuery();
            //conn.Close();

        }


        public void UpdateTodo(Todo todo)
        {
            var cmd = new SqlCommand("dbo.Todo_UpdateTodo", conn);
            cmd.Parameters.Add("@ID", SqlDbType.Int).Value = todo.ID;
            cmd.Parameters.Add("@Description", SqlDbType.NVarChar).Value = todo.Description;
            cmd.Parameters.Add("@Done", SqlDbType.Bit).Value = todo.Done;

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            conn.Close();

        }




        public List<Todo> GetTodoList()
        {
            var todoList = new List<Todo>();

            var cmd = new SqlCommand();
            // cmd.CommandText = "dbo.GetTodoes";
            cmd.CommandText = "dbo.Todo_GetAllTodo";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = conn;

            cmd.Connection.Open();

            var reader = cmd.ExecuteReader();
            
            while (reader.Read())
            {
                var todo = new Todo();
                todo.ID = int.Parse(reader["ID"].ToString());
                // todo.Description = reader["Name"].ToString();
                todo.Description = reader["Description"].ToString();
                // todo.Done = int.Parse(reader["Status"].ToString()) > 0;
                todo.Done = bool.Parse(reader["Done"].ToString());
                todoList.Add(todo);
            }
            conn.Close();

            return todoList;
        }


    }
}