using System;
using System.Collections.Generic;

namespace Group5_SWD392_SE1841.Models;

public partial class Timesheet
{
    public int TimeSheetId { get; set; }

    public int EmployeeId { get; set; }

    public int WorkStatusId { get; set; }

    public DateTime StartTime { get; set; }

    public DateTime? EndTime { get; set; }

    public int TaskId { get; set; }

    public string? Notes { get; set; }

    public byte[] RecordNo { get; set; } = null!;

    public int CreatedId { get; set; }

    public DateTime CreatedTime { get; set; }

    public int UpdatesId { get; set; }

    public DateTime UpdateTime { get; set; }

    public bool DeleteFlg { get; set; }

    public virtual Employee Employee { get; set; } = null!;

    public virtual Task Task { get; set; } = null!;
}
