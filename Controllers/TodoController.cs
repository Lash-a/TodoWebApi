using System.Web.Http;
using TodoWebApi.Models;
using TodoWebApi.Repositories;

namespace TodoWebApi.Controllers
{
    public class TodoController : ApiController
    {
        [HttpPost]
        public IHttpActionResult SaveTodo(Todo todo)
        {
            //იქმნება ახალი TodoRepository ობიექტი რომელიც შემდეგ გადადის ამ კლასის კონსტრუქტორში
            var todoRepository = new TodoRepository();
            // გამოვიძახეთ todoRepository კლასის saveTodo მეთოდი რომელსაც გადავეცით todo კლასი 
            // todo კლასში შედის ID Description და Done ცვლადები
            todoRepository.SaveTodo(todo);

            //თუ მოაღწია აქამდე ესეიგი ერრორი არ ამოვარდნილა ფუნქცია გაეშვა წარმატებით და დააბრუნოს სტატუსი 200
            return Ok();
        }

        [HttpPost]
        public IHttpActionResult GetTodoList()
        {
            var todoRepository = new TodoRepository();
            var todoList = todoRepository.GetTodoList();
            return Ok(todoList);
        }

        [HttpPost]
        public IHttpActionResult ChangeDoneOrNot(Todo todo)
        {
            var todoRepository = new TodoRepository();
            todoRepository.ChangeDoneOrNot(todo);

            return Ok();
        }


        [HttpPost]
        public IHttpActionResult DeleteTodo([FromBody]int id)
        {
            var todoRepository = new TodoRepository();
            todoRepository.DeleteTodo(id);

            return Ok();
        }


        [HttpPost]
        public IHttpActionResult UpdateTodo(Todo todo)
        {
            var todoRepository = new TodoRepository();
            todoRepository.UpdateTodo(todo);

            return Ok();
        }
    }
}
