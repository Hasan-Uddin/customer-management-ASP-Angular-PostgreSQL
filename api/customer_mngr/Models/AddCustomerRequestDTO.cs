namespace customer_mngr.Models
{
    public class AddCustomerRequestDTO
    {
        public string name { get; set; }
        public required string email { get; set; }
        public required string address { get; set; }
        public required string phone { get; set; }
    }
}
