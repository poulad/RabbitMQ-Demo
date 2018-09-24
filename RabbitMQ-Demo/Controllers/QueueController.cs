using Demo.Models.Queue;
using Demo.Services;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Controllers
{
    public class QueueController : Controller
    {
        private readonly IQueueService _qService;

        public QueueController(IQueueService qService)
        {
            _qService = qService;
        }

        public IActionResult Index()
        {
            ViewData["queue"] = "Demo";
            ViewData["count"] = 3;

            return View();
        }

        public IActionResult Push()
        {
            var msg = new Visit(HttpContext.Connection.RemoteIpAddress.ToString());
            _qService.Publish(msg);

            ViewData["at"] = msg.VisitTime;

            return View();
        }

        public IActionResult Pop()
        {
            var msg = _qService.Dequeue<Visit>();

            ViewData["visitor"] = msg?.Visitor;
            ViewData["visit_time"] = msg?.VisitTime;

            return View();
        }
    }
}