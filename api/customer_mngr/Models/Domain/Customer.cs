namespace customer_mngr.Models.Domain
{
    public class Customer
    {
        public Guid id { get; set; }
        public string name { get; set; }
        public required string email { get; set; }
        public required string address { get; set; }
        public required string phone { get; set; }
    }
}
