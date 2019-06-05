using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfo.API.Services
{
    public class CloudMailService : IMailService
    {
        public string mailFrom = Startup.Configuration["mailSettings:mailFrom"];
        public string mailTo = Startup.Configuration["mailSettings:mailTo"];


        public void Send(string subject, string content)
        {
            Console.WriteLine("********NEW CLOUD EMAIL !!********");
            Console.WriteLine(mailFrom);
            Console.WriteLine(subject);
            Console.WriteLine(content);
            Console.WriteLine("********END********");
        }
    }

}
