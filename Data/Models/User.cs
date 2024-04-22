using System;
using System.Collections.Generic;

namespace Data.Models;

public partial class User
{
    public Guid Id { get; set; }

    public int SequenceId { get; set; }

    public string? UserId { get; set; }

    public string? Password { get; set; }

    public string? UserType { get; set; }

    public string? Gender { get; set; }

    public string? Designation { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public string? CreatedBy { get; set; }

    public string? UpdatedBy { get; set; }

    public bool? Isdelete { get; set; }

    public Guid? PersonId { get; set; }

    public string? ImageUrl { get; set; }

    public bool? Status { get; set; }

    public virtual ICollection<Facility> Facilities { get; set; } = new List<Facility>();

    public virtual ICollection<Inventory> Inventories { get; set; } = new List<Inventory>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual Person? Person { get; set; }
}
