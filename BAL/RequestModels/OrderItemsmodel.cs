using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.RequestModels
{
    public class OrderItemsmodel
    {
        public Guid? id { get; set; }
        public Guid? itemid { get; set; }
        public string? quantity { get; set; }
        public string? orderitemdesc { get; set; }
        public string? typeofpackage { get; set; }
        public string? unitprice { get; set; }
    }
}
