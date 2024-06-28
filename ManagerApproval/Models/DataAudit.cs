using System;
using System.Collections.Generic;

namespace EmployeeLogin.Models;

public partial class DataAudit
{
    public int DataId { get; set; }

    public string LastData { get; set; } = null!;

    public string CurrentData { get; set; } = null!;

    public string Table { get; set; } = null!;

    public string Column { get; set; } = null!;

    public string Status { get; set; } = null!;

    public DateTime Created { get; set; }
}
