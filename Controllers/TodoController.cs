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

        // POST api/values
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
                Name = value.Name
            };

            if (!Todos.TryAdd(TodoCount.Value, insertValue))
            {
                throw new Exception("Todo already exists");
            }
            
            TodoCount++;
            return insertValue;
        }

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<TodoItem>> Get()
        {
            if (Todos == null)
            {
                return null;
            }

            return Todos.Values;
        }

    }
}