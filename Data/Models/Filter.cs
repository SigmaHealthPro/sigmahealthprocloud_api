using System;
using System.Collections.Generic;

namespace Data.Models;

public partial class Filter
{
    public Guid FilterId { get; set; }

    public string? PageName { get; set; }

    public string? FilterType { get; set; }

    public string? FilterCondition { get; set; }

    public string? LogicalOperator { get; set; }
}
