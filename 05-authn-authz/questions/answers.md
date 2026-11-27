### 1. What is the difference between authentication and authorization?

Authentication is a process of validating the identity of a user (human user, cloud service, IoT device, etc.). Authentication typically uses one or several factors. The factors are classified into three groups:
- What you know (PIN, password)
- What you have (smartphone, ID card)
- What you are (fingerprint, iris pattern, voice)

On the other hand, authorization stands for a process of determining what a user can do, which resources access. Access is determined with the use of rules, permissions, and policies. Authorization normally goes after successful authentication, although it also can do without authentication (anonymous access). 

### 2. What authorization approaches can you list? What is role-based access control?

These are the common authorization models in the modern world:
- Role-based access control (RBAC): roles are sets of permissions; a role is assigned to a user
- Mandatory access control (MAC): permissions are strictrly regulated centrally
- Attribute-based access control (ABAC): fine-grained permissions (based on time, IP, or device used, etc.)
- Claim-based access control (CBAC): used with microservices, OAuth 2.0, JWT

The bedrock of the RBAC model is a role - a group of permissions. Every user or system gets an appropriate role, and if the user changes position in the company, a new role is assigned to them. Additionally, RBAC facilitates the Least Privilege principle, making the security posture more robust. On the flipside, in some cases the role-based approach renders itself too rigid for business rules of a company, and ABAC might be employed for a more fine-grained configuration.

### 3. What exactly is Identity Management (Identity and Access Management)?

Identity and Access Management (IAM) is a holistic process which embraces both authentication and authorization concerns. With IAM you can be sure your resources are accessed by right users with right permissions.

Key capabilities of a mature IAM system include:
- Seamless sign-up and sign-in experience
- Variety of supported identity providers (Facebook, Apple, Azure Entra, etc.)
- Support of multi-factor authentication
- Role-based access control implementation

### 4. What authentication/authorization protocols do you know? What is the difference between OAuth & OpenID?

There are several protocols widely used in modern world:

- OAuth 2.0 - open-source authorization standard; supports various flows
- OpenID Connect (OIDC) - lightweight extension of OAuth, used for authentication
- Security Assertion Markup Language (SAML) - widely used in enterprise solutions XML-based standard
- Web Services Federation (WS-Fed) - protocol, widely used in Windows and Azure ecosystems

OAuth and OIDC are very close but their purposes are different:

- OAuth is an authorization standard; its flows make use of so called access tokens, which list user's permissions
- OIDC is an authentication protocol build upon OAuth; its goal is to provide an ID token which contains user profile data.

### 5. What is Authentication/Authorization Token. What is JWT token? What other approaches except authentication/authorization, can we use with security token?

An authentication/authorization token is a secure string that being presented by a client, let's them be authenticated/authorized to the system. 
The most common forms of tokens are:
- JWT token (explained further)
- opaque string (usually, a key to the session data stored on the server)
- API Key (token assigned to the client to identify it)

JWT token is self-contained, and enables stateless interaction. Opaque tokens and API keys presume some sort of state persisted.

JWT stands for JSON Web Token, and it's a standard used for safe transfer of sets of key-value entries. In its most popular variant, a JWT token consists of three parts:
- header (representing metadata)
- payload (containing the useful business data itself)
- signature (cryptographic footprint of header + payload)
These parts are JSON objects, and the JWT token is a period-separated string of Base64 representations of these parts. There are unsigned and encrypted versions of JWT also available in the standard.

There are some other applications of security tokens:
- client-side safe session storage (e.g., a JWT signed by the server)
- data exchange integrity (with the help of the signature)
- password reset, e-mail verification links (JWT is sent as a query string parameter)

### 6. What is Single Sign-On (SSO)? Name the steps to implement SSO. What are the benefits of SSO?



### 7. What is the difference between Two-Factor Authentication and Multi-Factor Authentication?

First, it is necessary to recall what kinds of authentication factors are there:
- something you know (password, PIN)
- something you have (smartphone, smart card)
- something you are (iris pattern, fingerprint)

In order to do MFA, you should check at least 2 factors from different groups (password + authenticator app, card + fingerprint, etc.) Two-factor authentication is just a subset of Multi-factor authentication, limited to exactly two factors used. MFA term is used as a general definition of authentication done with more than one factor.

### 8. Which of the OAuth flows can be used for user (customer) and which for client (server) authentication?