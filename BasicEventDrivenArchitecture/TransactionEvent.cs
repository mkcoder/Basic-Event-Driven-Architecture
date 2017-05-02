namespace BasicEventDrivenArchitecture
{
    public class TransactionEvent
    {
        public Customer OldState { get; private set; }
        public Customer NewState { get; private set; }
        public Transction Transaction { get; private set; }

        public TransactionEvent(Customer newState, Customer oldState, Transction transaction)
        {
            this.NewState = newState;
            this.OldState = oldState;
            this.Transaction = transaction;
        }

        public override string ToString()
        {
            return $"Old State: {OldState} \n" +
                   $"New State: {NewState}\n" +
                   $"Transaction: {Transaction}";
        }
    }
}