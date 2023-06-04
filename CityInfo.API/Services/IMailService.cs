namespace CityInfo.API.Services;

public interface IMailService
{
    void Send(string Subject, string Body);
}
