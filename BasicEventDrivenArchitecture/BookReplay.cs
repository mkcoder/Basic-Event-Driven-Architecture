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
        private EventStore store;

        public BookReplay(List<TransactionEvent> book)
        {
            this.book = book;            
            store = new EventStore();
        }

        public EventStore ReplayAll()
        {
            foreach (var transactionEvent in book)
            {
                store.Transaction(transactionEvent.OldState,
                    transactionEvent.NewState.Amount - transactionEvent.OldState.Amount, transactionEvent.Transaction);
            }
            return store;
        }

        public EventStore Replay(int upto)
        {
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
            foreach (var transactionEvent in book)
            {
                if ( transactionEvent.NewState.CustomerId==userId)
                    store.Transaction(transactionEvent.OldState,
                        transactionEvent.NewState.Amount - transactionEvent.OldState.Amount, transactionEvent.Transaction);
            }
            return store;
        }
    }
}
