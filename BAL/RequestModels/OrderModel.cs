using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.RequestModels
{
    public class OrderModel:BaseModel
    {
        
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
     
        public string? OrderItemDesc { get; set; }         
        
        public string? Product { get; set; }
        
        public string? CVXDesc { get; set; }
        
        public Guid? ProductId { get; set; }
        public string? manufacturername { get; set; }

        public int OrderId { get; set; }
       
        public Guid? OrderItemId { get; set; }
      
        public string? OrderItemStatus { get; set; }
       
        public string? UnitPrice { get; set; }
       
        public string? Quantity { get; set; }

    }
}
