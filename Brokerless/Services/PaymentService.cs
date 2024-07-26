using System.Text;
using System.Text.Json;
using Brokerless.Context;
using Brokerless.DTOs.Payment;
using Brokerless.Enums;
using Brokerless.Exceptions;
using Brokerless.Interfaces.Repositories;
using Brokerless.Interfaces.Services;
using Brokerless.Models;
using Microsoft.EntityFrameworkCore;

namespace Brokerless.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly BrokerlessDBContext _context;
        private readonly IUserService _userService;

        private string PAYMENT_API_KEY { get; }
        private string PAYMENT_URL { get; }
        private string WEBHOOK_SECRET_KEY { get; }

        public PaymentService(IConfiguration configuration, 
            ITransactionRepository transactionRepository, 
            BrokerlessDBContext context,
            IUserService userService
            )
        {


            _transactionRepository = transactionRepository;
            _context = context;
            _userService = userService;

            string api_key = configuration.GetSection("PaymentCredentials:API_KEY").Value;
            PAYMENT_API_KEY = api_key != null ? api_key.ToString() : throw new NullReferenceException("Cannot get Payment API_KEY from Configuration file");

            string value = configuration.GetSection("PaymentCredentials:PAYMENT_URL").Value;
            PAYMENT_URL = value != null ? value.ToString() : throw new NullReferenceException("Cannot get Payment URL from Configuration file");

            var WebHookSecretKey = configuration.GetSection("WebhookCredentials").GetSection("KEY").Value;
            WEBHOOK_SECRET_KEY = WebHookSecretKey != null ? WebHookSecretKey.ToString() : throw new NullReferenceException("Cannot get Webhook credentials from Configuration file");

        }


        public async Task<MakePaymentReturnDTO> InitializeTransactionForSubscription(int userId, SubscriptionTemplate subscriptionTemplate)
        {
            var paymentData = new
            {
                Amount = subscriptionTemplate.Price,
                Currency = subscriptionTemplate.Currency.ToString()
            };

            var jsonPayload = JsonSerializer.Serialize(paymentData);
            var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");
            HttpClient httpClient = new HttpClient();

            httpClient.DefaultRequestHeaders.Add("API_KEY", PAYMENT_API_KEY);

            var response = await httpClient.PostAsync(PAYMENT_URL, content);

            if (response.StatusCode.ToString() == "Unauthorized")
            {
                throw new PaymentGatewayUnauthorizedException();
            }

            response.EnsureSuccessStatusCode();


            string responseContent = await response.Content.ReadAsStringAsync();
            PaymentResponseDTO? paymentResult = JsonSerializer.Deserialize<PaymentResponseDTO>(responseContent);

            if (paymentResult == null)
            {
                throw new NullReferenceException("Payment response is not deserializable");
            }

            Transaction transaction = new Transaction
            {
                Amount = (double)subscriptionTemplate.Price,
                Currency = (Currency)subscriptionTemplate.Currency,
                TransactionId = paymentResult.TransactionId,
                UserId = userId,
                SubscriptionTemplateName = subscriptionTemplate.SubsriptionName,
                ExpiresOn = DateTimeOffset.FromUnixTimeMilliseconds((long)paymentResult.PaymentExpiresEpoch).LocalDateTime
            };

            await _transactionRepository.Add(transaction);

            MakePaymentReturnDTO makePaymentReturnDTO = new MakePaymentReturnDTO
            {
                PaymentUrl = paymentResult.PaymentURL
            };

            return makePaymentReturnDTO;
        }

        public async Task UpdateTransactionDetails(PaymentNotificationDTO paymentNotificationDTO, IHeaderDictionary headers)
        {
            string? KeyFromPaymentGateway = headers["SECRET_KEY"];

            if (KeyFromPaymentGateway != WEBHOOK_SECRET_KEY)
            {
                throw new UnauthorizedAccessException("Webhook Request Key is Invalid");
            }
            Transaction transaction = await _transactionRepository.GetTransactionWithAllNavProperties(paymentNotificationDTO.TransactionId);
            if (transaction == null)
            {
                throw new NoTransactionFoundException();
            }

            using (var DBTransaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    transaction.TransactionStatus = paymentNotificationDTO.PaymentStatus;

                    if (transaction.TransactionStatus == TransactionStatus.Completed)
                    {
                        User user = transaction.User;
                        SubscriptionTemplate subscriptionTemplate = transaction.SubscriptionTemplate;
                        await _userService.ActivateSubscription(user, subscriptionTemplate);
                        
                    }

                    await _transactionRepository.Update(transaction);
                    await DBTransaction.CommitAsync();

                }

                catch (Exception ex)
                {
                    await Console.Out.WriteLineAsync(ex.Message);
                }

            }

        }
    }
}

