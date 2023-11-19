﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    [Table("countries")]
    public class countries : baseclass
    {
        [Key]
        public int country_id { get; set; }
        [DataType("character varying")]
        public string? country_name { get; set; }
        public int zipcode { get; set; }
    }
}