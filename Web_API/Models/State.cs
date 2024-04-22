using System;
using System.Collections.Generic;

namespace Web_API.Models;

public partial class State
{
    public Guid Id { get; set; }

    public int StateId { get; set; }

    public string? StateName { get; set; }

    public string? StateCode { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public string? CreatedBy { get; set; }

    public string? UpdatedBy { get; set; }

    public bool? Isdelete { get; set; }

    public Guid? CountryId { get; set; }

    public virtual ICollection<Address> Addresses { get; set; } = new List<Address>();

    public virtual ICollection<City> Cities { get; set; } = new List<City>();

    public virtual ICollection<County> Counties { get; set; } = new List<County>();

    public virtual Country? Country { get; set; }

    public virtual ICollection<Juridiction> Juridictions { get; set; } = new List<Juridiction>();

    public virtual ICollection<Patient> Patients { get; set; } = new List<Patient>();

    public virtual ICollection<Person> People { get; set; } = new List<Person>();
}
