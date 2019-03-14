using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NaMiLib
{
    public class Commands
    {
        public const string Deploy = "ica/rest/";
        public const string baseURL = "https://nami.dpsg.de/"; //URL + Deploy + API + MajorVersion + "/" + MiniorVersion + "/";

        public const string Login = "ica/pages/login.jsp";
        public const string Gruppierungen = "ica/rest/nami/gruppierungen/filtered-for-navigation/gruppierung/node/";
        public const string RootGruppierung = "ica/rest/nami/gruppierungen/filtered-for-navigation/gruppierung/node/root";
        public const string Mitglied = "ica/rest/nami/mitglied/filtered-for-navigation/gruppierung/gruppierung/{GID}/{MID}";
        public const string Mitglieder = "ica/rest/nami/mitglied/filtered-for-navigation/gruppierung/gruppierung/{GID}/flist";
        public const string SearchMeta = "ica/rest/nami/search-multi/meta-for-search-entity";
        public const string Search = "ica/rest/nami/search-multi/result-list?searchedValues={VALUES}&start=0&limit=5000";
        public const string Filter = "ica/rest/nami/mitglied/filtered-for-navigation/gruppierung/gruppierung/{GID}/";
        //public const string Filter = "ica/rest/nami/mitglied/filtered-for-navigation/gruppierung/gruppierung/{GID}/?filterString={FILTER}&searchString={SEARCH}&page=1&start=0&limit=5000";
        public const string Logout = "ica/rest/nami/auth/logout";
        public const string Taetigkeiten = "ica/rest/nami/zugeordnete-taetigkeiten/filtered-for-navigation/gruppierung-mitglied/mitglied/{MID}/";
        public const string Taetigkeit = "ica/rest/nami/zugeordnete-taetigkeiten/filtered-for-navigation/gruppierung-mitglied/mitglied/{MID}/{TID}";

        public const string Geschlecht = "ica/rest/baseadmin/geschlecht/";
        public const string Staatsangehörigkeit = "ica/rest/baseadmin/staatsangehoerigkeit/";
        public const string Konfession = "ica/rest/baseadmin/konfession/";
        public const string Region = "ica/rest/baseadmin/region/";
        public const string Land = "ica/rest/baseadmin/land/";
        public const string ZahlungsKonditionen = "ica/rest/baseadmin/zahlungskondition/";
        public const string Beitragsart = "ica/rest/namiBeitrag/beitragsartmgl/gruppierung/{GID}";




        
        
        
    }
}
