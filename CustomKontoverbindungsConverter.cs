using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
namespace NaMiLib
{
    class CustomKontoverbindungsConverter : Newtonsoft.Json.JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return (objectType == typeof(NaMiKontoverbindung));
        }

        public override void WriteJson(JsonWriter writer, object value, Newtonsoft.Json.JsonSerializer serializer)
        {
            NaMiKontoverbindung data = (NaMiKontoverbindung)value;
            string s;
            if (data.kontoinhaber != null)
                data.kontoinhaber = data.kontoinhaber.ToString().Replace("Ü", "Ue").Replace("ü", "ue").Replace("Ö", "Oe").Replace("ö", "oe").Replace("Ä", "Ae").Replace("ä", "ae").Replace("ß", "ss").Replace("-", " ");
            if (data.institut != null)
                data.institut = data.institut.ToString().Replace("Ü", "Ue").Replace("ü", "ue").Replace("Ö", "Oe").Replace("ö", "oe").Replace("Ä", "Ae").Replace("ä", "ae").Replace("ß", "ss").Replace("-", " ");

            if (data.zahlungsKonditionId == null)
                s = "\"{\\\"id\\\":\\\"" + data.id + "\\\",\\\"zahlungsKonditionId\\\":null,\\\"mitgliedsNummer\\\":" + data.mitgliedsNummer + ",\\\"institut\\\":\\\"" + data.institut + "\\\",\\\"kontoinhaber\\\":\\\"" + data.kontoinhaber + "\\\",\\\"kontonummer\\\":\\\"" + data.kontonummer + "\\\",\\\"bankleitzahl\\\":\\\"" + data.bankleitzahl + "\\\",\\\"iban\\\":\\\"" + data.iban + "\\\",\\\"bic\\\":\\\"" + data.bic + "\\\"}\"";
            //writer.WriteRawValue("\"{\"id\\\":\"" + data.id + "\",\"zahlungsKonditionId\":null,\"mitgliedsNummer\":" + data.mitgliedsNummer + ",\"institut\":\"" + data.institut + "\",\"kontoinhaber\":\"" + data.kontoinhaber + "\",\"kontonummer\":\"" + data.kontonummer + "\",\"bankleitzahl\":\"" + data.bankleitzahl + "\",\"iban\":\"" + data.iban + "\",\"bic\":\"" + data.bic + "\"}\"");
            else
                s = "\"{\\\"id\\\":\\\"" + data.id + "\\\",\\\"zahlungsKonditionId\\\":" + data.zahlungsKonditionId + ",\\\"mitgliedsNummer\\\":" + data.mitgliedsNummer + ",\\\"institut\\\":\\\"" + data.institut + "\\\",\\\"kontoinhaber\\\":\\\"" + data.kontoinhaber + "\\\",\\\"kontonummer\\\":\\\"" + data.kontonummer + "\\\",\\\"bankleitzahl\\\":\\\"" + data.bankleitzahl + "\\\",\\\"iban\\\":\\\"" + data.iban + "\\\",\\\"bic\\\":\\\"" + data.bic + "\\\"}\"";
            //writer.WriteRawValue("\"{\"id\\\":\"" + data.id + "\",\"zahlungsKonditionId\":" + data.zahlungsKonditionId + ",\"mitgliedsNummer\":" + data.mitgliedsNummer + ",\"institut\":\"" + data.institut + "\",\"kontoinhaber\":\"" + data.kontoinhaber + "\",\"kontonummer\":\"" + data.kontonummer + "\",\"bankleitzahl\":\"" + data.bankleitzahl + "\",\"iban\":\"" + data.iban + "\",\"bic\":\"" + data.bic + "\"}\"");

            writer.WriteRawValue(s);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            return serializer.Deserialize(reader, objectType);
        }
    }
}
