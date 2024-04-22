using System;
using System.Collections.Generic;

namespace Web_API.Models;

public partial class CvxVaccineGroup
{
    public Guid Id { get; set; }

    public int CvxVaccineId { get; set; }

    public Guid? CvxCodeId { get; set; }

    public string? VaccineStatus { get; set; }

    public string? VaccineGroupName { get; set; }

    public string? CvxForVaccineGroup { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public string? CreatedBy { get; set; }

    public string? UpdatedBy { get; set; }

    public bool? Isdelete { get; set; }
}
