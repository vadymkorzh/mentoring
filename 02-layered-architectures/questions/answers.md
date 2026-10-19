# Name examples of the layered architecture. Do they differ or just extend each other?

There are several examples of the layered architecture:
- Onion Architecture
- Hexagonal Architecture (aka Ports and Adapters Architecture)
- Clean Architecture

They all are in fact exemplify evolutionary development of the idea of the Layered Architecture. What is different is the code organization and
dependency orientation: the three architectures above are domain-centric and embrace the idea of Dependency Inversion Principle, making the core domain models and
business logic completely independent from any other layers.

I would say that the architectures are not different, they extend and supersede each other in a way:
- Hexagonal architecture outlines the overall structure which would protect the core (external part vs. internal with ports/adapters on the boundaries)
- Onion architecture elaborates on the layered structure of the application core (domain model, domain services, architectural services)
- Clean architecture is a standardized summarized variant of Hexagonal and Onion architectures, popularized by Uncle Bob

# Is the below layered architecture correct and why? Is it possible from C to use B? from A to use C?

![Image](image.png)

According to the canonical definition of the layered architecture, a layer can only depend on another layer which is lower in the hierarchy. So C -> B dependency violates the principles of the Layered Architecture and introduces a circular dependency flaw.

On the other hand, A -> C dependency is permitted in the Layered Architecture, if we treat layer B as open. Such a laxation is acceptable when the layer above the open one uses the layer below naturally. However, this decision should be weighted and cautious as it violates level isolation.

# Is DDD a type of layered architecture? What is Anemic model? Is it really an antipattern?

Domain-Driven Development shouldn't be called a type of layered architecture, it is rather a software design philosophy, not a structural architecture.
DDD provides building blocks for the buisiness logic and guidelines how to organize the core of the application, and it isn't focused on concrete layers and dependency orientation concerns.

Anemic Model (anti-)pattern is a way to build domain model so that the state resides in a plain object, and the logic associated is placed somewhere else (typically, in a separate service). Proponents of OOP claim that this pattern is a direct violation of Encapsulation (keeping data and logic as close as possible), so it's in fact an anti-pattern. However, Anemic Model is widely used in modern frameworks, e.g., entities in ORMs, DTOs, plain CRUD objects. Generally, it depends on the complexity of the solution whether you should avoid the pattern; if the domain is simple, it is more pragmatic to embrace the Anemic Model pattern.

# What are architectural anti-patterns? Discuss at least three, think of any on your current or previous projects.



# What do Testability, Extensibility and Scalability NFRs mean. How would you ensure you reached them? Does Clean Architecture cover these NFRs?