namespace CityInfo.API.Services
{
    public class LocalMailService : IMailService
    {
        private readonly string _mailTo = "admin@mycompany.com";
        private readonly string _mailFrom = "noreply@mycompany.com";

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
