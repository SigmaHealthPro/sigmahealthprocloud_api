using System;
using System.Collections.Generic;

namespace Data.Models;

public partial class Lot
{
    public Guid Id { get; set; }

    public int LotId { get; set; }

    public string? LotNumber { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public string? CreatedBy { get; set; }

    public string? UpdatedBy { get; set; }

    public bool? Isdelete { get; set; }
}
