using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NaMiLib
{
    static class Log
    {
        public static void Write(string Message)
        {
            Console.WriteLine("<log>" + Message + "</log>");
        }
    }
}
