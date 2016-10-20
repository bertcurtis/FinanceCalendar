using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Finance_Calendar
{
    public class CheckedInfo
    {
        public TextBox InChecked { get; set; }
        public TextBox OutChecked { get; set; }
        public TextBox NoteCheck { get; set; }
        public DateTime SaveDateChecked { get; set; }
    }
}
