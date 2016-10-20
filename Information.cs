using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace Finance_Calendar
{
    public class Information
    {
        public string Ins { get; set; }
        public string Outs { get; set; }
        public string DayOfWeek { get; set; }
        public string Notes { get; set; }
        public DateTime DayOfMonth { get; set; }

        public string InCheckInt { get; set; }
        public string OutCheckedInt { get; set; }
        public string CheckedNotes { get; set; }
        public DateTime CheckedDate { get; set; }

        public DateTime StartingDate { get; set; }
        public string StartingValue { get; set; }

    }
}
