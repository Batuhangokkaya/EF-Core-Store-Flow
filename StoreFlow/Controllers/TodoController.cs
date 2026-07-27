using Microsoft.AspNetCore.Mvc;
using StoreFlow.Context;
using StoreFlow.Entities;

namespace StoreFlow.Controllers
{
    public class TodoController : Controller
    {
        private readonly StoreContext _context;

        public TodoController(StoreContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> CreateTodo()
        {
            var todos = new List<Todo>
            {
                new Todo{ Description = "Mail gönder", Status = true, Priority = "Birincil" },
                new Todo{ Description = "Rapor hazırla", Status = true, Priority = "İkincil" },
                new Todo{ Description = "Toplantıya katıl", Status = false, Priority = "Birincil" }
            };

            await _context.Todos.AddRangeAsync(todos);
            _context.SaveChanges();

            return View();
        }

        public IActionResult TodoAggregatePriority()
        {
            var priorityFirstlyTodo = _context.Todos
                .Where(x => x.Priority == "Birincil")
                .Select(x => x.Description)
                .ToList();

            var result = priorityFirstlyTodo.Aggregate(string.Empty, (acc, desc) => acc + $"<li>{desc}</li>");
            ViewBag.result = result;

            return View();
        }

        public IActionResult IncompleteTaskAppend()
        {
            var values = _context.Todos
                .Where(x => !x.Status)
                .Select(x => x.Description)
                .ToList()
                .Append("Gün sonunda tüm görevleri kontrol etmeyi unutmayın!")
                .ToList();

            return View(values);
        }

        public IActionResult IncompleteTaskPrepend()
        {
            var values = _context.Todos
                .Where(x => !x.Status)
                .Select(x => x.Description)
                .ToList()
                .Prepend("Gün başında tüm görevleri kontrol etmeyi unutmayın!")
                .ToList();

            return View(values);
        }

        public IActionResult TodoChunk()
        {
            var values = _context.Todos
                .Where(x => !x.Status)
                .ToList()
                .Chunk(2)
                .ToList();

            return View(values);
        }

        public IActionResult TodoConcat()
        {
            var values = _context.Todos
                .Where(x => x.Priority == "Birincil")
                .Concat(_context.Todos.Where(x => x.Priority == "İkincil"))
                .ToList();

            return View(values);
        }


        public IActionResult TodoUnion()
        {
            var values1 = _context.Todos
                .Where(x => x.Priority == "Birincil")
                .ToList();
                

            var values2 = _context.Todos
                .Where(x => x.Priority == "İkincil")
                .ToList();

            var result = values1.UnionBy(values2, x => x.Description).ToList();

            return View(result);
        }
    }
}