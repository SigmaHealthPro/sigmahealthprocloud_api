using System;
using System.Collections.Generic;

namespace Data.Models;

public partial class Onboardingdoc
{
    public Guid Id { get; set; }

    public int DocId { get; set; }

    public string? Filepath { get; set; }

    public string? Filename { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public bool? Isdelete { get; set; }

    public string? CreatedBy { get; set; }

    public string? UpdatedBy { get; set; }
}
