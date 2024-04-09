using Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.RequestModels
{
    public class RespOrderModel:BaseModel
    {
        public List<orderItems> OrderofItems { get; set; }
        public address Address { get; set; }
        public shipment Shiping { get; set; }

        public Guid? UserId { get; set; }

        public Guid? FacilityId { get; set; }

        public string? Facility { get; set; }

        public DateTime? OrderDate { get; set; }

        public string? OrderTotal { get; set; }

        public string? TaxAmount { get; set; }

        public string? DiscountAmount { get; set; }

        public string? Incoterms { get; set; }

        public Guid? TermsConditionsId { get; set; }

        public string? OrderStatus { get; set; }
        
    }
    public class orderItems : BaseModel
    {
        public string? OrderItemDesc { get; set; }
        public Guid? ProductId { get; set; }        
        public string? OrderItemStatus { get; set; }
        public string? UnitPrice { get; set; }
        public string? Quantity { get; set; }
    }
    public class address : BaseModel
    {
        public string? Line1 { get; set; }
        public string? Line2 { get; set; }

        [DefaultValue(null)]
        public string? Suite { get; set; }
        public Guid? Countyid { get; set; }

        public Guid? Countryid { get; set; }

        public Guid? Stateid { get; set; }
        public Guid? Cityid { get; set; }
        public string? ZipCode { get; set; }
    }
    public class shipment : BaseModel
    {
        public DateTime? ShipmentDate { get; set; }
        public string? PackageSize { get; set; }
        public string? SizeUnitofMesure { get; set; }
        public string? PackageLength { get; set; }
        public string? PackageWidth { get; set; }
        public string? PackageHeight { get; set; }
        public string? WeightUnitofMeasure { get; set; }
        public string? TypeofPackagingMaterial { get; set; }
        public string? TypeofPackage { get; set; }
        public string? Storingtemparature { get; set; }
        public string? TemperatureUnitofmeasure { get; set; }
        public string? NoofPackages { get; set; }
        public DateTime? Expecteddeliverydate { get; set; }
        public Guid? ShippmentAddressId { get; set; }
        public string? RecievingHours { get; set; }
        public string? RecieverId { get; set; }
        public string? IsSignatureneeded { get; set; }
        public string? TrackingNumber { get; set; }
        public string? Status { get; set; }
    }
}
