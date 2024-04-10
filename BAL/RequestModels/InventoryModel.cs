using Data.Models;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BAL.RequestModels
{
    public class InventoryModel : BaseModel
    {
        public int InventoryId { get; set; }

        public DateTime? InventoryDate { get; set; }

        public Guid? ProductId { get; set; }

        public string? QuantityRemaining { get; set; }

        public DateTime? ExpirationDate { get; set; }

        public string? TempRecorded { get; set; }

        public string? UnitOfTemp { get; set; }

        public Guid? FacilityId { get; set; }
        public string? Facility { get; set; }
        public string? Product { get; set; }
        public string? User { get; set; }
        public Guid? SiteId { get; set; }
        public string? Site { get; set; }
        [Required]
        public bool IsEdit { get; set; }

    }
}
