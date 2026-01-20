namespace Shipping.Policies.Shipping
{
    public class SagaData : ContainSagaData
    {
        public string OrderId { get; set; }
        public bool Placed { get; set; }
        public bool Billed { get; set; }
    }
}
