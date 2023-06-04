namespace CityInfo.API.Services
{
    public class LocalMailService : IMailService
    {
        private readonly string _mailTo = string.Empty;
        private readonly string _mailFrom = string.Empty;

        public LocalMailService(IConfiguration configuration)
        {
            _mailTo = configuration["mailSettings:mailToAddress"];
            _mailFrom = configuration["mailSettings:mailFromAddress"];
        }

        public void Send(string Subject, string Body)
        {
            Console.WriteLine(
                $"mail form {_mailFrom} to {_mailTo}, with {nameof(LocalMailService)}."
            );
            Console.WriteLine($"Subject:{Subject}");
            Console.WriteLine($"Body:{Body}");
        }
    }
}
