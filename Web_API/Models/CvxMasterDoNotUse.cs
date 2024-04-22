using System;
using System.Collections.Generic;

namespace Web_API.Models;

public partial class CvxMasterDoNotUse
{
    public string? CvxDescription { get; set; }

    public string? VaccineName { get; set; }

    public string? Note { get; set; }

    public string? VaccineStatus { get; set; }

    public string? NonVaccine { get; set; }

    public long? CvxCode { get; set; }

    public DateOnly? UpdateDate { get; set; }
}
