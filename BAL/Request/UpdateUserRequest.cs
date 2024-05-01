using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Request
{
    public class UpdateUserRequest
    {
        public Guid Id { get; set; }
        public string? UserId { get; set; }

        public string? Password { get; set; }

        public string? UserType { get; set; }

        public string? Gender { get; set; }

        public string? Designation { get; set; }

        public string? UpdatedBy { get; set; }

        public Guid? PersonId { get; set; }

        public string? ImageUrl { get; set; }
    }
}
