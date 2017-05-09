using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicEventDrivenArchitecture
{
    public class BookReplay
    {
        private List<TransactionEvent> book;
        private readonly EventStore _store = new EventStore();

        public BookReplay(List<TransactionEvent> book)
        {
            this.book = book;
        }

        public IEnumerable<Customer> ReplayAll()
        {
            foreach (var groupedCustomerBook in book.GroupBy(_ => _.NewState.CustomerId).Select((k,v) => new { cId = k.Key, events = k.ToList()}))
            {
                Customer c = null;
                foreach (var transactionEvent in groupedCustomerBook.events)
                {
                    c = _store.Transaction(transactionEvent.OldState,
                    Math.Abs(transactionEvent.NewState.Amount - transactionEvent.OldState.Amount), transactionEvent.Transaction);
                }
                yield return c;
            }
        }

        public IEnumerable<Customer> Replay(int upto)
        {
            Customer c = null;
            for (int i = 0; i < upto; i++)
            {
                var transactionEvent = book[i];
                c = _store.Transaction(transactionEvent.OldState,
                       Math.Abs(transactionEvent.NewState.Amount - transactionEvent.OldState.Amount), transactionEvent.Transaction);
            }
            yield return c;
        }

        public Customer ReplayByUser(string userId)
        {
            Customer c = null;
            foreach (var transactionEvent in book.Where(b => b.NewState.CustomerId==userId))
            {
                c = _store.Transaction(transactionEvent.OldState,
                    Math.Abs(transactionEvent.NewState.Amount - transactionEvent.OldState.Amount), transactionEvent.Transaction);
            }
            return c;
        }
    }
}
