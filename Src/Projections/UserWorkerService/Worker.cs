using Common.Constants;
using Common.Events.UserEvent;
using Common.Infrastructure;

namespace UserWorkerService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IConfiguration configuration;

        public Worker(ILogger<Worker> logger, IConfiguration configuration)
        {
            _logger = logger;
            this.configuration = configuration;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var connectionString = configuration["SqlServerConnectionString"];

            var userService = new Services.UserService(connectionString);
            var emailService = new Services.EmailService();

            QueryFactory.CreateBasicConsumer()
                 .EnsureExchange(CommonConstants.UserExchangeName)
                 .EnsureQueue(CommonConstants.UserEmailChangedQueueName, CommonConstants.UserExchangeName)
                 .Receive<UserEmailChangedEvent>(async user =>
                 {
                     var emailConfirmationId = await userService.CreateEmailConfirmation(user);

                     var emailConfirmationLink = emailService.GenerateConfirmationLink(emailConfirmationId, configuration["EmailConfirmationLink"]);

                     emailService.SendEmail(user.NewEmailAddress, emailConfirmationLink).GetAwaiter().GetResult();
                 })
                 .StartConsuming(CommonConstants.UserEmailChangedQueueName);
        }
    }
}