using ManagerApprove.Models;

namespace ManagerApprove.Services
{
    public class HangfireService
    {
        public static void Print(Employee e)
        {
            Console.WriteLine(e.Name);
        }
    }
}
