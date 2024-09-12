using Azure;
using Azure.Communication.Email;
using Brokerless.Interfaces.Services;

namespace Brokerless.Services
{
    public class EmailService:IEmailService
    {
        private readonly string _azureConnectionString;
        private readonly string _fromEmail;

        public EmailService(IConfiguration configuration)
        {
            _azureConnectionString = configuration.GetValue<string>("EmailService:ConnectionString");
            _fromEmail = configuration.GetValue<string>("EmailService:FromEmail");
        }

        public async Task SendEmail(string receiverEmail, string subject, string message)
        {
            try
            {
                string connectionString = _azureConnectionString;
                var emailClient = new EmailClient(connectionString);

                _ = Task.Run(async () =>
                {
                    try
                    {
                        EmailSendOperation emailSendOperation = await emailClient.SendAsync(
                            WaitUntil.Started,
                            senderAddress: _fromEmail,
                            recipientAddress: receiverEmail,
                            subject: subject,
                            htmlContent: null,
                            plainTextContent: message
                        );
                    }
                    catch (Exception ex)
                    {
                        await Console.Out.WriteLineAsync("Failed to send email: " + ex.Message);
                    }
                });
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync("Failed to initiate email send operation: " + ex.Message);
            }
        }
    }
}
