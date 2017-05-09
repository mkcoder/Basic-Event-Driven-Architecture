using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
using System.Text;
using System.Threading.Tasks;

namespace BasicEventDrivenArchitecture
{
    public delegate void LogTransactionEvent(TransactionEvent t, EventArgs e);

    public class EventStore
    {
        private AccountSystem _accountSystem;

        public event LogTransactionEvent Changed;

        public EventStore()
        {
            _accountSystem = new AccountSystem();
        }

        protected virtual void OnChanged(TransactionEvent t, EventArgs e)
        {
            Console.WriteLine($"{DateTime.Now} [{nameof(EventStore)}] Logging transaction {t} {e}");
            Changed?.Invoke(t, e);
        }

        public Customer Transaction(Customer c, decimal amount, Transction t)
        {
            Customer action = _accountSystem.Transaction(c, amount, t);
            OnChanged(new TransactionEvent(action, c, t), EventArgs.Empty);            
            return action;
        }
    }
}
