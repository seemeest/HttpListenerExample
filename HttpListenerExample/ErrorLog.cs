using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpListenerExample
{
    public static class ErrorLog
    {
        
        public static void WriteError(string Error){

            Console.ForegroundColor = ConsoleColor.Green;

            Console.WriteLine(DateTime.Now.ToString());

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(Error);
            Console.ResetColor(); 
        }
    }
}
