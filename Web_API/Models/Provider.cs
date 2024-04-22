using System;
using System.Collections.Generic;

namespace Web_API.Models;

public partial class Provider
{
    public Guid Id { get; set; }

    public string? ProviderId { get; set; }

    public string? ProviderName { get; set; }

    public Guid? FacilityId { get; set; }

    public string? ContactNumber { get; set; }

    public string? Email { get; set; }

    public string? ProviderType { get; set; }

    public string? Specialty { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public string? CreatedBy { get; set; }

    public string? UpdatedBy { get; set; }

    public virtual ICollection<Event> Events { get; set; } = new List<Event>();

    public virtual Facility? Facility { get; set; }
}
