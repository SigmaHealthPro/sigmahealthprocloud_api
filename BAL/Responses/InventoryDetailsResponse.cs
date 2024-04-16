using Data.Models;


namespace BAL.Responses
{
    public class InventoryDetailsResponse
    {
        public Guid Id { get; set; }
        public DateTime? InventoryDate { get; set; }
        public DateTime? ExpirationDate { get; set; }

        public string? TempRecorded { get; set; }
        public Guid? ProductId { get; set; }
        public Guid? FacilityId { get; set; }
        public Guid? SiteId { get; set; }

        //Person
        public string? QuantityRemaining { get; set; }
        public string? UnitOfTemp { get; set; }
        public string? Facility { get; set; }
        public string? Product { get; set; }
        public string? User { get; set; }
        public string? Site { get; set; }
     

        public static InventoryDetailsResponse FromInventoryEntity(Inventory inventory, Facility facility,Site site, Product product)

        {
            return new InventoryDetailsResponse
            {
                Id = inventory.Id,
                InventoryDate = inventory.InventoryDate,
                QuantityRemaining = inventory.QuantityRemaining,
                ExpirationDate = inventory.ExpirationDate,
                TempRecorded = inventory.TempRecorded,
                UnitOfTemp = inventory.UnitOfTemp,
                Facility = facility.FacilityName,
                Product = product.ProductName,
                Site = site.SiteName,
             
            };
        }
    }
}
