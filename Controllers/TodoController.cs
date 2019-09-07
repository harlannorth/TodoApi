using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TodoApi.Models;

namespace TodoApi.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class TodosController : ControllerBase
    {
        private static long? TodoCount;
        private static Dictionary<long, TodoItem> Todos;

        public TodosController()
        {
            if (!TodoCount.HasValue)
            {
                TodoCount = 0;
            }

            if (Todos == null)
            {
                Todos = new Dictionary<long, TodoItem>();
            }

        }

        // POST api/todos
        [HttpPost]
        public TodoItem Post([FromBody] TodoItem value)
        {
            if (Todos == null)
            {
                throw new Exception("Todos is null");
            }

            var insertValue = new TodoItem {
                Id = TodoCount.Value,
                IsComplete = value.IsComplete,
                Title = value.Title
            };

            if (!Todos.TryAdd(TodoCount.Value, insertValue))
            {
                throw new Exception("Todo already exists");
            }
            
            TodoCount++;
            return insertValue;
        }

        // GET api/todos
        [HttpGet]
        public ActionResult<IEnumerable<TodoItem>> Get()
        {
            if (Todos == null)
            {
                return null;
            }

            return Todos.Values;
        }

        // GET api/todos/5
        [HttpGet("{id}")]
        public ActionResult<TodoItem> Get(long id)
        {
            if (Todos == null)
            {
                return null;
            }
    
            if (Todos.TryGetValue(id, out var todo))
            {
                return todo;
            }                

            return null;
        }

        // PUT api/todos/5
        [HttpPut("{id}")]
        public TodoItem Put(long id, [FromBody] TodoItem value)
        {
            if (Todos == null)
            {
                throw new Exception("Todos is null");
            }

            if (id != value.Id)
            {
                throw new Exception("Id and Todo Id mismatch");
            }

            if (!Todos.ContainsKey(id))
            {
                throw new Exception("Todo doesn't exist");
            }

            Todos.Remove(id);
            Todos.Add(id, value);
            return Todos[id];    

        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(long id)
        {
            if (Todos == null)
            {
                throw new Exception("Todos is null");
            }

            if (!Todos.ContainsKey(id))
            {
                throw new Exception("Todo doesn't exist");
            }
            Todos.Remove(id);
        }

    }
}