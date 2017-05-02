namespace BasicEventDrivenArchitecture
{
    public class Customer
    {
        public decimal Amount { get; set; }
        public string CustomerId { get; set; }
        

        public Customer(string CustomerId)
        {
            this.CustomerId = CustomerId;
            this.Amount = 0.0m;
        }

        public Customer(Customer customer)
        {
            this.CustomerId = customer.CustomerId;
            this.Amount = customer.Amount;
        }

        public override string ToString()
        {
            return $"UserName: {CustomerId} Amount: {Amount}";
        }
    }
}