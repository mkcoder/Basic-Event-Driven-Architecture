using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicEventDrivenArchitecture
{
    class Program
    {
        static void Main(string[] args)
        {
            Customer bob = new Customer("Bob");
            Customer jane = new Customer("Jane");
            EventStore eventStore = new EventStore();
            TransactionEventBook TEB = new TransactionEventBook(eventStore);
            bob = eventStore.Transaction(bob, 1.0m, Transction.Deposit);
            bob = eventStore.Transaction(bob, 2.0m, Transction.Deposit);
            bob = eventStore.Transaction(bob, 3.0m, Transction.Deposit);
            bob = eventStore.Transaction(bob, 4.0m, Transction.Deposit);
            jane = eventStore.Transaction(jane, 100m, Transction.Deposit);
            bob = eventStore.Transaction(bob, 1000m, Transction.Deposit);
            bob = eventStore.Transaction(bob, 100m, Transction.Withdraw);
            jane = eventStore.Transaction(jane, 45m, Transction.Deposit);
            jane = eventStore.Transaction(jane, 5m, Transction.Withdraw);
            BookReplay br = new BookReplay(TEB.Book);
            Console.Clear();
            Console.WriteLine("Recreating the EventStore for all the transaction");
            br.ReplayAll();
            Console.Clear();
            Console.WriteLine("Recreating the EventStore for all the transaction upto 2");
            br.Replay(2);
            Console.Clear();
            String userId = "Bob";
            Console.Clear();
            Console.WriteLine($"Recreating the EventStore for {userId}");
            br.ReplayByUser(userId);
        }
    }
}
