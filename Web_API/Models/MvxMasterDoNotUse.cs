using System;
using System.Collections.Generic;

namespace Web_API.Models;

public partial class MvxMasterDoNotUse
{
    public string? MvxCode { get; set; }

    public string? ManufacturerName { get; set; }

    public string? Notes { get; set; }

    public string? Status { get; set; }

    public DateOnly? Lastupdateddate { get; set; }

    public string? ManufacturerId { get; set; }
}
