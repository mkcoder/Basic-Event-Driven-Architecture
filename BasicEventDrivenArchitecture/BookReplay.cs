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

        public BookReplay(List<TransactionEvent> book)
        {
            this.book = book;
        }

        public EventStore ReplayAll()
        {
            EventStore store = new EventStore();
            foreach (var transactionEvent in book)
            {
                store.Transaction(transactionEvent.OldState,
                    transactionEvent.NewState.Amount - transactionEvent.OldState.Amount, transactionEvent.Transaction);
            }
            return store;
        }

        public EventStore Replay(int upto)
        {
            EventStore store = new EventStore();
            for (int i = 0; i < upto; i++)
            {
                var transactionEvent = book[i];
                store.Transaction(transactionEvent.OldState,
                       transactionEvent.NewState.Amount - transactionEvent.OldState.Amount, transactionEvent.Transaction);
            }
            return store;
        }

        public EventStore ReplayByUser(string userId)
        {
            EventStore store = new EventStore();
            foreach (var transactionEvent in book)
            {
                if ( transactionEvent.NewState.CustomerId==userId)
                    store.Transaction(transactionEvent.OldState,
                        Math.Abs(transactionEvent.NewState.Amount - transactionEvent.OldState.Amount), transactionEvent.Transaction);
            }
            return store;
        }
    }
}
