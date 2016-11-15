using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEOLogParser
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                args = new string[]
                {
                    Directory.GetCurrentDirectory() + "\\downloadedLog.txt"
                };
            }

            byte[] bytes = File.ReadAllBytes(args[0]);
            LogParser parser = new LogParser(bytes);
            parser.WriteToFile();
        }
    }
}
