using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Newtonsoft.Json;

namespace NaMiLib
{
    [JsonConverter(typeof(CustomKontoverbindungsConverter))]
    public class NaMiKontoverbindung
    {
        public int id { get; set; }
        public string institut { get; set; }
        public string bankleitzahl { get; set; }
        public string kontonummer { get; set; }
        public string iban { get; set; }
        public string bic { get; set; }
        public string kontoinhaber { get; set; }
        public int mitgliedsNummer { get; set; }
        public string zahlungsKondition { get; set; }
        public object zahlungsKonditionId { get; set; }
    }
}
