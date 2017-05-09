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

        public Customer Transaction(Customer customer, decimal amount, Transction transction)
        {
            Customer action = null;
            switch (transction)
            {
                case Transction.Withdraw:
                    action = Withdraw(customer, amount);
                    return action;
                    break;
                case Transction.Deposit:
                    action = Deposit(customer, amount);
                    return action;
                    break;
            }
            return action;            
        }
    }
}