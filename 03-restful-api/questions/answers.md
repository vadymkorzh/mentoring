### 1. Explain the difference between terms: REST and RESTful. What are the six constraints?

Basically, REST (REpresentative State Transfer) is an architectural style for distributed systems, defined in the dissertation written by Roy Fielding in 2000. The work defines 6 constraints which any RESTful system must follow (in fact, first 5 are mandatory whilst the last is optional):

1. Uniform interface: consistent uniform interface for client/server interaction must be defined

1. Client/server interaction: RESTful systems must be implemented using this pattern

1. Stateless: servers mustn't persist state between calls

1. Cacheable: RESTful responses must label themselves cacheable or not

1. Layered system: interaction is separated in layers, each layer interacts only with the layer immediately below it

1. Code on demand: servers may send code to clients

### 2. HTTP Request Methods (the difference) and HTTP Response codes. What is idempotency? Is HTTP the only protocol supported by the REST?

Idempotency is a ...

REST doesn't mandate usage of HTTP. Every interface that follows the six constraints of REST (described in 1) can be called RESTful. However, in practice HTTP is used as the application level protocol in the majority of REST implementations.

### 3. What are the advantages of statelessness in RESTful services?

1. In the main, statelessness facilitates scalability of RESTful services: as long as there is no state persisted on the server side, it is possible to spawn any number of instances of the service to process increased load.

1. Additionally, statelessness makes caching much more transparent, since there is no behind-the-scene context associated with responses to be cached. As long as caching is easier, the performance is better.

1. Thirdly, since the state is managed only on the client side, the complexity of the service implementation is reduced. It makes it more maintainable and understandable.

### 4. How can caching be organized in RESTful services?

Caches may exist in any place between the client and the service, e.g., it may be the local (browser) cache, proxy cache, or reverse proxy (gateway, load balancer) cache. Real systems ofther have a series of caches instead of a single one.

In case of the most used REST approach (via HTTP), there are following rules applied to caching policies:

- GET responses are cacheable by default (and usually are)
- POST responses aren't cacheable by default (special headers are needed to enable caching)
- PUT, PATCH, DELETE - not cacheable
- cacheability can be overriden by specific HTTP headers: Expires, Cache-Control, ETag, Last-Modified

### 5. How can versioning be organized in RESTful services?



### 6. What are the best practices of resource naming?



### 7. What are OpenAPI and Swagger? What implementations/libraries for .NET exist? When would you prefer to generate API docs automatically and when manually?



### 8. What is OData? When will you choose to follow it and when not?



### 9. What is Richardson Maturity Model? Is it always a good idea to reach the 3rd level of maturity?



### 10. What does pros and cons REST have in comparison with other web API types?


