using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NaMiLib
{
    public class NaMiResponse<DataT>
    {
        public bool success { get; set; }

        public DataT data { get; set; }

        public string responseType { get; set; }

        public string message { get; set; }

        public string title { get; set; }
    }
}
