using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Constant
{
    public class SearchEventParams
    {

        public string? keyword { get; set; }
        public int pagenumber { get; set; }
        public int pagesize { get; set; }
        public string? EventName { get; set; }
        public DateTime? EventDate { get; set; }
        public string? VaccineName { get; set; }
        public string? ProviderName { get; set; }
        public string? SiteName { get; set; }
        public string? orderby { get; set; }
    }
}
