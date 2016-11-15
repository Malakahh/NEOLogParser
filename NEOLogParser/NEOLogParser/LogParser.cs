using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEOLogParser
{
    class LogParser
    {
        byte[] loadedBytes;

        List<LogDataPoint> dataPoints;

        public LogParser(byte[] log)
        {
            loadedBytes = log;

            if (!ValidateLogSize())
            {
                Console.WriteLine("Log size is wrong - Constructor");
                return;
            }

            ParseDataPoints();
        }

        private void ParseDataPoints()
        {
            if (!ValidateLogSize())
            {
                Console.WriteLine("Log size is wrong - ParseDataPoints");
                return;
            }

            dataPoints = new List<LogDataPoint>();

            List<byte> bytes = new List<byte>();

            for (int i = 0; i < loadedBytes.Length; i++)
            {
                bytes.Add(loadedBytes[i]);

                if (bytes.Count == 6)
                {
                    dataPoints.Add(LogDataPoint.CreateLogDataPoint(bytes.ToArray()));
                    bytes.Clear();
                }
            }
        }

        private bool ValidateLogSize()
        {
            //return (loadedBytes.Length - 2) % 6 == 0;
            return loadedBytes.Length % 6 == 0;
        }

        public void WriteToFile()
        {
            string s = "TIME" + LogDataPoint.Separator + "STEP" + LogDataPoint.Separator + "VOLTAGE" + LogDataPoint.Separator + "CURRENT" + LogDataPoint.Separator + "TEMP\n";

            for (int i = 0; i < dataPoints.Count; i++)
            {
                s += dataPoints[i].ToString() + "\n";
            }

            File.WriteAllText(Directory.GetCurrentDirectory() + "\\ConvertedLog.txt", s);
        }
    }
}
