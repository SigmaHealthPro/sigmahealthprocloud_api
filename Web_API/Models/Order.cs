using System;
using System.Collections.Generic;

namespace Web_API.Models;

public partial class Order
{
    public Guid Id { get; set; }

    public int OrderId { get; set; }

    public Guid? UserId { get; set; }

    public Guid? FacilityId { get; set; }

    public DateTime? OrderDate { get; set; }

    public string? OrderTotal { get; set; }

    public string? TaxAmount { get; set; }

    public string? DiscountAmount { get; set; }

    public string? Incoterms { get; set; }

    public Guid? TermsConditionsId { get; set; }

    public string? OrderStatus { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public string? CreatedBy { get; set; }

    public string? UpdatedBy { get; set; }

    public bool? Isdelete { get; set; }

    public virtual Facility? Facility { get; set; }

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    public virtual ICollection<Shipment> Shipments { get; set; } = new List<Shipment>();

    public virtual User? User { get; set; }
}
