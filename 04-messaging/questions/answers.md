### 1. What is Message Based Architecture? What is the difference between Message Based Architecture and Event Based Architecture?’

Message Based Architecture (MBA) is an architectural pattern used to build higly scalable and loosely coupled solutions. A cornerstone of the architecture is a notion of a message - an object that carries some data and intended to trigger some action downstream.

Messages are sent to a channel like a message queue or service bus, which handles routing, durability, and other concerns. MBA mainly utilizes a point-to-point communication model, when one message producer is bound to a particular message consumer, and a message is to be consumed only once.

MBA finds most use cases in reliable asynchronous processing, when a task must be delivered and executed in a guaranteed, scalable and fault-tolerant way. It may be a task queue, a load balancing solution, etc.

Event Based Architecture is another message-driven architectural pattern highly popular in building distributed systems. It differs from MBA in the following way:
- Semantically: in case of EBA, a message is called Event, and it states a completed fact; interested parties might react to the fact or ignore it.
- Distribution model: usually, the Publisher/Subscriber (Pub/Sub) model is used, with 1-to-many relation between the producer and the consumers.
- Primary use cases: real-time event streaming, notifications, audit logging.

### 2. What is Message Broker? How do message brokers work?

Message Broker is a software solution which facilitates asynchronous message-based communication between services, systems, or applications. 

Messages are reliably and durably collected in message queues, and the solution must guarantee message delivery. In addition, data routing, message translation, client state management features are supplied by the broker.

Main part of any message broker are message queues which accumulate data in form of messages, and message managers which handle the validation, routing, persistence and delivery of messages.

Message delivery is often implemented using receipts: a message is retained in the queue until the consumer reliably admits its successful processing. Otherwise, the message is returned to the queue and scheduled for a retry. If a message isn't processed after some configured amount of time or number of retries, it becomes poisonous and is placed in the Dead Letter Queue.

### 3. When should you use message brokers?

Message brokers may be used when reliable communication, fault-tolerance, and delivery guarantees are required. 

Possible use cases: 
- Microservices communication: messaging facilitates loose coupling, extensibility, scalability, and maintainability.
- Mobile notifications: pub/sub model is a perfect match for handing message broadcasting.
- Long-running tasks: because of the asynchronous nature of messaging, there will be no blocking calls.
- Crucial business tasks: message brokers are resilient and guarantee message delivery in case of failures.

### 4. Name and describe any distribution pattern.

There are two main message distribution models used in MBs:
- Point-to-point (1-to-1 relationship): one producer is bound to a single consumer.
- Publish/Subscribe (1-to-many relationship): a producer (publisher) emits a message (to a so-called topic), which may be consumed by 0 or more interested subscribers. 

In case of Pub/Sub model, producers post messages to a configured destination (a topic), consumers subscribe to topics they are interested in, and the message broker is responsible for routing and delivering messages to configured topics. This model allows for close-to-ideal decoupling of producers and consumers as neither side know anything about its counterpart(s).

The most widely known implementation of the pattern is Apache Kafka. RabbitMQ also provides this model via Topic/Fanout feature.

### 5. What are the advantages and disadvantages of using message broker?

Advantages of MBs: 
- Reliablity via delivery guarantees (message persistence, mechanism of retries, and dead letter queue)
- Performance via asynchronous message processing
- Fault-tolerance via ability to wait for the consumer to become online or healthy

Disadvantages:
- Added complexity to the architecture (as another piece of software is added as middleware)
- More effortful debugging and logging due to the distributed and asynchronous nature

### 6. What is the difference between Queue and Topic?

Queue is a backbone data structure used in Point-To-Point distribution model. If there are several consumers listening to a single queue, the load is distributed among the available processors (each consumer dequeues and works with a separate message). Once a consumer confirms successful completion of processing, the message is removed from the queue.

On the other hand, a topic collects a series of messages which are distributed evenly among the subscribers (a copy of every message to every consumer). Often, the messages are persisted in the topic for some pre-configured amount of time for new subscribers to have ability to consume data retrospectively. Topics are used in the Pub/Sub distribution model.

### 7. What are the typical failures in MBA? How can you address them? What is Saga pattern?

There are several typical MBA failures:

1. Poison messages: some messages might cause the consumer to fail (either because of validation issues or some other error) and return to the queue; thus, they are retried again and again, not allowing consequent messages to be processed; such messages are called poison messages.

    Solution: isolate poison messages in a special queue - Dead Letter Queue - and handle it separately; make consumer idempotent.

1. Dropped messages: either the broker or the consumer may crash as soon as they acknowledge a message; it may lead to the message to be dropped and lost.

    Solution: acknowledge messages on the broker or consumer side only when it is successfully processed; configure persistence (storing messages on a drive).

1. Message duplication: message brokers can't guarantee 100% only-once delivery, sometimes message duplications occurs.

    Solution: make message processing idempotent.

Pattern Saga is a distributed systems pattern widely used in microservices architectural style. It helps maintain transactional consistency of a distributed data operation. In case of a data operation that spans more than a single data store, a series of updates may be planned by the Saga pattern, which eventually (in case of success) lead the system to the desired state. If a failure occurs, there should be a compensatory mechanism in form of compensation transactions that will revert the changes made.

There are two main ways to implement the pattern: either using Choreography pattern (no central controller that handles the logic of the transaction) or Orchestrator pattern (with a dedicated sevice managing the flow). Each way implies usage of a message broker as an intermediary that provides message management.


