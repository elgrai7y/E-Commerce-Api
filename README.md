E-Commerce API Project
This repository contains a robust backend system for an E-Commerce platform, built with a focus on clean architecture, scalability, and modern design patterns. The API facilitates core e-commerce functionalities including product discovery, cart management, and secure order processing.


## Technical Architecture
The project is built using an N-Tier Architecture consisting of three primary layers plus a Common layer to ensure a separation of concerns.


### Key Patterns & TechnologiesFramework: 
.NET with Entity Framework (EF) Core
.Patterns: Repository Pattern (Generic & Non-Generic), Unit of Work, and Result Pattern for consistent API responses
.Validation: Fluent Validation for robust data integrity
.DTOs: Data Transfer Objects used for decoupled data mapping
.Asynchronous Programming: Fully implemented async/await throughout the system.


## Features
1. Authentication & AuthorizationMicrosoft Identity: Utilized for comprehensive user management.JWT Authentication: Secure token-based access.Policy-Based Authorization: Fine-grained access control.Security: UserId is securely extracted directly from JWT Claims rather than being passed in request bodies.
2. Product & Category ManagementFull CRUD operations for Categories and Products.Advanced Product browsing featuring filtering, search, and pagination.Integrated File Management for uploading Product and Category images.
3. Shopping Cart & OrdersCart Management: Add items, remove items, and update quantities.Order Processing: Place orders and view a detailed order history.
