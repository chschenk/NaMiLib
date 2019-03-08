using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NaMiLib
{
    public class NaMiTaetigkeit
    {
        public string gruppierung { get; set; }
        public string caeaGroup { get; set; }
        public string untergliederung { get; set; }
        public DateTime anlagedatum { get; set; }
        public DateTime aktivVon { get; set; }
        public string caeaGroupForGf { get; set; }
        public string beitragsArt { get; set; }
        public DateTime aktivBis { get; set; }
        public string taetigkeit { get; set; }
        public string rowCssClass { get; set; }
        public string mitglied { get; set; }
    }
}
