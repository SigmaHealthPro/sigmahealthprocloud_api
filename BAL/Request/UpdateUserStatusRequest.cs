using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Request
{
    public class UpdateUserStatusRequest
    {
        public Guid Id { get; set; }
        public bool? Status { get; set; }
    }
}
