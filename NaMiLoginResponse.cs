using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.IO;

namespace NaMiLib
{

    class NaMiLoginResponse
    {

        public object servicePrefix { get; set; }
        public object methodCall { get; set; }
        public object response { get; set; }
        public int statusCode { get; set; }
        public string statusMessage { get; set; }
        public string apiSessionName { get; set; }
        public string apiSessionToken { get; set; }
        public int minorNumber { get; set; }
        public int majorNumber { get; set; }

         public static NaMiLoginResponse CreateResponse(string jsonData)
         {
             Log.Write("Creating LoginResponse From " + jsonData);
             DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(NaMiLoginResponse));
             MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(jsonData));
             return (NaMiLoginResponse)ser.ReadObject(stream);
         }
    }
}
