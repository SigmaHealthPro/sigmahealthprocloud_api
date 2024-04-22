using System;
using System.Collections.Generic;

namespace Web_API.Models;

public partial class Address
{
    public Guid Id { get; set; }

    public string? AddressId { get; set; }

    public string? Line1 { get; set; }

    public string? Line2 { get; set; }

    public string? Suite { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public string? CreatedBy { get; set; }

    public string? UpdatedBy { get; set; }

    public Guid? CountyId { get; set; }

    public Guid? CountryId { get; set; }

    public Guid? StateId { get; set; }

    public Guid? CityId { get; set; }

    public string? ZipCode { get; set; }

    public virtual City? City { get; set; }

    public virtual Country? Country { get; set; }

    public virtual County? County { get; set; }

    public virtual ICollection<Organization> Organizations { get; set; } = new List<Organization>();

    public virtual ICollection<Shipment> Shipments { get; set; } = new List<Shipment>();

    public virtual ICollection<Site> Sites { get; set; } = new List<Site>();

    public virtual State? State { get; set; }
}
