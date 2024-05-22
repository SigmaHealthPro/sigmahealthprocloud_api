using System;
using System.Collections.Generic;

namespace Data.Models;

public partial class Inventory
{
    public Guid Id { get; set; }

    public int InventoryId { get; set; }

    public DateTime? InventoryDate { get; set; }

    public Guid? ProductId { get; set; }

    public string? QuantityRemaining { get; set; }

    public DateTime? ExpirationDate { get; set; }

    public string? TempRecorded { get; set; }

    public string? UnitOfTemp { get; set; }

    public Guid? UserId { get; set; }

    public Guid? FacilityId { get; set; }

    public Guid? SiteId { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public string? CreatedBy { get; set; }

    public string? UpdatedBy { get; set; }

    public bool? Isdelete { get; set; }

    public string? Lotnumber { get; set; }

    public virtual Facility? Facility { get; set; }

    public virtual Product? Product { get; set; }

    public virtual Site? Site { get; set; }

    public virtual User? User { get; set; }
}
