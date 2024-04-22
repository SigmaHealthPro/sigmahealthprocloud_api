using System;
using System.Collections.Generic;

namespace Web_API.Models;

public partial class Cvx
{
    public Guid Id { get; set; }

    public int CvxId { get; set; }

    public string? CvxCode { get; set; }

    public string? CvxDescription { get; set; }

    public string? VaccineName { get; set; }

    public string? Note { get; set; }

    public string? VaccineStatus { get; set; }

    public string? NonVaccine { get; set; }

    public DateOnly? ReleaseDate { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public string? CreatedBy { get; set; }

    public string? UpdatedBy { get; set; }

    public bool? Isdelete { get; set; }

    public virtual ICollection<Event> Events { get; set; } = new List<Event>();

    public virtual ICollection<VaccinePrice> VaccinePrices { get; set; } = new List<VaccinePrice>();
}
