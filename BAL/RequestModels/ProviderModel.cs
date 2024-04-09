
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.RequestModels
{
    public class ProviderModel : BaseModel
    {
        [Required]
        public string? ProviderId { get; set; }
        [Required]
        public string? ProviderName { get; set; }
        [Required]
        public string? ProviderType { get; set; }
        [Required]
        public string? ContactNumber { get; set; }
        [Required]
        public string? Email { get; set; }
        [Required]
        public string? Speciality { get; set; }
        [Required]
        public Guid? FacilityId { get; set; }
        [Required]
        public string? FacilityName { get; set; }
        [Required]
        public string? CityName { get; set; }
        [Required]
        public string? StateName { get; set; }
        [Required]
        public string? ZipCode { get; set; }
        [Required]
        public Guid? AddressId { get; set; }

    }

}

