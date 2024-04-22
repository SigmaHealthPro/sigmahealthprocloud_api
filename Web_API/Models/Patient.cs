using System;
using System.Collections.Generic;

namespace Web_API.Models;

public partial class Patient
{
    public Guid Id { get; set; }

    public int PatientId { get; set; }

    public DateTime? DateOfHistoryVaccine { get; set; }

    public string? AliasFirstName { get; set; }

    public string? AliasLastName { get; set; }

    public string? AliasMidddleName { get; set; }

    public string? PatientPrimaryLanguage { get; set; }

    public string? PatientStatusJuridictionLevel { get; set; }

    public string? PatientStatus { get; set; }

    public string? ProtectionIndicator { get; set; }

    public string? PatientStatusIndicatorProviderlevel { get; set; }

    public DateTime? ProtectionIndicatorEffectiveDate { get; set; }

    public string? ReminderRecallStatus { get; set; }

    public DateTime? ReminderRecallStatusEffectiveDate { get; set; }

    public string? ResponsiblePersonFirstName { get; set; }

    public string? ResponsiblePersonLastName { get; set; }

    public string? ResponsiblePersonMidddleName { get; set; }

    public string? ResponsiblePersonRelationshipToPatient { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public string? CreatedBy { get; set; }

    public string? UpdatedBy { get; set; }

    public bool? Isdelete { get; set; }

    public Guid? PersonId { get; set; }

    public Guid? CityId { get; set; }

    public Guid? StateId { get; set; }

    public Guid? CountryId { get; set; }

    public virtual City? City { get; set; }

    public virtual Country? Country { get; set; }

    public virtual Person? Person { get; set; }

    public virtual State? State { get; set; }
}
