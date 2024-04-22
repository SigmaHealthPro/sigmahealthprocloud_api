using System;
using System.Collections.Generic;

namespace Web_API.Models;

public partial class Site
{
    public Guid Id { get; set; }

    public string? SiteId { get; set; }

    public string? SiteName { get; set; }

    public string? SiteType { get; set; }

    public string? ParentSite { get; set; }

    public string? SiteContactPerson { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public string? CreatedBy { get; set; }

    public string? UpdatedBy { get; set; }

    public bool? Isdelete { get; set; }

    public Guid? FacilityId { get; set; }

    public Guid? AddressId { get; set; }

    public bool? IsImmunizationSite { get; set; }

    public string? SitePinNumber { get; set; }

    public virtual Address? Address { get; set; }

    public virtual ICollection<Event> Events { get; set; } = new List<Event>();

    public virtual Facility? Facility { get; set; }

    public virtual ICollection<Inventory> Inventories { get; set; } = new List<Inventory>();
}
