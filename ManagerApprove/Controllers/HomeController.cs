using ManagerApprove.Contracts;
using ManagerApprove.DTO;
using ManagerApprove.Helpers;
using ManagerApprove.Models;
using ManagerApprove.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace ManagerApprove.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        //private readonly MiniProjDbContext _miniProjDbContext;
        private readonly ITimesheet _itimesheet;

        public HomeController(ILogger<HomeController> logger,ITimesheet itimesheet)
        {
            _logger = logger;
            _itimesheet= itimesheet;
        }

        public async Task<IActionResult> Index(int pageIndex = 1, int pageSize = 25)
        {
            var allItems = _itimesheet.GetTimeEmpDTOAll();
            var totalItems = await allItems.CountAsync();

            var Timesh = await allItems.SkipAndTakeToListAsync((pageIndex - 1) * pageSize, pageSize);

            //var Timesh = await _miniProjDbContext.Timesheets
            //    .Skip((pageIndex - 1) * pageSize)
            //    .Take(pageSize)
            //    .ToListAsync();


            var viewModel = new PaginationViewModel<TimesheetEmployeeDTO>
            {
                Items = Timesh,
                PageIndex = pageIndex,
                TotalPages = (int)Math.Ceiling(totalItems / (double)pageSize),
                PageSize = pageSize,
                TotalItems = totalItems
            };

            return View(viewModel);
        }

        [HttpGet("Details/{id}")]
        public IActionResult Details(int id)
        {
            var model = _itimesheet.GetByIdDTO(id);
            return View(model);
        }

        [HttpPost("/Details/Update")]
        public IActionResult Update(TimesheetEmployeeDTO model)
        {
            return View(model);
        }

        [HttpPost("Updating")]
        public IActionResult Updating(TimesheetEmployeeDTO model) {
            var modelTime = new Timesheet
            {
                TimesheetId = model.TimesheetId,
                EmployeeId = model.EmployeeId,
                Date = model.Date,
                HoursWorked = model.HoursWorked,
                Status = model.Status,
            };
            _itimesheet.Update(modelTime);
            return RedirectToAction("Index","Home");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
