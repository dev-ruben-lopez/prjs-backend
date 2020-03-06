using System;
namespace DependencyInversionSample
{

    public class AppPoolWriter
    {
        EventLogWriter writer = null;
        private string sLocation = "";

        public AppPoolWriter()
        {
        }

        public bool Notify (string sMessage)
        {
            if (writer == null)
                writer = new EventLogWriter();

            writer.Write(sLocation, sMessage);

            return true;
        }
    }
}
