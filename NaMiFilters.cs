using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaMiLib
{
    public static class NaMiFilters
    {
            public static NaMiFilter id  = (new NaMiFilter("Id", "id", true));
            public static NaMiFilter woelfling = (new NaMiFilter("Alle Wölflinge", "woelfling", false));
            public static NaMiFilter jungpfadfinder = (new NaMiFilter("Alle Jungpfadfinder", "jungpfadfinder", false));
            public static NaMiFilter pfadfinder = (new NaMiFilter("Alle Pfadfinder", "pfadfinder", false));
            public static NaMiFilter rover = (new NaMiFilter("Alle Rover", "rover", false));
            public static NaMiFilter wiederverwendenflag = (new NaMiFilter("Datenweiterverwendung", "wiederverwendenFlag", true));
            public static NaMiFilter email = (new NaMiFilter("E-Mail", "email", true));
            public static NaMiFilter emailVertretungsberechtigter = (new NaMiFilter("E-Mail Erziehungsberechtigter", "emailVertretungsberechtigter", true));
            public static NaMiFilter geburtsDatum = (new NaMiFilter("Geburtsdatum", "geburtsDatum", true));
            public static NaMiFilter lastUpdated = (new NaMiFilter("Letzte Änderung", "lastUpdated", true));
            public static NaMiFilter mitgliedsNummer = (new NaMiFilter("Mitglieds-Nr.", "mitgliedsNummer", true));
            public static NaMiFilter mglType = (new NaMiFilter("Mitgliedstyp", "mglType", true));
            public static NaMiFilter nachname = (new NaMiFilter("Nachname", "nachname", true));
            public static NaMiFilter staatsangehoerigkeit = (new NaMiFilter("Staatsangehörigkeit", "", true));
            public static NaMiFilter gruppierung = (new NaMiFilter("Stammgruppierung", "gruppierung", true));
            public static NaMiFilter status = (new NaMiFilter("Status", "status", true));
            public static NaMiFilter vorname = (new NaMiFilter("Vorname", "vorname", true));
    }
}
