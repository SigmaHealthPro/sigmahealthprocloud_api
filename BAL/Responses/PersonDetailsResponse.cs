﻿using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Responses
{
    public class PersonDetailsResponse
    {
        public Guid Id { get; set; }

        public int PersonId { get; set; }

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

        public static PersonDetailsResponse FromPersonEntity(Person person)
        {
            return new PersonDetailsResponse
            {
                Id = person.Id,
                PersonId = person.PersonId,
                PersonType = person.PersonType,
                FirstName = person.FirstName,
                LastName = person.LastName,
                Gender = person.Gender,
                CreatedDate = person.CreatedDate,
                UpdatedDate = person.UpdatedDate,
                CreatedBy = person.CreatedBy,
                UpdatedBy = person.UpdatedBy,
                Isdelete = person.Isdelete,
                DateOfBirth = person.DateOfBirth,
                MiddleName = person.MiddleName,
                MotherFirstName = person.MotherFirstName,
                MotherLastName = person.MotherLastName,
                MotherMaidenLastName = person.MotherMaidenLastName,
                BirthOrder = person.BirthOrder,
                BirthStateId = person.BirthStateId
            };
        }

    }
}
