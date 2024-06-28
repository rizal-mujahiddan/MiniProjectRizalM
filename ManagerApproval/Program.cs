using EmployeeLogin.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;


namespace EmployeeLogin
{
    internal class Program
    {

        private static void Main(string[] args)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Main Menu:");
                Console.WriteLine("1. Login");
                Console.WriteLine("2. Exit");

                var choice = Convert.ToInt32(Console.ReadLine());

                if (choice == 1)
                {
                    var configuration = new ConfigurationBuilder()
                                           .SetBasePath(Directory.GetCurrentDirectory())
                                           .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                                           .Build();

                    var optionsBuilder = new DbContextOptionsBuilder<MiniProjDbContext>();
                    optionsBuilder.UseSqlServer(configuration.GetConnectionString("MyDB"));

                    var dbcontext = new MiniProjDbContext(optionsBuilder.Options);

                    Console.WriteLine("Masukkan Employee ID");

                    if (!int.TryParse(Console.ReadLine(), out int empId))
                    {
                        Console.WriteLine("Employee ID Not Number");
                        continue;
                    }

                    var empObj = dbcontext.Employees.FirstOrDefault(x => x.EmployeeId == empId);

                    if (empObj != null)
                    {
                        Console.Write("Masukkan Hours Worked: ");

                        if (!decimal.TryParse(Console.ReadLine(), out decimal hours))
                        {
                            Console.WriteLine("Employee ID Is Not Number");
                            continue;
                        }

                        Console.Write("Masukkan Date (YYYY-MM-DD) : ");
                        if (!DateTime.TryParse(Console.ReadLine(), out DateTime date))
                        {
                            Console.WriteLine("Bukan Date");
                            continue;
                        }

                        dbcontext.Timesheets.Add(new Timesheet
                        {
                            Date = date,
                            EmployeeId = empId,
                            HoursWorked = hours,
                            Status = "Pending"
                        });
                        dbcontext.SaveChanges();


                    }
                    else
                    {
                        Console.WriteLine("Employee ID is not found");
                        Console.ReadKey();
                        continue;
                    }
                }
                else if (choice == 2)
                {
                    break;
                }
                else
                {
                    continue;
                }
            }
        }
    }
}
