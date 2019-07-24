using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SelectPaste.Model
{
    public class Rootobject
    {
        public string api { get; set; }
        public string v { get; set; }
        public string[] ret { get; set; }
        public Data data { get; set; }
    }

    public class Data
    {
        public string t { get; set; }
    }
}
