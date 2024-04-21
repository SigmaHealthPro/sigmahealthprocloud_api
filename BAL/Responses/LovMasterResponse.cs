using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Responses
{
    public class LovMasterResponse
    {
        public Guid Id { get; set; }

        public int ReferenceId { get; set; }

        public string? Key { get; set; }

        public string? Value { get; set; }

        public string? LovType { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public string? CreatedBy { get; set; }

        public string? UpdatedBy { get; set; }

        public bool? Isdelete { get; set; }

        public string? LongDescription { get; set; }

        public bool? Status { get; set; }
    }

}
