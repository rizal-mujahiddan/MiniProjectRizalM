using ManagerApprove.Models;

namespace ManagerApprove.Services
{
    public class HangfireService
    {
        private readonly MiniProjDbContext _context;
        public HangfireService(MiniProjDbContext context)
        {
            _context = context;
        }
        public async Task Print()
        {
            var empl = _context.Employees.ToList();
            foreach (var employee in empl) { 
                Console.WriteLine(employee.Name);
            }
        }
    }
}
