using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEOLogParser
{
    class LogDataPoint
    {
        public static string Separator = ";";

        public int Time;
        public int Step;
        public int Voltage;
        public int Current;
        public int Temperature;

        public override string ToString()
        {
            return Time + Separator + 
                Step + Separator + 
                Voltage + Separator + 
                Current + Separator + 
                Temperature;
        }

        public static LogDataPoint CreateLogDataPoint(byte[] bytes)
        {
            if (bytes.Length != 6)
                return null;

            LogDataPoint dataPoint = new LogDataPoint();

            //Word 0 - voltage and temperature
            dataPoint.Voltage = ((bytes[0] & 0x01) << 8) | bytes[1];
            dataPoint.Voltage *= 100; //Because mV
            dataPoint.Temperature = bytes[0] >> 1;

            //Word 1 - current and step
            dataPoint.Current = bytes[3];
            dataPoint.Current *= 100; //Beucase mA
            dataPoint.Step = bytes[2];

            //Word 2 - time
            dataPoint.Time = (bytes[4] << 8) | bytes[5];

            return dataPoint;
        }
    }
}
