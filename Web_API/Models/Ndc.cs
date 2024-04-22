using System;
using System.Collections.Generic;

namespace Web_API.Models;

public partial class Ndc
{
    public Guid Id { get; set; }

    public int NdcId { get; set; }

    public string? SaleNdc11 { get; set; }

    public string? SaleProprietaryName { get; set; }

    public string? SaleLabler { get; set; }

    public string? SaleDate { get; set; }

    public string? EndDate { get; set; }

    public string? SaleGtin { get; set; }

    public string? SaleLastUpdate { get; set; }

    public string? UseNdc11 { get; set; }

    public string? NouseNdc { get; set; }

    public string? UseGtin { get; set; }

    public string? UseLastUpdate { get; set; }

    public Guid? CvxId { get; set; }

    public Guid? MvxId { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public string? CreatedBy { get; set; }

    public string? UpdatedBy { get; set; }

    public bool? Isdelete { get; set; }
}
