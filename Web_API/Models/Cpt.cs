using System;
using System.Collections.Generic;

namespace Web_API.Models;

public partial class Cpt
{
    public Guid Id { get; set; }

    public int CptId { get; set; }

    public string? CptCode { get; set; }

    public string? CptDescription { get; set; }

    public Guid? CvxCodeId { get; set; }

    public string? Comment { get; set; }

    public string? CptCodeId { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public string? CreatedBy { get; set; }

    public string? UpdatedBy { get; set; }

    public bool? Isdelete { get; set; }

    public DateOnly? ReleaseDate { get; set; }
}
