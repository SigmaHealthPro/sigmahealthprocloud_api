using System;
using System.Collections.Generic;

namespace Data.Models;

public partial class Subfeature
{
    public Guid SubfeatureId { get; set; }

    public string? SubfeatureName { get; set; }

    public Guid? Featureid { get; set; }

    public string? SubfeatureLink { get; set; }

    public string? IconCode { get; set; }

    public int? ViewOrder { get; set; }

    public virtual Feature? Feature { get; set; }
}
