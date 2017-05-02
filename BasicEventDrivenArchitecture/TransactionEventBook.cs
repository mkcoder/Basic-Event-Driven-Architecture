using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicEventDrivenArchitecture
{
    public class TransactionEventBook
    {
        private EventStore _eventStore;
        public List<TransactionEvent> Book { get; set; }= new List<TransactionEvent>();

        public TransactionEventBook(EventStore eventStore)
        {
            _eventStore = eventStore;
            _eventStore.Changed += new LogTransactionEvent(AddTransctionToBook);
        }

        private void AddTransctionToBook(TransactionEvent t, EventArgs e)
        {
            Console.WriteLine($"{DateTime.Now} [{nameof(TransactionEventBook)}]: Adding the following transcation ({t}) to the Book .");
            Book.Add(t);
        }
    }
}
