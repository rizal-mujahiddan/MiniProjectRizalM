using ManagerApprove.DTO;
using ManagerApprove.Models;

namespace ManagerApprove.Contracts
{
    public interface ITimesheet : ICrud<Timesheet>
    {
        IAsyncEnumerable<TimesheetEmployeeDTO> GetTimeEmpDTOAll();
        TimesheetEmployeeDTO GetByIdDTO(int id);
    }
}
