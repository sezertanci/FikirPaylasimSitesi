using Common;
using Common.Constants;
using Common.Events.UserEvent;
using Common.Infrastructure;

namespace UserWorkerService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var connectionString = Configuration.SqlServerConnectionString;

            var userService = new Services.UserService();
            var emailService = new Services.EmailService();

            QueryFactory.CreateBasicConsumer()
                 .EnsureExchange(CommonConstants.UserExchangeName)
                 .EnsureQueue(CommonConstants.UserEmailChangedQueueName, CommonConstants.UserExchangeName)
                 .Receive<UserEmailChangedEvent>(async user =>
                 {
                     var emailConfirmationId = await userService.CreateEmailConfirmation(user);

                     var emailConfirmationLink = emailService.GenerateConfirmationLink(emailConfirmationId, Configuration.EmailConfirmationLink);

                     emailService.SendEmail(user.NewEmailAddress, emailConfirmationLink).GetAwaiter().GetResult();
                 })
                 .StartConsuming(CommonConstants.UserEmailChangedQueueName);
        }
    }
}