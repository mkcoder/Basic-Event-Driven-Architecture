using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicEventDrivenArchitecture
{
    public class TransactionEventBook
    {
        public List<TransactionEvent> Book { get; }

        public TransactionEventBook(EventStore eventStore)
        {
            eventStore.Changed += new LogTransactionEvent(AddTransctionToBook);
            Book = new List<TransactionEvent>();
        }

        private void AddTransctionToBook(TransactionEvent t, EventArgs e)
        {
            Console.WriteLine($"{DateTime.Now} [{nameof(TransactionEventBook)}]: Adding the following transcation ({t}) to the Book .");
            Book.Add(t);
        }
    }
}
