namespace Brokerless.Interfaces.Services
{
    public interface IEmailService
    {
        public Task SendEmail(string receiverEmail, string subject, string message);
    }
}
