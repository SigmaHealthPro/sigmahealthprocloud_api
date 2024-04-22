using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Request
{
    public class CreateUserRequest
    {
        public int SequenceId { get; set; }

        public string? UserId { get; set; }

        public string? Password { get; set; }

        public string? UserType { get; set; }

        public string? Gender { get; set; }

        public string? Designation { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public string? CreatedBy { get; set; }

        public string? UpdatedBy { get; set; }

        public bool? Isdelete { get; set; }

        public Guid? PersonId { get; set; }

        public string? ImageUrl { get; set; }

        public bool? Status { get; set; }
    }
}
