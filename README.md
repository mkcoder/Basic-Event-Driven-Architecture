# Basic-Event-Driven-Architecture
I was fortunate enough to attend the GOTO Conference 2017 in Chicago. 
Martin Fowler who was the keynote speaker on Monday talked about Event Driven Architecture.
From the 4 different system he mentioned; event sourcing seemed to be the coolest.

So here is a very basic example i could think of that works?

`[Customer] - a single enttiy that represent a state`

`[AccountSystem] - Which is hidden inside out EventStore and called from there`
 
`[EventStore] - is the one that calls the events and delegates transaction call to the AccountSystem`

`[TransactionEventBook] - subscribe to the event inside the EventStore and stores all the Transction`

`[BookReplay] - replays events in the book: all, n amount, or filtered by CustomerId;`



