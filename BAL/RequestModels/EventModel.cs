using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.RequestModels
{
    public class EventModel : BaseModel
    {
        public string? EventName { get; set; }
        public DateTime? EventDate { get; set; }
        public string? VaccineName { get; set; }
        public Guid? ProviderId { get; set; }
        public string? ProviderName { get; set; }
        public Guid? SiteId { get; set; }
        public string? SiteName { get; set; }

    }
}
