# What are the cons and pros of the Monolith architectural style?

Cons: 
- Developing an application as a monolith often leads to tight coupling, the codebase becomes convoluted and difficult to understand (when the size of the application grows).
- The development is not flexible in terms of technology choice (you cannot build a module using a technology which is not from the monolith's stack).
- Another drawback is poor scalability: if a component needs scaling, it can be scaled only with the whole application.

Pros: 
- Monolith applications are easy to develop and deploy, execution is traceable and, thanks to in-memory calls, the performance is high (no network overhead).

# What are the cons and pros of the Microservices architectural style?

Cons: 
- This style introduces complexity of deployment: it is necessary to manage CI/CD for a whole fleet of autonomous services
- When the boundaries of separate services are determined and the services are built, it is quite difficult to introduce changes to the contracts.
- Testablity and traceability are complex and sometimes problematic due to the distributed nature of transactions; debugging is also harder than in Monolith case.
- Harder to attain transactional consistency: more commonly eventual consistency is embraced.

Pros: 
- Scalability is optimal: it is possible to scale exactly the services which need to be scaled to process the load; the other components may remain intact.
- There is flexibility in technology choices: each service may be built using the most appropriate stack, there is just a requirement that the services should understand each other (via some common protocol)
- Each service can have its own dedicated team and autonomous SDLC, the codebase for a service is generally smaller; the only caveat is respecting the contracts and watching for breaking changes.

# What is the difference between SOA and Microservices?

Both SOA and Microservices are architectural styles oriented on autonomous services with clear business responsibilities, however, in case of the latter the scope is more granular.

Main difference between the styles is the scope of their application: SOA is integration- or enterprise-wide while Microservices is application-wide. 

One of other differences lays in the interprocess communication medium: in SOA it's a so-called Enterprise Service Bus, a centralised massive layer often loaded with business logic; in Microservices HTTP or lightweight messaging protocols are used (no business logic lives in the communication).

Additionally, SOA solutions usually make use of a single data storage while in Microservices each service has its own persistence.

# [Open question] What does hybrid architectural style mean? Think of your current and previous projects and try to describe which architectural styles they most likely followed.

A hybrid architectural style is a style which bears certain features or properties from two or more canonical ("pure") styles (Monolith, Microservices, Service-Oriented, etc.) It may be a monolithic thick client which uses data layer as a service (a kind of DBaaS). Or the same client may be a part of a Microservice architecture along with traditional granular services.

I can remember a project I worked on some time ago. That was a desktop application that categorized massive loads of data. When we decided to get rid of a bottleneck via cloud-based workers (serverless Azure Functions), all the processing had been tackled in memory and quite often the application failed. Our pure monolithic desktop solution delegated a huge part of its workflow to the cloud computing, thus making the architecture hybrid.

# Name several examples of the distributed architectures. What do ACID and BASE terms mean.

Distributed architectures - those where the components are hosted in separate processes, often on separate networked resources (physical machines, virtual machines, containers, etc.) - are the following:

- Microservices (fleet of autonomous services)
- Service-oriented (enterprise-wide apps, ESB)
- Client-server (front-end, back-end)
- Serverless (cloud-based workers)
- Peer to peer (nodes in the network)

ACID stands for Atomicity, Consistency, Isolation, Durability - set of mandatory properties of a reliable transaction processing. This principles are fundamental for traditional relational DBMSs:
- Atomicity - either all or no operations succeed
- Consistency - no constraint is violated as a result of the transaction
- Isolation - parallel transactions don't interfere
- Durability - successful transaction is guaranteed to be stored

BASE stands for Basic Availability, SOft State, Eventual Consistency. It is a concept integral in distributed systems and NoSQL databases:
- Basic Availability - every reqest receives a response
- Soft State - state may change without direct user input (think background replication)
- Eventual Consistency - data may be out of sync until the replication process is over

# Name several use cases where Serverless architecture would be beneficial.