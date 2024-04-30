using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.RequestModels
{
    public class VaccineModel:BaseModel
    {
        public string? cvxcode { get; set; }
        public string? cvxdesc { get; set; }

        public string? vaccinename { get; set; }
        public string? cvxnotes { get; set; }
        public string? vaccinestatus { get; set; }
        public string? nonvaccine { get; set; }
        public DateOnly? cvxreleasedate { get; set; }
        public string? mvxcode { get; set; }
        public string? manufacturername { get; set; }

        public string? mvxnotes { get; set; }
        public string? status { get; set; }
        public DateOnly? mvxreleasedate { get; set; }
        public string? vaccinebrandname { get; set; }
        public string? packaging { get; set; }
        public string? costperdose { get; set; }
        public string? privatesectorcost { get; set; }
        public string? contractenddate { get; set; }
        public string? contractnumber { get; set; }       


    }
}
