namespace BasicEventDrivenArchitecture
{
    public class TransactionEvent
    {
        public Customer OldState { get; }
        public Customer NewState { get; }
        public Transction Transaction { get; }

        public TransactionEvent(Customer newState, Customer oldState, Transction transaction)
        {
            NewState = newState;
            OldState = oldState;
            Transaction = transaction;
        }

        public override string ToString()
        {
            return $"Old State: {OldState} \n" +
                   $"New State: {NewState}\n" +
                   $"Transaction: {Transaction}";
        }
    }
}