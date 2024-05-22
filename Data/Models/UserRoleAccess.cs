using System;
using System.Collections.Generic;

namespace Data.Models;

public partial class UserRoleAccess
{
    public Guid UserRoleAccessId { get; set; }

    public Guid LovMasterRoleId { get; set; }

    public Guid LinkId { get; set; }

    public string LinkType { get; set; } = null!;

    public string? AccessLevel { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public string? CreatedBy { get; set; }

    public string? UpdatedBy { get; set; }

    public virtual LovMaster LovMasterRole { get; set; } = null!;
}
