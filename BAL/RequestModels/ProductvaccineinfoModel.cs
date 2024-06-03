using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.RequestModels
{
    public class ProductvaccineinfoModel
    {
        public string? vaccinename { get; set; }
        public string? cvxnotes { get; set; }
        public string? vaccinestatus { get; set; }
        public string? nonvaccine { get; set; }        
        public string? mvxcode { get; set; }
        public string? cvxcode { get; set; }
        public string? manufacturername { get; set; }

        public string? mvxnotes { get; set; }
        public string? productname { get; set; }
    }
}
