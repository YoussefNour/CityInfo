namespace CityInfo.API.Services
{
    public class CloudMailService : IMailService
    {
        private readonly string _mailTo = string.Empty;
        private readonly string _mailFrom = string.Empty;

        public CloudMailService(IConfiguration configuration)
        {
            _mailTo = configuration["mailSettings:mailToAddress"];
            _mailFrom = configuration["mailSettings:mailFromAddress"];
        }

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
