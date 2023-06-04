using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfo.API.Services
{
    public class CloudMailService: IMailService
    {
        private readonly string _mailTo = "admin@mycompany.com";
        private readonly string _mailFrom = "noreply@mycompany.com";

        public void Send(string Subject, string Body)
        {
            Console.WriteLine(
                $"mail form {_mailFrom} to {_mailTo}, with {nameof(CloudMailService)}."
            );
            Console.WriteLine($"Subject:{Subject}");
            Console.WriteLine($"Body:{Body}");
        }
    }
}
