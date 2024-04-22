using System;
using System.Collections.Generic;

namespace Web_API.Models;

public partial class VaccinePriceDoNotUse
{
    public string? Vaccine { get; set; }

    public string? Brandname { get; set; }

    public string? Ndc { get; set; }

    public string? Packaging { get; set; }

    public string? CdccostDose { get; set; }

    public string? PrivateSectorCostDose { get; set; }

    public string? ContractEndDate { get; set; }

    public string? Manufacturer { get; set; }

    public string? ContractNumber { get; set; }
}
