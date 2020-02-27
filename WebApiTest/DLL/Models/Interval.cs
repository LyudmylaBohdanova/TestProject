using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL.Models
{
    public class Interval
    {
        public int ID { get; set; }
        public DateTime BeginDate { get; set; }
        public DateTime EndDate { get; set; }

        public virtual IEnumerable<LogInterval> LogInterval { get; set; }
    }
}
