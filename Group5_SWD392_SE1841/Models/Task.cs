using System;
using System.Collections.Generic;

namespace Group5_SWD392_SE1841.Models;

public partial class Task
{
    public int TaskId { get; set; }

    public int EmployeeId { get; set; }

    public int ProjectId { get; set; }

    public string TaskName { get; set; } = null!;

    public DateTime? EndTime { get; set; }

    public int TaskStatusId { get; set; }

    public byte[] RecordNo { get; set; } = null!;

    public int CreatedId { get; set; }

    public DateTime CreatedTime { get; set; }

    public int UpdatesId { get; set; }

    public DateTime UpdateTime { get; set; }

    public bool DeleteFlg { get; set; }

    public virtual Employee Employee { get; set; } = null!;

    public virtual Project Project { get; set; } = null!;
}
