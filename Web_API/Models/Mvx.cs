using System;
using System.Collections.Generic;

namespace Web_API.Models;

public partial class Mvx
{
    public Guid Id { get; set; }

    public int MvxId { get; set; }

    public string? MvxCode { get; set; }

    public string? ManufacturerName { get; set; }

    public string? Notes { get; set; }

    public string? Status { get; set; }

    public string? ManufacturerId { get; set; }

    public DateOnly? ReleaseDate { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public string? CreatedBy { get; set; }

    public string? UpdatedBy { get; set; }

    public bool? Isdelete { get; set; }
}
