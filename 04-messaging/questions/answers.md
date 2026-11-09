### 1. What is Message Based Architecture? What is the difference between Message Based Architecture and Event Based Architecture?’

Message Based Architecture (MBA) is an architectural pattern used to build higly scalable and loosely coupled solutions. A cornerstone of the architecture is a notion of a message - an object that carries some data and intended to trigger some action downstream.

Messages are sent to a channel like a message queue or service bus, which handles routing, durability, and other concerns. MBA mainly utilizes a point-to-point communication model, when one message producer is bound to a particular message consumer, and a message is to be consumed only once.

MBA finds most use cases in reliable asynchronous processing, when a task must be delivered and executed in a guaranteed, scalable and fault-tolerant way. It may be a task queue, a load balancing solution, etc.

Event Based Architecture is another message-driven architectural pattern highly popular in building distributed systems. It differs from MBA in the following way:
- Semantically: in case of EBA, a message is called Event, and it states a completed fact; interested parties might react to the fact or ignore it.
- Delivery model: usually, the Publisher/Subscriber (Pub/Sub) model is used, with 1-to-many relation between the producer and the consumers.
- Primary use cases: real-time event streaming, notifications, audit logging.

### 2. What is Message Broker? How do message brokers work?



### 3. When should you use message brokers?



### 4. Name and describe any distribution pattern.



### 5. What are the advantages and disadvantages of using message broker?



### 6. What is the difference between Queue and Topic?



### 7. What are the typical failures in MBA? How can you address them? What is Saga pattern?