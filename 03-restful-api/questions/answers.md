### 1. Explain the difference between terms: REST and RESTful. What are the six constraints?

Basically, REST (REpresentative State Transfer) is an architectural style for distributed systems, defined in the dissertation written by Roy Fielding in 2000. The work defines 6 constraints which any RESTful system must follow (in fact, first 5 are mandatory whilst the last is optional):

1. Uniform interface: consistent uniform interface for client/server interaction must be defined

1. Client/server interaction: RESTful systems must be implemented using this pattern

1. Stateless: servers mustn't persist state between calls

1. Cacheable: RESTful responses must label themselves cacheable or not

1. Layered system: interaction is separated in layers, each layer interacts only with the layer immediately below it

1. Code on demand: servers may send code to clients

### 2. HTTP Request Methods (the difference) and HTTP Response codes. What is idempotency? Is HTTP the only protocol supported by the REST?

- Mainly used HTTP request methods are GET, POST, PUT, PATCH, and DELETE. Some other include HEAD, OPTIONS, QUERY. 

    The methods may be classified in various ways. One way is by safety: GET is a safe method, POST, PUT, PATCH and DELETE are not. Safe methods/operations don't alter the state of the system.

    Another way to classify the methods is by idempotency: GET, PUT, DELETE are idempotent, POST and PATCH are generally not.

- Standard HTTP response codes consist of three digit (XXX form) and semantically divided into five groups:
    - 1xx - Informational
    - 2xx - Success (200 OK, 204 No Content, etc.)
    - 3xx - Redirection (300 Multiple Choices, 302 Found, etc.)
    - 4xx - Client Error (400 Bad Request, 404 Not Found, etc.)
    - 5xx - Server Error (500 Internal Server Error, 503 Service Unavailable, etc.)

- Idempotency is a property of a REST operation that signifies that the result of an operation stays exactly the same if the operation is accomplished just once or many times sequentially with the identical attributes. With REST over HTTP, operations with GET, PUT, HEAD, OPTIONS, DELETE verbs are idempotent while POST and PATCH aren't.

- REST doesn't mandate usage of HTTP. Every interface that follows the six constraints of REST (described in 1) can be called RESTful. However, in practice HTTP is used as the application level protocol in the majority of REST implementations.

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

Versioning in REST makes breaking changes introduction much safer and easier. 

There are several different approaches to versioning for HTTP REST services:

- Path versioning (/v1/users -> /v2/users): deemed to be optimal for most projects; controversial as violates REST prinicple of stable resource identifier
- Subdomain versioning (v1.example.com -> v2.example.com): incurs some DNS or CORS configuration overhead
- Query string versioning (/users?v=1 -> /users?v=2): generally discouraged (caching issues; semantic issues)
- Header versioning (X-API-Version: v1 or Accept: application/foo.v1): more complicated for clients; harder to test

### 6. What are the best practices of resource naming?

There are several guidelines for resource naming in REST style:

1. Resources should use nouns for names (singular for specific resources, plural for collections)
1. Verbs in resource IDs (URIs) should be avoided
1. Trailing slashes (/) in URIs are also discouraged
1. It is better to use hyphens (-) over underscores (_) in resource IDs
1. Parts of URI should be spelled in lowercase

### 7. What are OpenAPI and Swagger? What implementations/libraries for .NET exist? When would you prefer to generate API docs automatically and when manually?

OpenAPI and its historical precursor Swagger are both language-agnostics standards (or specifications) for describing HTTP APIs. Nowadays, Swagger is used as an umbrella term for a suite of both open-source and commercial products which implement OpenAPI specification, e.g. Swagger UI.

Since .NET 9, the ecosystem (ASP.NET Core) has a built-on OpenAPI support (Microsoft-managed package). Before that, Swashbuckle and NSwag packages were used to utilize Swagger/OpenAPI features. Both packages are still available:
- Swashbuckle: adds Swagger JSON document generation and Swagger UI utility
- NSwag: generate Swagger JSON and Swagger UI / ReDoc UI + client code generation (TS, Angular, C#, etc.)
NSwag is actively supported, Swashbuckle is almost deprecated and should be avoided.  

Automatic API documentation presentation works well when the API is not published to end-users for consumption and consulting. Some internal APIs can tolerate lack of gloss and details. However, pure manual API documentation is also cumbersome and hard to maintain. So, generally and pragmatically, a hybrid approach should be utilized: quick automatic generation for endpoint/parameter/request/response description plus human-created texts for quickstarts/tutorials/edge cases.

### 8. What is OData? When will you choose to follow it and when not?

OData stands for Open Data Protocol, and it is a standard that sets up rules for uniform provision and consumption of REST services. OData's particular features are built-in querying facilities that might be included directly in the URI (filtering, aggregation, etc.)

This protocol is widely used by enterprises via Microsoft or SAP products, and it is quiete reasonable choice for building complex enterprise systems. However, for simpler models and more lightweight services REST or GraphQL might be more appropriate. 

### 9. What is Richardson Maturity Model? Is it always a good idea to reach the 3rd level of maturity?

Richardson Maturity Model (aka RMM) consists of four levels of RESTful service maturity:

- L0: single URI for all resources (not necessarily named as a noun) + single verb (typically, POST) are used (procedural call, aka RPC)
- L1: multiple URIs (for different types of resources; every resource is uniquely identified) + still a single verb (again, typically POST)
- L2: resources are uniquely idenified with URIs + HTTP verbs are used semantically for CRUD operations
- L3: this level adds so called HATEOAS (Hypermedia As The Engine Of Application State) to L2 (each response carries navigational links to related resources and actions, aka hypermedia controls)

Seemingly, most real-world RESTful services operate on L2, and it is quite enough for the customers. This level relies on documentation extensively, as there is no hypermedia for automatic discoverability (brought by L3). However, L3's distinctive feature add significant complexity, which burdens both clients and server maintainers, and this 'perfect' level of RESTful maturity is mainly neglected.

### 10. What does pros and cons REST have in comparison with other web API types?


