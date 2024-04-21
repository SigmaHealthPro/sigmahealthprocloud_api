using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Request
{
    public class BestMatchRequest
    {
        public IEnumerable<object> Data { get; set; }
        public IEnumerable<object> TargetData { get; set; }
    }
}
