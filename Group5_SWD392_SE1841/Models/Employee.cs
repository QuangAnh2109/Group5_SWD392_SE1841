using System;
using System.Collections.Generic;

namespace Group5_SWD392_SE1841.Models;

public partial class Employee
{
    public int EmployeeId { get; set; }

    public int? ManagerId { get; set; }

    public int? DepartmentId { get; set; }

    public string EmployeeName { get; set; } = null!;

    public string EmployeeEmail { get; set; } = null!;

    public int EmployeeStatusId { get; set; }

    public byte[] RecordNo { get; set; } = null!;

    public int CreatedId { get; set; }

    public DateTime CreatedTime { get; set; }

    public int UpdatesId { get; set; }

    public DateTime UpdateTime { get; set; }

    public bool DeleteFlg { get; set; }

    public virtual ICollection<Account> Accounts { get; set; } = new List<Account>();

    public virtual Department? Department { get; set; }

    public virtual ICollection<Employee> InverseManager { get; set; } = new List<Employee>();

    public virtual Employee? Manager { get; set; }

    public virtual ICollection<ProjectEmployee> ProjectEmployees { get; set; } = new List<ProjectEmployee>();

    public virtual ICollection<Timesheet> Timesheets { get; set; } = new List<Timesheet>();
}
