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

        //public Timesheet Add(Timesheet entity)
        //{
        //    throw new NotImplementedException();
        //}

        //public void Delete(int id)
        //{
        //    throw new NotImplementedException();
        //}

        //public IEnumerable<Timesheet> GetAll()
        //{
        //    throw new NotImplementedException();
        //}

        //public Timesheet GetById(int id)
        //{
        //    throw new NotImplementedException();
        //}

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
                //throw new ArgumentException(ex.InnerException.Message);
            }    
        }

        public Timesheet Update(Timesheet entity)
        {
            _dbcontext.Timesheets.Update(entity);
            _dbcontext.SaveChanges();
            return entity;
        }
    }
}
