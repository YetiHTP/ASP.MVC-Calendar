using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EGUI_L2.Models
{
    public class EventViewModel
    {
        public string header { get; set; }
        public bool edited { get; set; }
        public string desc { get; set; }
        public DateTime time { get; set; }
    }
}
