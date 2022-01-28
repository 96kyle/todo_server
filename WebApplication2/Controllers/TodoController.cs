using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebApplication2.Databases;

namespace WebApplication2.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : BaseController
    {
        public TodoController(DatabaseContext databaseContext) : base(databaseContext)
        {
        }

        [HttpGet("")]
        public List<Todo> List()
        {

            var result = Self.TodoList.ToList();

            return result;
        }

        [HttpPost("")]
        public User Create([FromBody] TodoCreateModel model)
        {
            var result = DatabaseContext.Users.FirstOrDefault(p => p.Index.ToString() == UserIndex);

            result.TodoList.Add(new Todo()
            {
                UserId = result.Index,
                IsDone = false,
                IsTopFixed = false,
                Title = model.Title,
                Content = model.Content,
                DueDate = model.DueDate,
                CreatedTime = DateTime.Now,
                UpdatedTime = DateTime.Now,
            });

            DatabaseContext.SaveChanges();

            return result;
        }

        [HttpPut("{id}/toggleDone")]
        public Todo SwitchDone([FromRoute] int id)
        {
            var result = DatabaseContext.Todos.FirstOrDefault(p => p.Id == id);

            result.IsDone = !result.IsDone;

            result.TodoHistories.Add(new TodoHistory
            {
                TodoId = id,
                Title = "완료 상태 변경",
                Content = result.IsDone ? "상태를 완료로 변경하셨습니다." : "상태를 미완료로 변경하셨습니다.",
                CreatedTime = DateTime.Now,
            });

            result.UpdatedTime = DateTime.Now;

            if (result.IsDone == true)
            {
                result.CompletedTime = DateTime.Now;
            }
            else
            {
                result.CompletedTime = null;
            }

            DatabaseContext.SaveChanges();

            return result;
        }

        [HttpPut("{id}")]
        public Todo Update([FromRoute] int id, [FromBody] TodoCreateModel model)
        {
            var result = DatabaseContext.Todos.FirstOrDefault(p => p.Id == id);

            result.Title = model.Title;
            result.Content = model.Content;
            result.DueDate = model.DueDate;
            result.UpdatedTime = DateTime.Now;
            result.TodoHistories.Add(new TodoHistory
            {
                TodoId = id,
                Title = $"{model.Title}로 변경",
                Content = $"{model.Content}로 변경",
                CreatedTime = DateTime.Now,
            });

            DatabaseContext.SaveChanges();

            return result;
        }

        [HttpDelete("{id}")]
        public Todo Delete([FromRoute] int id)
        {
            var result = DatabaseContext.Todos.FirstOrDefault(p => p.Id == id);
            DatabaseContext.Todos.Remove(result);

            DatabaseContext.SaveChanges();
            return result;
        }
    }

    public class TodoCreateModel
    {
        public string Title { get; set; }

        public string Content { get; set; }

        public DateTime? DueDate { get; set; }
    }
}