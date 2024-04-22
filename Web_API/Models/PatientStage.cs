using System;
using System.Collections.Generic;

namespace Web_API.Models;

public partial class PatientStage
{
    public string? PatientId { get; set; }

    public string? PatientName { get; set; }

    public Guid Id { get; set; }

    public DateTime? CreatedDate { get; set; }

    public string? CreatedBy { get; set; }
}
