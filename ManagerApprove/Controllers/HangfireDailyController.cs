using Hangfire;
using ManagerApprove.Models;
using ManagerApprove.Services;
using Microsoft.AspNetCore.Mvc;

namespace ManagerApprove.Controllers
{
    public class HangfireDailyController : Controller
    {
        public IActionResult Index()
        {
            RecurringJob.AddOrUpdate((HangfireService hs) => hs.Print(),Cron.Minutely);
            return Redirect("/Hangfire");
            //return Json();
        }
    }
}
