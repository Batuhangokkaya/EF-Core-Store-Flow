using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StoreFlow.Context;
using StoreFlow.Entities;

namespace StoreFlow.Controllers
{
    public class MessageController : Controller
    {
        private readonly StoreContext _context;

        public MessageController(StoreContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var values = _context.Messages
                .AsNoTracking()
                .ToList();
            return View(values);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Message message)
        {
            _context.Messages.Add(message);
            _context.SaveChanges();
            return RedirectToAction("Index", "Message");
        }

        public IActionResult Delete(int id)
        {
            var value = _context.Messages.Find(id);
            _context.Messages.Remove(value);
            _context.SaveChanges();
            return RedirectToAction("Index", "Message");
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            var value = _context.Messages.Find(id);
            return View(value);
        }
        [HttpPost]
        public IActionResult Update(Message message)
        {
            _context.Messages.Update(message);
            _context.SaveChanges();
            return RedirectToAction("Index", "message");
        }

        public IActionResult MessageDetail(int id)
        {
            var message = _context.Messages.FirstOrDefault(x => x.MessageID == id);

            if (message == null)
            {
                return NotFound();
            }

            if (!message.IsRead)
            {
                message.IsRead = true;
                _context.SaveChanges();
            }

            return View(message);
        }
    }
}