using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using RestSharp;
using System.Text;
using System.Net;
using System.Web;
using System.Threading.Tasks;

namespace NaMiLib
{
    /// <summary>
    /// Ist die Schnittstelle für die Grundlegende Kommunikation mit den Webservice
    /// </summary>
    public class Connector
    {
        public static bool isAuthenticated = false;
        private static CookieContainer cookies = new CookieContainer();
        private const string LoginURL = "ica/rest/nami/auth/manual/sessionStartup";
        private RestClient client;
        List<NaMiFilter> Filters;
        public NaMiFilter ID = new NaMiFilter("", "", true);

        /// <summary>
        /// Initialisiert die Klasse
        /// </summary>
        public Connector()
        {
            client = new RestClient(Commands.baseURL);
            client.CookieContainer = new CookieContainer();
            client.FollowRedirects = true;
            client.UserAgent = "NaMi-REST-Lib by C. Schenk";
            client.AddHandler("text/plain", new JsonSerializer());
            isAuthenticated = false;

            Filters = new List<NaMiFilter>();
            Filters.Add(NaMiFilters.id);
            Filters.Add(NaMiFilters.woelfling);
            Filters.Add(NaMiFilters.jungpfadfinder);
            Filters.Add(NaMiFilters.pfadfinder);
            Filters.Add(NaMiFilters.rover);
            Filters.Add(NaMiFilters.wiederverwendenflag);
            Filters.Add(NaMiFilters.email);
            Filters.Add(NaMiFilters.emailVertretungsberechtigter);
            Filters.Add(NaMiFilters.geburtsDatum);
            Filters.Add(NaMiFilters.lastUpdated);
            Filters.Add(NaMiFilters.mitgliedsNummer);
            Filters.Add(NaMiFilters.mglType);
            Filters.Add(NaMiFilters.nachname);
            Filters.Add(NaMiFilters.staatsangehoerigkeit);
            Filters.Add(NaMiFilters.gruppierung);
            Filters.Add(NaMiFilters.status);
            Filters.Add(NaMiFilters.vorname);
        }

        /// <summary>
        /// Loggt einen Benutzer ein.
        /// </summary>
        /// <param name="username">Die Mitgliedsnummer des Mitglieds welches sich anmelden will</param>
        /// <param name="password">Das Password des Mitglieds</param>
        /// <returns></returns>
        public bool Login(string username, string password)
        {

            if (!isAuthenticated)
            {
                RestRequest req = new RestRequest(LoginURL, Method.POST);

                req.AddParameter("Login", "API");
                req.AddParameter("username", username);
                req.AddParameter("password", password);
                req.RequestFormat = DataFormat.Json;
                IRestResponse<NaMiLoginResponse> response;
                response = client.Execute<NaMiLoginResponse>(req);
                if (response.StatusCode == HttpStatusCode.OK)
                    isAuthenticated = true; //Dirty Hack we should check if we are connected ToDo: fix that
            }
            return isAuthenticated;
        }

        /// <summary>
        /// Loggt den eingeloggten User wieder aus. Falls Niemand eingeloogt ist passiert nichts.
        /// </summary>
        public void Logout()
        {
            RestRequest req = new RestRequest(Commands.Logout, Method.GET);
            client.Execute(req);
            client.CookieContainer = new CookieContainer();
            isAuthenticated = false;
        }

        /// <summary>
        /// Fordert Daten vom Webservice an.
        /// </summary>
        /// <typeparam name="DataT">Dateityp der angeforderten Daten</typeparam>
        /// <param name="cmd">URL zu der ageforderten Ressource</param>
        /// <returns></returns>
        public DataT GetData<DataT>(string cmd)
        {
            if (!isAuthenticated)
                throw new NaMiNotLoggedInException();
            RestRequest req = new RestRequest(cmd, Method.GET);
            string s = client.Execute(req).Content;
            var resp = client.Execute<NaMiResponse<DataT>>(req);
            if (!resp.Data.success)
                throw new NaMiException<DataT>(resp.Data);
            return resp.Data.data;
        }

        /// <summary>
        /// Gibt das angeforderte Mitglied zurück
        /// </summary>
        /// <param name="GruppierungsID">Gruppierungs ID der Gruppierung des angeforderten Mitglieds</param>
        /// <param name="MitgliedsID">Mitglieds ID des angeforderten Mitglieds</param>
        /// <returns></returns>
        public NaMiMitglied GetMitglied(int GruppierungsID, int MitgliedsID)
        {
            if (!isAuthenticated)
                throw new NaMiNotLoggedInException();
            RestRequest req = new RestRequest(Commands.Mitglied, Method.GET);
            req.AddUrlSegment("GID", GruppierungsID.ToString());
            req.AddUrlSegment("MID", MitgliedsID.ToString());
            var resp = client.Execute<NaMiResponse<NaMiMitglied>>(req);
            if (!resp.Data.success)
                return null;
            return resp.Data.data;
        }

        /// <summary>
        /// Gibt eine Liste vom Typ NaMiObjekt zurück welche alle Mitglieder einer Gruppierung enthält
        /// </summary>
        /// <param name="GruppierungsID">Die Gruppierungs ID der angeforderten Gruppierung</param>
        /// <returns></returns>
        public List<NaMiObjekt> GetMitglieder(int GruppierungsID)
        {
            if (!isAuthenticated)
                throw new NaMiNotLoggedInException();
            RestRequest req = new RestRequest(Commands.Mitglieder, Method.GET);
            req.AddUrlSegment("GID", GruppierungsID.ToString());
            var resp = client.Execute<NaMiResponse<List<NaMiObjekt>>>(req);
            if (!resp.Data.success)
                throw new NaMiException<List<NaMiObjekt>>(resp.Data);
            return resp.Data.data;
        }

        /// <summary>
        /// Gibt eine Liste vom Typ NaMiMitglied zurück welche alle Mitglieder einer Gruppierung enthält
        /// </summary>
        /// <param name="GruppierungsID">Die Gruppierungs ID der angeforderten Gruppierung</param>
        /// <returns></returns>
        public List<NaMiMitglied> GetMitgliederFullList(int GruppierungsID)
        {
            if (!isAuthenticated)
                throw new NaMiNotLoggedInException();
            List<NaMiMitglied> Mitglieder = new List<NaMiMitglied>();
            foreach (NaMiObjekt mitglied in GetMitglieder(GruppierungsID))
            {
                var m = GetMitglied(GruppierungsID, mitglied.id);
                if(m != null)
                    Mitglieder.Add(m);
            }
            return Mitglieder;
        }

        /// <summary>
        /// Wandelt eine Liste vom Typ NaMiObjekt in eine Liste vom Typ NaMiMitglied um
        /// </summary>
        /// <param name="objects">Liste von NaMiObjekten</param>
        /// <param name="GruppierungsID">Die Gruppierungs ID der NaMiObjekte</param>
        /// <returns></returns>
        public List<NaMiMitglied> GetFullList(List<NaMiObjekt> objects, int GruppierungsID)
        {
            if (!isAuthenticated)
                throw new NaMiNotLoggedInException();
            List<NaMiMitglied> Mitglieder = new List<NaMiMitglied>();
            foreach (NaMiObjekt mitglied in objects)
            {
                var m = GetMitglied(GruppierungsID, mitglied.id);
                if (m != null)
                    Mitglieder.Add(m);
            }
            return Mitglieder;
        }

        /// <summary>
        /// Updatet das Gewünschte Mitglied
        /// </summary>
        /// <param name="Mitglied">Das zu Updatene geänderte Mitglied</param>
        /// <returns></returns>
        public NaMiMitglied UpdateMitglied(NaMiMitglied Mitglied)
        {
            if (!isAuthenticated)
                throw new NaMiNotLoggedInException();
            RestRequest req = new RestRequest(Commands.Mitglied, Method.PUT);
            req.DateFormat = "yyyy-MM-ddTHH:mm:ss";

            req.RequestFormat = DataFormat.Json;
            req.AddUrlSegment("GID", Mitglied.gruppierungId.ToString());
            req.AddUrlSegment("MID", Mitglied.id.ToString());

            string s = Newtonsoft.Json.JsonConvert.SerializeObject(Mitglied, new Newtonsoft.Json.Converters.IsoDateTimeConverter());

            req.AddParameter("application/json; charset=utf-8", s, ParameterType.RequestBody);
            var resp = client.Execute<NaMiResponse<NaMiMitglied>>(req);
            if (!resp.Data.success)
                throw new NaMiException<NaMiMitglied>(resp.Data);
            return resp.Data.data;

        }


        public List<NaMiField> GetSearchableFields()
        {
            if (!isAuthenticated)
                throw new NaMiNotLoggedInException();
            NaMiMetaObjekt search = GetData<NaMiMetaObjekt>(Commands.SearchMeta);

            for (int i = 0; i < search.fields.Count; i++)
            {
                search.fields[i].UpdateAuswahl(this);
            }
            return search.fields;
        }

        /// <summary>
        /// Liefert eine Liste vom Typ NaMiObjekt unter der berücksichtigung eines Filters
        /// </summary>
        /// <param name="f">Der anzuwendene Filter</param>
        /// <param name="GruppierungsID">Die Gruppierungs ID der Gruppierung in welcher der Filter angewendet werden soll</param>
        /// <returns></returns>
        public List<NaMiObjekt> GetFilteredList(NaMiFilter f, int GruppierungsID)
        {
            if (!isAuthenticated)
                throw new NaMiNotLoggedInException();
            return GetFilteredList(f, GruppierungsID, "");
        }

        /// <summary>
        /// Liefert eine Liste vom Typ NaMiObjekt unter der berücksichtigung eines Filters
        /// </summary>
        /// <param name="f">Der anzuwendene Filter</param>
        /// <param name="GruppierungsID">Die Gruppierungs ID der Gruppierung in welcher der Filter angewendet werden soll</param>
        /// <param name="AdditionalText">Text nach dem gefiltert werden soll</param>
        /// <returns></returns>
        public List<NaMiObjekt> GetFilteredList(NaMiFilter f, int GruppierungsID, string AdditionalText)
        {
            if (!isAuthenticated)
                throw new NaMiNotLoggedInException();
            RestRequest req = new RestRequest(Commands.Filter, Method.GET);
            req.AddUrlSegment("GID", GruppierungsID.ToString());
            req.AddUrlSegment("FILTER", f.Command);
            req.AddUrlSegment("SEARCH", AdditionalText);
            var resp = client.Execute<NaMiResponse<List<NaMiObjekt>>>(req);
            if (!resp.Data.success)
                throw new NaMiException<List<NaMiObjekt>>(resp.Data);
            return resp.Data.data;
        }

        /// <summary>
        /// Führt eine Suche aus und gibt eine Liste vom Typ NaMiObjekt zurück
        /// </summary>
        /// <param name="SearchParameters">Parameter nach dennen gesucht werden soll</param>
        /// <returns></returns>
        public List<NaMiObjekt> PerformSearch(List<NaMiField> SearchParameters)
        {
            if (!isAuthenticated)
                throw new NaMiNotLoggedInException();
            string values = "";
            foreach (NaMiField f in SearchParameters)
            {
                if (f.search[0] != null)
                    if (f.multiSelect)
                    {
                        values = values + "\"" + f.name + "Id\":[";
                        foreach (string s in f.search)
                            values = values + "\"" + f.search[0] + "\",";
                        values = values.Substring(0, values.Length - 1);
                        values = values + "],";
                    }
                    else
                    {
                        values = values + "\"" + f.name + "\":\"" + f.search[0] + "\",";

                    }
            }
            values = values + "\"taetigkeitId\":[]";

            values = "{" + values + "}";
            RestRequest req = new RestRequest(Commands.Search, Method.GET);
            req.AddUrlSegment("VALUES", values);
            var resp = client.Execute<NaMiResponse<List<NaMiObjekt>>>(req);
            if (!resp.Data.success)
                throw new NaMiException<List<NaMiObjekt>>(resp.Data);
            return resp.Data.data;
        }

        /// <summary>
        /// Gibt eine List vom Typ NaMiObjekt zurück welche alle zugreifbaren Gruppierungen enthält
        /// </summary>
        /// <returns></returns>
        public List<NaMiObjekt> GetGruppierungen()
        {
            if (!isAuthenticated)
                throw new NaMiNotLoggedInException();
            return GetData<List<NaMiObjekt>>(Commands.Gruppierungen);
        }

        /// <summary>
        /// Gibt die Stammgruppierung des Mitglieds als NaMiObjekt zurück
        /// </summary>
        /// <returns></returns>
        public NaMiObjekt GetRootGruppierung()
        {
            if (!isAuthenticated)
                throw new NaMiNotLoggedInException();
            return GetData<List<NaMiObjekt>>(Commands.RootGruppierung)[0];
        }

        public List<NaMiTaetigkeit> GetTaetigkeiten(int MitgliedsID)
        {
            List<NaMiTaetigkeit> taetigkeiten = new List<NaMiTaetigkeit>();
            if (!isAuthenticated)
                throw new NaMiNotLoggedInException();
            RestRequest req = new RestRequest(Commands.Taetigkeiten, Method.GET);
            req.AddUrlSegment("MID", MitgliedsID.ToString());
            var resp = client.Execute<NaMiResponse<List<NaMiObjekt>>>(req);
            if (!resp.Data.success)
                throw new NaMiException<List<NaMiObjekt>>(resp.Data);
            foreach (NaMiObjekt t in resp.Data.data)
            {
                taetigkeiten.Add(GetTaetigkeit(MitgliedsID, t.id));
            }
            return taetigkeiten;
        }

        public NaMiTaetigkeit GetTaetigkeit(int MitgliedsID, int TaetigkeitsID)
        {
            if (!isAuthenticated)
                throw new NaMiNotLoggedInException();
            RestRequest req = new RestRequest(Commands.Taetigkeit, Method.GET);
            req.AddUrlSegment("MID", MitgliedsID.ToString());
            req.AddUrlSegment("TID", TaetigkeitsID.ToString());
            var resp = client.Execute<NaMiResponse<NaMiTaetigkeit>>(req);
            if (!resp.Data.success)
                throw new NaMiException<NaMiTaetigkeit>(resp.Data);
            return resp.Data.data;
        }

        public List<NaMiTaetigkeit> GetAktiveTaetigkeiten(int MitgliedsID)
        {
            List<NaMiTaetigkeit> aktiveTaetigkeiten = new List<NaMiTaetigkeit>();
            foreach (NaMiTaetigkeit t in GetTaetigkeiten(MitgliedsID))
            {
                if (t.aktivBis.Equals(new DateTime()))
                {
                    aktiveTaetigkeiten.Add(t);
                }
                else if (t.aktivBis > DateTime.Now)
                {
                    aktiveTaetigkeiten.Add(t);
                }
            }
            return aktiveTaetigkeiten;
        }

        public List<NaMiObjekt> GetMetaGeschlecht()
        {
            if (!isAuthenticated)
                throw new NaMiNotLoggedInException();
            RestRequest req = new RestRequest(Commands.Geschlecht, Method.GET);
            var resp = client.Execute<NaMiResponse<List<NaMiObjekt>>>(req);
            if (!resp.Data.success)
                throw new NaMiException<List<NaMiObjekt>>(resp.Data);
            return resp.Data.data;
        }
        public List<NaMiObjekt> GetMetaStaatsangehoerigkeit()
        {
            if (!isAuthenticated)
                throw new NaMiNotLoggedInException();
            RestRequest req = new RestRequest(Commands.Staatsangehörigkeit, Method.GET);
            var resp = client.Execute<NaMiResponse<List<NaMiObjekt>>>(req);
            if (!resp.Data.success)
                throw new NaMiException<List<NaMiObjekt>>(resp.Data);
            return resp.Data.data;
        }
        public List<NaMiObjekt> GetMetaKonfession()
        {
            if (!isAuthenticated)
                throw new NaMiNotLoggedInException();
            RestRequest req = new RestRequest(Commands.Konfession, Method.GET);
            var resp = client.Execute<NaMiResponse<List<NaMiObjekt>>>(req);
            if (!resp.Data.success)
                throw new NaMiException<List<NaMiObjekt>>(resp.Data);
            return resp.Data.data;
        }

        public List<NaMiObjekt> GetMetaBeitragsart(int GruppierungsID)
        {
            if (!isAuthenticated)
                throw new NaMiNotLoggedInException();
            RestRequest req = new RestRequest(Commands.Beitragsart, Method.GET);
            req.AddUrlSegment("GID", GruppierungsID.ToString());
            var resp = client.Execute<NaMiResponse<List<NaMiObjekt>>>(req);
            if (!resp.Data.success)
                throw new NaMiException<List<NaMiObjekt>>(resp.Data);
            return resp.Data.data;
        }

        public List<NaMiObjekt> GetMetaBundesland()
        {
            if (!isAuthenticated)
                throw new NaMiNotLoggedInException();
            RestRequest req = new RestRequest(Commands.Region, Method.GET);
            var resp = client.Execute<NaMiResponse<List<NaMiObjekt>>>(req);
            if (!resp.Data.success)
                throw new NaMiException<List<NaMiObjekt>>(resp.Data);
            return resp.Data.data;
        }

        public List<NaMiObjekt> GetMetaLand()
        {
            if (!isAuthenticated)
                throw new NaMiNotLoggedInException();
            RestRequest req = new RestRequest(Commands.Land, Method.GET);
            var resp = client.Execute<NaMiResponse<List<NaMiObjekt>>>(req);
            if (!resp.Data.success)
                throw new NaMiException<List<NaMiObjekt>>(resp.Data);
            return resp.Data.data;
        }

        public List<NaMiObjekt> GetMetaZahlungsart()
        {
            if (!isAuthenticated)
                throw new NaMiNotLoggedInException();
            RestRequest req = new RestRequest(Commands.ZahlungsKonditionen, Method.GET);
            var resp = client.Execute<NaMiResponse<List<NaMiObjekt>>>(req);
            if (!resp.Data.success)
                throw new NaMiException<List<NaMiObjekt>>(resp.Data);
            return resp.Data.data;
        }
    }
    public struct NaMiFilter
    {
        public string Command;
        public string Name;
        public bool NeedAditionalSearchString;
        public NaMiFilter(string FilterName, string Cmd, bool NeedSearchField)
        {
            Name = FilterName;
            Command = Cmd;
            NeedAditionalSearchString = NeedSearchField;
        }
    }
}
