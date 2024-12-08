namespace queueapp1.Server
{
    using MassTransit;
    using Microsoft.Extensions.Hosting;
    using queueapp1.Server.Contracts;

    public class Worker : BackgroundService
    {
        readonly IBus _bus;

        public Worker(IBus bus)
        {
            _bus = bus;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            // produce queue every one minute
            while (!stoppingToken.IsCancellationRequested)
            {
                await _bus.Publish(new MessageQueue{ Value = "Hello World" }, stoppingToken);
                await Task.Delay(60000, stoppingToken);
            }
        }
    }
}
