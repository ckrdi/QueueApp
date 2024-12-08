namespace queueapp1.Server.Models
{
    public class Message
    {
        public int Id { get; set; }
        public string? Value { get; set; }
        public string? Status { get; set; }
        public DateTime CreatedDateTime { get; set; }
    }
}
