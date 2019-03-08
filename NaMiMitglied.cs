using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Newtonsoft.Json;

namespace NaMiLib
{

    public class NaMiMitglied
    {
        public NaMiMitglied()
        {
            stufe = "";
            jungpfadfinder = "";
            woelfling = "";
            pfadfinder = "";
            rover = "";
        }
        [JsonProperty]
        public bool sonst02 { get; set; }
        [JsonProperty]
        public string emailVertretungsberechtigter { get; set; }
        [JsonProperty]
        public bool sonst01 { get; set; }
        [JsonProperty]
        public string beitragsart { get; set; }
        [JsonIgnore]
        public object ersteUntergliederungId { get; set; }
        [JsonProperty]
        public int staatsangehoerigkeitId { get; set; }
        [JsonIgnore]
        public string mglTypeId { get; set; }
        [JsonProperty]
        public string mglType { get; set; }
        [JsonProperty]
        public string rover { get; set; }
        [JsonProperty]
        public string plz { get; set; }
        [JsonProperty]
        public string geburtsDatum { get; set; }
        [JsonProperty]
        public int regionId { get; set; }
         [JsonProperty]
        public string eintrittsdatum { get; set; }
        [JsonProperty]
        public object konfessionId { get; set; }
        [JsonProperty]
        public string telefon3 { get; set; }
        [JsonProperty]
        public string status { get; set; }
        [JsonIgnore]
        public string lastUpdated { get; set; }
        [JsonIgnore]
        public int mitgliedsNummer { get; set; }
        [JsonProperty]
        public string telefon1 { get; set; }
        [JsonProperty]
        public string telefon2 { get; set; }
        [JsonProperty]
        public string gruppierung { get; set; }
        [JsonProperty]
        public string stufe { get; set; }

        [JsonConverter(typeof(CustomKontoverbindungsConverter))]
        public NaMiKontoverbindung kontoverbindung { get; set; }
        [JsonProperty]
        public string email { get; set; }
        [JsonProperty]
        public string region { get; set; }
        [JsonIgnore]
        public object ersteTaetigkeitId { get; set; }
        [JsonProperty]
        public string telefax { get; set; }
        [JsonProperty]
        public int geschlechtId { get; set; }
        [JsonProperty]
        public string geschlecht { get; set; }
        [JsonProperty]
        public string nameZusatz { get; set; }
        [JsonProperty]
        public string land { get; set; }
        [JsonProperty]
        public object ersteTaetigkeit { get; set; }
        [JsonProperty]
        public bool zeitschriftenversand { get; set; }
        [JsonProperty]
        public int version { get; set; }
        [JsonProperty]
        public string gruppierungId { get; set; }
        [JsonProperty]
        public int id { get; set; }
        [JsonProperty]
        public string strasse { get; set; }
        [JsonProperty]
        public string staatsangehoerigkeit { get; set; }
        [JsonProperty]
        public string ort { get; set; }
        [JsonProperty]
        public string nachname { get; set; }
        [JsonProperty]
        public string vorname { get; set; }
        [JsonProperty]
        public string woelfling { get; set; }
        [JsonProperty]
        public object ersteUntergliederung { get; set; }
        [JsonProperty]
        public string pfadfinder { get; set; }
        [JsonProperty]
        public object staatsangehoerigkeitText { get; set; }
        [JsonProperty]
        public string jungpfadfinder { get; set; }
        [JsonProperty]
        public object fixBeitrag { get; set; }
        [JsonProperty]
        public List<int> beitragsartId { get; set; }
        [JsonProperty]
        public bool wiederverwendenFlag { get; set; }
        [JsonProperty]
        public int landId { get; set; }
        [JsonProperty]
        public string konfession { get; set; }
    }

}
