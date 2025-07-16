using System;
using System.Collections.Generic;

namespace Group5_SWD392_SE1841.Models;

public partial class Department
{
    public int DepartmentId { get; set; }

    public string DepartmentName { get; set; } = null!;

    public int DepartmentStatus { get; set; }

    public byte[] RecordNo { get; set; } = null!;

    public int CreatedId { get; set; }

    public DateTime CreatedTime { get; set; }

    public int UpdatesId { get; set; }

    public DateTime UpdateTime { get; set; }

    public bool DeleteFlg { get; set; }

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
}
