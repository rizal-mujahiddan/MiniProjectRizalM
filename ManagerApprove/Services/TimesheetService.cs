using ManagerApprove.Contracts;
using ManagerApprove.DTO;
using ManagerApprove.Helpers;
using ManagerApprove.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ManagerApprove.Services
{
    public class TimesheetService : ITimesheet
    {
        private readonly MiniProjDbContext _dbcontext;
        public TimesheetService(MiniProjDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public Timesheet Add(Timesheet entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Timesheet> GetAll()
        {
            throw new NotImplementedException();
        }

        public Timesheet GetById(int id)
        {
            throw new NotImplementedException();
        }

        public TimesheetEmployeeDTO GetByIdDTO(int id)
        {
            try
            {
                var timeEmpJoin = _dbcontext.Timesheets
                                  .Include(tmsh => tmsh.Employee)
                                  .Where(tmsh=>tmsh.TimesheetId == id).FirstOrDefault();
                
                var timeEmpDto = new TimesheetEmployeeDTO {
                        TimesheetId = timeEmpJoin.TimesheetId,
                        EmployeeId = timeEmpJoin.EmployeeId,
                        Name = timeEmpJoin.Employee.Name,
                        Date = timeEmpJoin.Date,
                        HoursWorked = timeEmpJoin.HoursWorked,
                        Status = timeEmpJoin.Status
                };
                return timeEmpDto;
            }
            catch (Exception ex)
            {

                throw new ArgumentException(ex.Message);
            }
        }

        public IAsyncEnumerable<TimesheetEmployeeDTO> GetTimeEmpDTOAll()
        {
            try
            {
                var timeEmpDtoList = new List<TimesheetEmployeeDTO>();

                var timeEmpJoin = _dbcontext.Timesheets.Include(tmsh => tmsh.Employee).ToList();
                foreach(var timeEmp in timeEmpJoin)
                {
                    timeEmpDtoList.Add(new TimesheetEmployeeDTO{
                        TimesheetId=timeEmp.TimesheetId,
                        EmployeeId=timeEmp.EmployeeId,
                        Name=timeEmp.Employee.Name,
                        Date=timeEmp.Date,
                        HoursWorked=timeEmp.HoursWorked,
                        Status=timeEmp.Status,
                    });
                }
                var asyncTimeEmpDtoList = timeEmpDtoList.ToAsyncEnumerable();
                return asyncTimeEmpDtoList;
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }    
        }

        public Timesheet Update(Timesheet entity)
        {
            _dbcontext.Timesheets.Update(entity);
            _dbcontext.SaveChanges();
            //using (var transaction = _dbcontext.Database.BeginTransaction())
            //{
            //    try
            //    {
            //        // Disable the audit trigger
            //        _dbcontext.Database.ExecuteSqlRaw("DISABLE TRIGGER trg_Timesheets_Update ON Timesheets");

            //        // Perform the update
            //        _dbcontext.Timesheets.Update(entity);
            //        _dbcontext.SaveChanges();

            //        // Enable the audit trigger
            //        _dbcontext.Database.ExecuteSqlRaw("ENABLE TRIGGER trg_Timesheets_Update ON Timesheets");

            //        // Commit the transaction
            //        transaction.Commit();
            //    }
            //    catch (Exception ex)
            //    {
            //        // Rollback the transaction if an error occurs
            //        transaction.Rollback();
            //        // Log the exception or handle it as needed
            //        throw new ArgumentException(ex.Message);
            //    }
            //}

            return entity;
        }
    }
}
