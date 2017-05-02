namespace BasicEventDrivenArchitecture
{
    internal class AccountSystem
    {
        public Customer Withdraw(Customer customer, decimal amount)
        {
            Customer c = new Customer(customer);
            if ( c.Amount - amount >= 0)
                c.Amount -= amount;
            customer = c;
            return c;
        }

        public Customer Deposit(Customer customer, decimal amount)
        {
            Customer c = new Customer(customer);
            c.Amount += amount;
            return c;
        }
    }
}