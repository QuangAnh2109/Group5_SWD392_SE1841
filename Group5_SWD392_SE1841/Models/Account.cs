using System;
using System.Collections.Generic;

namespace Group5_SWD392_SE1841.Models;

public partial class Account
{
    public int AccountId { get; set; }

    public string UserName { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public int AccountRoleId { get; set; }

    public int EmployeeId { get; set; }

    public int AccountStatusId { get; set; }

    public byte[] RecordNo { get; set; } = null!;

    public int CreatedId { get; set; }

    public DateTime CreatedTime { get; set; }

    public int UpdatesId { get; set; }

    public DateTime UpdateTime { get; set; }

    public bool DeleteFlg { get; set; }

    public virtual Employee Employee { get; set; } = null!;
}
