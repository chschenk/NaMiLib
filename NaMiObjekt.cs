using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaMiLib
{
    public class NaMiObjekt
    {
        public string descriptor { get; set; }
        public string name { get; set; }
        public string representedClass { get; set; }
        public int id { get; set; }

        public override string ToString()
        {
            return descriptor;
        }
    }
}
