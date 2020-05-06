using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EGUI_L2.Models
{
    public class CalendarViewModel
    {
        public string header { get; set; }
        public int shift { get; set; }
        public int curr_day { get; set; }
        public int days_in_month { get; set; }
        public int[] highlight_days { get; set; }
    }
}
