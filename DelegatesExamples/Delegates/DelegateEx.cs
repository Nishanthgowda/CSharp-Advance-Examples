using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DelegatesExamples.Delegates
{
    public class DelegateEx
    {
        public delegate void Logdel(string text);
        public Logdel _logdel { get; set; }

        public void callingDelegateMethods()
        {
            Console.WriteLine("Please Enter Your Name : ");
            string name = Console.ReadLine();



            //Logdel logdel = new Logdel(LogTextToFile);


            // writing using lamda expression
            //logdel d = (text) =>
            //{
            //    Console.WriteLine($"{DateTime.Now} : {text}");
            //};

            //calling multiple delegates

            Logdel logconsole, logfile;
            logconsole = new Logdel(LogTextConsole);
            logfile = new Logdel(LogTextToFile);

            Logdel multiDelcall = logconsole + logfile;

            //  multiDelcall(name);

            // using delegate as parameter

            PassingDelAsParameter(multiDelcall, name);


            Console.ReadLine();
        }
        public static void PassingDelAsParameter(Logdel logdel, string text)
        {
            logdel(text);
        }
        public static void LogTextConsole(string text)
        {
            Console.WriteLine($"{DateTime.Now} : {text}");
        }
        public static void LogTextToFile(string text)
        {
            using (StreamWriter sw = new StreamWriter(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "xyz.txt"), true))
            {
                sw.WriteLine($"{DateTime.Now} : {text}");
            }
            // File.WriteAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "xyz.txt"), $"{DateTime.Now} : {text}");
        }
    }

}
