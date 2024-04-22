using System;
using System.Collections.Generic;

namespace Web_API.Models;

public partial class TermsCondition
{
    public Guid Id { get; set; }

    public int TermsConditionsId { get; set; }

    public string? TermsConditions { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public string? CreatedBy { get; set; }

    public string? UpdatedBy { get; set; }

    public bool? Isdelete { get; set; }
}
