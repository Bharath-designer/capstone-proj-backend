using System.Text;
using System.Text.Json.Serialization;
using Brokerless.Context;
using Brokerless.Interfaces.Repositories;
using Brokerless.Interfaces.Services;
using Brokerless.Repositories;
using Brokerless.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Brokerless
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder
                .Services.AddControllers(options =>
                {
                    options.Filters.Add(new AuthorizeFilter());
                })
                .ConfigureApiBehaviorOptions(options =>
                    options.SuppressModelStateInvalidFilter = true
                )
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                });

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["TokenKey:JWT"]))
                    };
                });

            builder.Services.AddDbContext<BrokerlessDBContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("default"));
            });

            #region Services
            builder.Services.AddScoped(typeof(IAuthService), typeof(AuthService));
            builder.Services.AddScoped(typeof(IUserService), typeof(UserService));
            builder.Services.AddScoped(typeof(ITokenService), typeof(TokenService));
            builder.Services.AddScoped(typeof(ISubscriptionService), typeof(SubscriptionService));
            builder.Services.AddScoped(typeof(IPaymentService), typeof(PaymentService));
            builder.Services.AddScoped(typeof(IPropertyService), typeof(PropertyService));
            #endregion


            #region Repositories
            builder.Services.AddScoped(typeof(IUserRepository), typeof(UserRepository));
            builder.Services.AddScoped(typeof(ISubscriptionTemplateRepository), typeof(SubscriptionTemplateRepository));
            builder.Services.AddScoped(typeof(ITransactionRepository), typeof(TransactionRepository));
            builder.Services.AddScoped(typeof(ITagRepository), typeof(TagRepository));
            builder.Services.AddScoped(typeof(IPropertyRepository), typeof(PropertyRepository));
            #endregion

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigin", builder =>
                {
                    builder.AllowAnyOrigin()
                           .AllowAnyMethod()
                           .AllowAnyHeader();
                });
            });


            var app = builder.Build();

            app.UseCors("AllowSpecificOrigin");

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
