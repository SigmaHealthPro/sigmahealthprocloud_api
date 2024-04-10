namespace BAL.Constant
{
    public class SearchInventoryParams
    {
        public string keyword { get; set; }
        public int pagenumber { get; set; }
        public int pagesize { get; set; }
        public int InventoryId { get; set; }

        public DateTime? InventoryDate { get; set; }

        public Guid? ProductId { get; set; }

        public string? QuantityRemaining { get; set; }

        public DateTime? ExpirationDate { get; set; }

        public string? TempRecorded { get; set; }

        public string? UnitOfTemp { get; set; }

        public Guid? UserId { get; set; }

        public string? FacilityName { get; set; }
        public string? SiteName { get; set; }
 

        //public string sortBy { get; set; } = "patientname";
        //public string sortDirection { get; set; } = "desc";


    }
}