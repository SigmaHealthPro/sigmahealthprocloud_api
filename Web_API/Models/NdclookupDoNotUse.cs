using System;
using System.Collections.Generic;

namespace Web_API.Models;

public partial class NdclookupDoNotUse
{
    public string? SaleNdc11 { get; set; }

    public string? SaleProprietaryName { get; set; }

    public string? SaleLabeler { get; set; }

    public string? StartDate { get; set; }

    public string? EndDate { get; set; }

    public string? SaleGtin { get; set; }

    public string? SaleLastUpdate { get; set; }

    public string? UseNdc11 { get; set; }

    public string? NoUseNdc { get; set; }

    public string? UseGtin { get; set; }

    public string? UseLastUpdate { get; set; }

    public string? CvxCode { get; set; }

    public string? CvxDescription { get; set; }

    public string? MvxCode { get; set; }
}
