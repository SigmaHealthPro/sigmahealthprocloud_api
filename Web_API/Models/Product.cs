using System;
using System.Collections.Generic;

namespace Web_API.Models;

public partial class Product
{
    public Guid Id { get; set; }

    public int ProductId { get; set; }

    public string? ProductName { get; set; }

    public Guid? CvxCodeId { get; set; }

    public Guid? MvxCodeId { get; set; }

    public string? ProductNameStatus { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public string? CreatedBy { get; set; }

    public string? UpdatedBy { get; set; }

    public bool? Isdelete { get; set; }

    public DateOnly? ReleaseDate { get; set; }

    public virtual ICollection<Inventory> Inventories { get; set; } = new List<Inventory>();
}
