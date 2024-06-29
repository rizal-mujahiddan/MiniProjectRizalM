using Hangfire;
using ManagerApprove.Models;
using ManagerApprove.Services;
using Microsoft.AspNetCore.Mvc;

namespace ManagerApprove.Controllers
{
    public class HangfireDailyController : Controller
    {
        private readonly MiniProjDbContext _context;
        private readonly HangfireService _hangfireService;
        public HangfireDailyController(MiniProjDbContext context, HangfireService hangfireService)
        {
            _context = context;
            _hangfireService = hangfireService;
        }
        public IActionResult Index()
        {
            RecurringJob.AddOrUpdate(() => _hangfireService.Print());

            return Redirect("/Hangfire");
            //return Json();
        }
    }
}
