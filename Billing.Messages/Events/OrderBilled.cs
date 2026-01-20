namespace Billing.Messages.Events
{
    public class OrderBilled : IEvent
    {
        public string OrderId { get; set; }
    }
}
