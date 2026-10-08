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

# Name several examples of the distributed architectures. What do ACID and BASE terms mean.

# Name several use cases where Serverless architecture would be beneficial.