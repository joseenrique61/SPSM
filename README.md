# Spare Parts Shop (SPSM)

Spare Parts Shop is an e-commerce platform designed for buying and selling automotive spare parts. This project implements a modern, robust, and scalable software architecture based on microservices, designed to overcome the limitations of traditional monolithic architectures.

## Table of Contents
1.  [Introduction](#introduction)
2.  [The Problem to Solve](#the-problem-to-solve)
3.  [Architectural Features](#architectural-features)
4.  [System Architecture](#system-architecture)
    -   [Microservices](#microservices)
    -   [Technology Stack](#technology-stack)
5.  [Getting Started](#getting-started)
    -   [Prerequisites](#prerequisites)
    -   [Running the Application](#running-the-application)
6.  [Architecture Diagrams](#architecture-diagrams)
7.  [Source Code Repository](#source-code-repository)
8.  [Authors](#authors)

## Introduction
This project represents the design and implementation of a **Microservices Architecture (MSA)** for the *Spare Parts Shop* web application. The solution aims to deliver a platform with high availability, fault tolerance, and elastic scalability by using an ecosystem of autonomous, containerized services with Docker, orchestrated through an API Gateway.

## The Problem to Solve
Traditional monolithic software architectures present serious challenges as a business evolves. This project directly addresses the following problems:

-   **Strong Coupling:** A change in a small part of the system requires redeploying the entire application.
-   **Limited Scalability:** The entire application must be scaled as a single unit, leading to inefficient resource usage.
-   **Fragility:** A failure in a non-critical component can bring down the entire system.
-   **Technological Barriers:** The monolithic codebase makes it difficult to adopt new technologies or update existing ones.

## Architectural Features
The solution is designed with the following patterns and principles to ensure a resilient and high-performance system:

-   **High Availability (HA):** The system is designed to remain operational even in the event of partial failures of internal components.
-   **Horizontal Scalability:** Each microservice can be scaled independently, optimizing infrastructure usage.
-   **Fault Tolerance:** The **Bulkhead Pattern** (isolation via Docker containers) and asynchronous communication are used to prevent failures in one service from cascading throughout the system. Unprocessed messages are handled by a **Dead Letter Queue (DLQ)**.
-   **CQRS (Command Query Responsibility Segregation):** Write operations (`InventoryService`) are separated from read operations (`SearchService`) to optimize the performance and scalability of each.
-   **API Gateway (Kong):** Acts as a single entry point, centralizing responsibilities such as routing, authentication (JWT validation), load balancing, and rate limiting.
-   **Asynchronous Communication (RabbitMQ):** The **Publish/Subscribe** pattern is used to decouple services, improve resilience, and enhance system responsiveness.
-   **Database per Service:** Each microservice has its own database, ensuring low coupling and technological autonomy. Eventual consistency is achieved through events.
-   **Optimistic Concurrency Control:** To prevent race conditions (e.g., overselling a product), a version token is used for critical inventory update operations.

## System Architecture

### Microservices
The business logic is broken down into the following independent and autonomous services:

-   **`InventoryService`**: The write (Command) service for the product catalog. It is the source of truth for product data and publishes change events.
-   **`SearchService`**: The read (Query) service, optimized for searches. It subscribes to events from `InventoryService` to keep its data replica synchronized.
-   **`UserService`**: Manages user registration, authentication, and JWT (JSON Web Token) generation.
-   **`ShoppingCartService`**: Manages the state of each user's shopping cart (add, remove, update products).
-   **`PaymentService`**: Processes payment transactions and publishes a `PaymentCompleted` event upon a successful purchase.
-   **`NotificationService`**: Sends notifications to users (e.g., order confirmations via email) by subscribing to events like `PaymentCompleted`.

### Technology Stack
-   **Backend:** .NET / ASP.NET Core
-   **Databases:** PostgreSQL and MongoDB
-   **Containerization:** Docker
-   **API Gateway:** Kong
-   **Message Broker:** RabbitMQ
-   **Security:** JWT (JSON Web Tokens)

## Getting Started
Follow these steps to run the development environment locally.

### Prerequisites
Ensure you have the following tools installed on your system:
-   [Docker](https://www.docker.com/get-started)
-   [Docker Compose](https://docs.docker.com/compose/install/)
-   Git
-   A command-line terminal (like Bash).

### Running the Application
1.  Clone the repository to your local machine:
    ```bash
    git clone https://github.com/joseenrique61/SPSM.git
    cd SPSM
    ```
2.  Run the startup script. This command will build the Docker images and start all the containers for the microservices, databases, and required infrastructure.

    ```bash
    bash start.sh
    ```
Once the script finishes, the entire platform will be running and ready to use.

## Architecture Diagrams
For a visual and interactive exploration of the architecture (C1, C2, and C3 models), you can visit the following IcePanel link:

-   **[View Diagrams on IcePanel](https://s.icepanel.io/qfyErBmyiktAwS/BlAs)**

## Authors
This project was developed by:
-   **José Cerezo:** Backend Developer and DevOps
-   **Mateo Puga:** UI/UX Designer and Technical Writer
-   **Ariel Anchapaxi:** Backend Developer and Software Architect
