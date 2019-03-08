using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NaMiLib
{
    public class NaMiException<DataT> : Exception
    {
        private NaMiResponse<DataT> _resp;
        public NaMiException(NaMiResponse<DataT> resp)
        {
            _resp = resp;
        }

        public override string Message
        {
            get
            {
                return _resp.title + ": " + _resp.message;
            }
        }
        public NaMiResponse<DataT> Response
        {
            get
            {
                return _resp;
            }
        }
    }
}
