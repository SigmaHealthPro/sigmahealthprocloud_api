using System;
using System.Collections.Generic;

namespace Web_API.Models;

public partial class Country
{
    public Guid Id { get; set; }

    public int CountryId { get; set; }

    public string? CountryName { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public string? CreatedBy { get; set; }

    public string? UpdatedBy { get; set; }

    public bool? Isdelete { get; set; }

    public string? Alpha2code { get; set; }

    public string? Alpha3code { get; set; }

    public virtual ICollection<Address> Addresses { get; set; } = new List<Address>();

    public virtual ICollection<Patient> Patients { get; set; } = new List<Patient>();

    public virtual ICollection<State> States { get; set; } = new List<State>();
}
