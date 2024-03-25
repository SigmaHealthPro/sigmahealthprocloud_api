using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.RequestModels
{
    public class Userloginmodel
    {
        [Required]
        public Guid? id { get; set; }
        [Required]
        public string? username { get; set; }
        [Required]
        public string? firstName { get; set; }
        [Required]
        public string? lastName { get; set; }
        [Required]
        public string? email { get; set; }
       
        [Required]
        public string? password { get; set; }
        [Required]
        public string? position { get; set; }
        [Required]
        public string? gender { get; set; }
        [Required]
        public string? phone { get; set; }
        [Required]
        public string? birthdate { get; set; }
        [Required]
        public string? role { get; set; }
        [Required]
        public string? facility { get; set; }
        [Required]
        public string? juridiction { get; set; }
        [Required]
        public string? image { get; set; }
        [Required]
        public Guid? juridictionid { get; set;}
        [Required]
        public Guid? organizationid { get; set; }


    }
}
