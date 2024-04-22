using System;
using System.Collections.Generic;

namespace Web_API.Models;

public partial class Facility
{
    public Guid Id { get; set; }

    public string? FacilityId { get; set; }

    public string? FacilityName { get; set; }

    public string? AdministeredAtLocation { get; set; }

    public string? SendingOrganization { get; set; }

    public string? ResponsibleOrganization { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public string? CreatedBy { get; set; }

    public string? UpdatedBy { get; set; }

    public bool? Isdelete { get; set; }

    public Guid? OrganizationsId { get; set; }

    public Guid? UserId { get; set; }

    public bool? Status { get; set; }

    public virtual ICollection<Inventory> Inventories { get; set; } = new List<Inventory>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual Organization? Organizations { get; set; }

    public virtual ICollection<Provider> Providers { get; set; } = new List<Provider>();

    public virtual ICollection<Site> Sites { get; set; } = new List<Site>();

    public virtual User? User { get; set; }
}
