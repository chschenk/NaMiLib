using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NaMiLib
{
    public class NaMiNotLoggedInException : Exception
    {
        private string _message = "ERROR: Es ist niemand angemeldet! Bitte melden Sie sich an bevor Sie auf Daten zugreifen!";

        public override string Message
        {
            get
            {
                return _message;
            }
        }
    }
}
