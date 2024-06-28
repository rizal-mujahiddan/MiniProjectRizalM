using System;
using System.Collections.Generic;

namespace EmployeeLogin.Models;

public partial class Timesheet
{
    public int TimesheetId { get; set; }

    public int EmployeeId { get; set; }

    public DateTime Date { get; set; }

    public decimal HoursWorked { get; set; }

    public string Status { get; set; } = null!;

    public virtual Employee Employee { get; set; } = null!;
}
