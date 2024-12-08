namespace queueapp1.Server.Consumers
{
    using MassTransit;
    using queueapp1.Server.Contracts;
    using queueapp1.Server.Data;
    using queueapp1.Server.Models;

    public class QueueConsumer : IConsumer<MessageQueue>
    {
        readonly ILogger<QueueConsumer> _logger;
        protected AppDbContext _appDbContext;

        public QueueConsumer(
            ILogger<QueueConsumer> logger,
            AppDbContext appDbContext)
        {
            _logger = logger;
            _appDbContext = appDbContext;
        }

        public Task Consume(ConsumeContext<MessageQueue> context)
        {
            _logger.LogInformation("{Value}", context.Message.Value);

            var dbSet = _appDbContext.Set<Message>();
            var entity = new Message
            {
                Value = context.Message.Value,
                Status = "DONE",
                CreatedDateTime = DateTime.UtcNow,
            };

            dbSet.Add(entity);
            _appDbContext.SaveChanges();

            return Task.CompletedTask;
        }
    }
}