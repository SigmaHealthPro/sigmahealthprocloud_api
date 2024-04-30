using System;
using System.Collections.Generic;

namespace Web_API.Models;

public partial class VaccinepriceBkp
{
    public Guid? Id { get; set; }

    public int? PriceId { get; set; }

    public string? Brandname { get; set; }

    public Guid? NdcId { get; set; }

    public string? Packaging { get; set; }

    public string? CostPerDose { get; set; }

    public string? PrivateSectorCostPerDose { get; set; }

    public string? ContractEndDate { get; set; }

    public string? Manufacturer { get; set; }

    public string? ContractNumber { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public string? CreatedBy { get; set; }

    public string? UpdatedBy { get; set; }

    public bool? Isdelete { get; set; }

    public Guid? CvxId { get; set; }
}
