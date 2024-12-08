namespace queueapp1.Server.Controllers
{
    using MassTransit;
    using Microsoft.AspNetCore.Cors;
    using Microsoft.AspNetCore.Mvc;
    using queueapp1.Server.Contracts;
    using queueapp1.Server.Controllers.Models;
    using queueapp1.Server.Data;
    using queueapp1.Server.Models;

    [ApiController]
    [Route("[controller]")]
    public class QueueController : ControllerBase
    {
        private readonly IBus _bus;
        protected AppDbContext _appDbContext;

        public QueueController(
            IBus bus,
            AppDbContext appDbContext)
        {
            _bus = bus;
            _appDbContext = appDbContext;
        }

        [EnableCors("AllowPolicy")]
        [HttpGet(Name = "GetQueue")]
        public IEnumerable<Message> Get()
        {
            var dbSet = _appDbContext.Set<Message>();
            var queryable = dbSet.AsQueryable();

            return queryable.OrderBy(item => item.CreatedDateTime).ToArray();
        }

        [EnableCors("AllowPolicy")]
        [HttpPost(Name = "CreateQueue")]
        public async Task<bool> Create(CreateModel model)
        {
            // send queue manually
            await _bus.Publish(new MessageQueue { Value = model.Value });
            return true;
        }
    }
}
