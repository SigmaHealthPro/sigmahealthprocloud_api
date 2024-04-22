using System;
using System.Collections.Generic;

namespace Web_API.Models;

public partial class CvxVi
{
    public Guid Id { get; set; }

    public int CvxVisId { get; set; }

    public Guid? CvxCodeId { get; set; }

    public string? VisDocumentName { get; set; }

    public string? VisFullyEncodedText { get; set; }

    public DateOnly? VisEditionDate { get; set; }

    public string? VisEditionStatus { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public string? CreatedBy { get; set; }

    public string? UpdatedBy { get; set; }

    public bool? Isdelete { get; set; }
}
