using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.RequestModels
{
    public class UserAddressModel
    {
        public Guid? id { get; set; }
        public string? Addressid { get; set; }
        public string? Line1 { get; set; }
        public string? Line2 { get; set; }        
        public string? Suite { get; set; }
        public string? Cityname { get; set; }
        public string? Countyname { get; set; }
        public string? Statename { get; set; }
        public string? Countryname { get; set; }
        public Guid? Countyid { get; set; }
        public Guid? Countryid { get; set; }
        public Guid? Stateid { get; set; }
        public Guid? Cityid { get; set; }
        public string? ZipCode { get; set; }
    }
}
