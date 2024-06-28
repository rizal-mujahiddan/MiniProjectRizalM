namespace ManagerApprove.DTO
{
    public class TimesheetEmployeeDTO
    {
        public int TimesheetId { get; set; }
        public int EmployeeId { get; set; }
        public string Name { get; set; } = null!;
        public DateTime Date { get; set; }
        public decimal HoursWorked { get; set; }
        public string Status { get; set; } = null!;
    }
}
