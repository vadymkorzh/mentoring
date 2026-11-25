### 1. What is the difference between authentication and authorization?

Authentication is a process of validating the identity of a user (human user, cloud service, IoT device, etc.). Authentication typically uses one or several factors. The factors are classified into three groups:
- What you know (PIN, password)
- What you have (smartphone, ID card)
- What you are (fingerprint, iris pattern, voice)

On the other hand, authorization stands for a process of determining what a user can do, which resources access. Access is determined with the use of rules, permissions, and policies. Authorization normally goes after successful authentication, although it also can do without authentication (anonymous access). 

### 2. What authorization approaches can you list? What is role-based access control?



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



### 6. What is Single Sign-On (SSO)? Name the steps to implement SSO. What are the benefits of SSO?



### 7. What is the difference between Two-Factor Authentication and Multi-Factor Authentication?



### 8. Which of the OAuth flows can be used for user (customer) and which for client (server) authentication?