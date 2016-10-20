using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Finance_Calendar
{
    public class DayInfo
    {
        public TextBox In { get; set; }
        public TextBox Out { get; set; }
        public TextBox Note { get; set; }
        public DateTime SaveDate { get; set; }

        public DateTime StartingDate { get; set; }
        public decimal StartingValue { get; set; }
    }
}
