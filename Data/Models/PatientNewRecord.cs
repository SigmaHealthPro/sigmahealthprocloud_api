using System;
using System.Collections.Generic;

namespace Data.Models;

public partial class PatientNewRecord
{
    public Guid Id { get; set; }

    public string? PersonType { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? Gender { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public string? CreatedBy { get; set; }

    public string? UpdatedBy { get; set; }

    public bool? Isdelete { get; set; }

    public string? DateOfBirth { get; set; }

    public string? MiddleName { get; set; }

    public string? MotherFirstName { get; set; }

    public string? MotherLastName { get; set; }

    public string? MotherMaidenLastName { get; set; }

    public string? BirthOrder { get; set; }

    public Guid? BirthStateId { get; set; }

    public static PatientNewRecord FromPatientNewRecordEntity(PatientNewRecord record)
    {
        return new PatientNewRecord
        {
            Id = record.Id,
            PersonType = record.PersonType,
            FirstName = record.FirstName,
            LastName = record.LastName,
            Gender = record.Gender,
            CreatedDate = record.CreatedDate,
            UpdatedDate = record.UpdatedDate,
            CreatedBy = record.CreatedBy,
            UpdatedBy = record.UpdatedBy,
            Isdelete = record.Isdelete,
            DateOfBirth = record.DateOfBirth,
            MiddleName = record.MiddleName,
            MotherFirstName = record.MotherFirstName,
            MotherLastName = record.MotherLastName,
            MotherMaidenLastName = record.MotherMaidenLastName,
            BirthOrder = record.BirthOrder,
            BirthStateId = record.BirthStateId
        };
    }
}
