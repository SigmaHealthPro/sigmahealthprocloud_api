using System;
using System.Collections.Generic;

namespace Web_API.Models;

public partial class OrderItem
{
    public Guid Id { get; set; }

    public int OrderItemId { get; set; }

    public string? OrderItemDesc { get; set; }

    public Guid? ProductId { get; set; }

    public Guid? OrderId { get; set; }

    public string? OrderItemStatus { get; set; }

    public string? UnitPrice { get; set; }

    public string? Quantity { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public string? CreatedBy { get; set; }

    public string? UpdatedBy { get; set; }

    public bool? Isdelete { get; set; }

    public virtual Order? Order { get; set; }
}
