﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.RequestModels
{
    public class ContactsModel
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public int ContactsId { get; set; }

        [Required]
        public string? ContactValue { get; set; }

        [Required]

        public string? ContactType { get; set; }

    }
}