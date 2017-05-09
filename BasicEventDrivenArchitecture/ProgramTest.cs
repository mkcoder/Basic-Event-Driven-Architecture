using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace BasicEventDrivenArchitecture
{
    [TestFixture]
    public class AccountSystemTest
    {
        private Customer bob = new Customer("Bob");
        private Customer jane = new Customer("Jane");
        private EventStore eventStore = new EventStore();
        private TransactionEventBook TEB;

        [SetUp]
        public void Setup()
        {
            TEB = new TransactionEventBook(eventStore);
            BookReplay br = new BookReplay(TEB.Book);
        }

        [Test]
        public void WithdrawOfNegativeAmountStateDoesntChange()
        {
            // Given
            bob = eventStore.Transaction(bob, 1.0m, Transction.Deposit);
            bob = eventStore.Transaction(bob, 1.0m, Transction.Deposit);
            bob = eventStore.Transaction(bob, 1.0m, Transction.Deposit);
            bob = eventStore.Transaction(bob, 1.0m, Transction.Deposit);

            // Then
            Assert.AreEqual(bob.Amount, 4.0m);

            bob = eventStore.Transaction(bob, 100.0m, Transction.Withdraw);
            Assert.AreEqual(bob.Amount, 4.0m);
        }

        [Test]
        public void AccountSystemWorkAsDesigned()
        {
            // Given
            bob = eventStore.Transaction(bob, 1.0m, Transction.Deposit);
            bob = eventStore.Transaction(bob, 1.0m, Transction.Deposit);
            bob = eventStore.Transaction(bob, 1.0m, Transction.Deposit);
            bob = eventStore.Transaction(bob, 1.0m, Transction.Deposit);

            // Then
            Assert.AreEqual(bob.Amount, 4.0m);

            bob = eventStore.Transaction(bob, 4.0m, Transction.Withdraw);
            Assert.AreEqual(bob.Amount, 0.0m);
        }

        [Test]
        public void AccountSystemWorkAsDesignedWithMoreThanOneUser()
        {
            // Given
            bob = eventStore.Transaction(bob, 1.0m, Transction.Deposit);
            bob = eventStore.Transaction(bob, 1.0m, Transction.Deposit);
            bob = eventStore.Transaction(bob, 1.0m, Transction.Deposit);
            bob = eventStore.Transaction(bob, 1.0m, Transction.Deposit);

            jane = eventStore.Transaction(jane, 100m, Transction.Deposit);
            jane = eventStore.Transaction(jane, 45m, Transction.Deposit);
            jane = eventStore.Transaction(jane, 5m, Transction.Withdraw);

            // Then
            Assert.AreEqual(bob.Amount, 4.0m);
            Assert.AreEqual(jane.Amount, 140.0m);

            bob = eventStore.Transaction(bob, 4.0m, Transction.Withdraw);
            Assert.AreEqual(bob.Amount, 0.0m);
        }
    }

    [TestFixture]
    public class BookReplayTest
    {
        private Customer jane = new Customer("Jane");
        private Customer bob = new Customer("Bob");
        private EventStore eventStore = new EventStore();
        private TransactionEventBook TEB;
        private BookReplay br;

        [SetUp]
        public void Setup()
        {
            TEB = new TransactionEventBook(eventStore);
            br = new BookReplay(TEB.Book);
            SetupState();
        }

        private Customer getCustomerForId(string customerCustomerId) => customerCustomerId == "Bob" ? bob : jane;

        private void SetupState()
        {
            // Given
            bob = eventStore.Transaction(bob, 1.0m, Transction.Deposit);
            bob = eventStore.Transaction(bob, 1.0m, Transction.Deposit);
            bob = eventStore.Transaction(bob, 1.0m, Transction.Deposit);
            bob = eventStore.Transaction(bob, 1.0m, Transction.Withdraw);

            jane = eventStore.Transaction(jane, 100m, Transction.Deposit);
            jane = eventStore.Transaction(jane, 45m, Transction.Deposit);
            jane = eventStore.Transaction(jane, 5m, Transction.Withdraw);
        }

        [Test]
        public void TestThatWhenIReplayAllTheEventsFor()
        {
            foreach (var customer in br.ReplayAll())
            {
                Assert.AreEqual(getCustomerForId(customer.CustomerId).Amount, customer.Amount);
                Assert.AreEqual(getCustomerForId(customer.CustomerId).CustomerId, customer.CustomerId);
            }
        }

        [Test]
        public void TestThatWhenIReplayAllTheEventsByAUserBothStatesAreTheSame()
        {
            var replayCustomer = br.ReplayByUser("Bob");
            Assert.AreEqual(replayCustomer.Amount, bob.Amount);
            Assert.AreEqual(replayCustomer.CustomerId, bob.CustomerId);
        }

        [Test]
        public void TestThatWhenIReplayUptoAEventNumberBothCustomerAreAtTheSameState()
        {
        }
    }


}
