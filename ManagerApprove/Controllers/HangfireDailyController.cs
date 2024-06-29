using Hangfire;
using ManagerApprove.Models;
using ManagerApprove.Services;
using Microsoft.AspNetCore.Mvc;

namespace ManagerApprove.Controllers
{
    public class HangfireDailyController : Controller
    {
        private readonly MiniProjDbContext _context;
        public HangfireDailyController(MiniProjDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var dataEmployeeAll = _context.Employees.ToList();

            foreach (var employee in dataEmployeeAll)
            {
                RecurringJob.AddOrUpdate(() => Console.WriteLine(employee.Name), Cron.Minutely);
            }

            return Redirect("/Hangfire");
            return Json(dataEmployeeAll);
            //return Json();
        }
    }
}
