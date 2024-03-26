using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Constant
{
    public class SearchProviderParams
    {

        public string? keyword { get; set; }
        public int pagenumber { get; set; }
        public int pagesize { get; set; }
        public string? providerName { get; set; }
        public string? providerType { get; set; }
        public string? email { get; set; }
        public string? speciality { get; set; }
        public string? facilityid { get; set; }
        public string? facility_name { get; set; }
        public string? city { get; set; }
        public string? contact_number { get; set; }
        public string? state { get; set; }
        public string? zipcode { get; set; }
        public string? orderby { get; set; }
    }
}
