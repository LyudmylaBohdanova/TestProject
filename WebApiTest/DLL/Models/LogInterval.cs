using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL.Models
{
    public class LogInterval
    {
        public int ID { get; set; }
        public DateTime DateChange { get; set; }
        public string TypeChange { get; set; }
        public int Interval_ID { get; set; }
        public DateTime? BeginDate { get; set; }
        public DateTime? EndDate { get; set; }

        public virtual Interval Interval { get; set; }

        public LogInterval() { }
        public LogInterval(DateTime date, string type, int id = 0)
        {
            DateChange = date;
            TypeChange = type;
            Interval_ID = id;
            BeginDate = null;
            EndDate = null;
        }
    }
}
