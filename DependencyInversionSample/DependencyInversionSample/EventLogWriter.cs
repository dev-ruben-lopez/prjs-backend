using System;
using System.IO;

namespace DependencyInversionSample
{
    public class EventLogWriter
    {
        public void Write(string fileLocation, string sMessage)
        {
            if (!File.Exists(fileLocation))
                File.Create(fileLocation);

            File.AppendText(DateTime.Today.ToLocalTime().ToString() + " - " + sMessage);
        }
    }
}
