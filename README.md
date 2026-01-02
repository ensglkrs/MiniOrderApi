# MiniOrderApi - E-Commerce Backend System

## Project Description

MiniOrderApi is a robust backend RESTful API developed using .NET 8. It is designed to handle core e-commerce operations, including user authentication, product inventory management, order processing with automated business logic, and administrative analytics.

The project adheres to modern software engineering practices, utilizing N-Tier Architecture, the Repository Pattern, and solid security measures via JWT (JSON Web Tokens).

## Key Features

* **Authentication & Authorization:** Implementation of secure login and registration processes using JWT. The system supports Role-Based Access Control (RBAC) to differentiate between "Admin" and "User" privileges.
* **Product Management:** Full CRUD (Create, Read, Update, Delete) operations for product inventory.
* **Order Processing:** Advanced business logic that handles order creation, calculates total transaction amounts, and automatically deducts sold items from stock levels to ensure data integrity.
* **Admin Dashboard:** Dedicated endpoints for administrators to view real-time statistics, including total revenue, order counts, and best-selling products using LINQ queries.
* **Global Exception Handling:** A centralized middleware architecture to catch runtime errors and return standardized, client-friendly JSON responses.
* **Data Validation:** Integration of FluentValidation to strictly validate incoming requests and prevent invalid data entry.

## Technology Stack

* **Framework:** .NET 8 Core Web API
* **Database:** PostgreSQL
* **ORM:** Entity Framework Core (Code-First Approach)
* **Object Mapping:** AutoMapper
* **Validation:** FluentValidation
* **Documentation:** Swagger / OpenAPI
* **Authentication:** Microsoft.AspNetCore.Authentication.JwtBearer

## Solution Architecture

The project follows a modular N-Tier architecture to ensure separation of concerns and maintainability:

1.  **API Layer:** Contains Controllers and Global Exception Middleware.
2.  **Service Layer:** Handles business logic, DTO mapping, and interaction between the API and Data layers.
3.  **Data Access Layer (Repository):** Manages direct database interactions using Entity Framework Core.
4.  **Entity Layer:** Defines database models and Data Transfer Objects (DTOs).

## Getting Started

### Prerequisites

* .NET 8 SDK
* PostgreSQL Server
* Visual Studio 2022 or Visual Studio Code

### Installation & Setup

1.  **Clone the Repository**
    Download the project files to your local machine.

2.  **Database Configuration**
    Open the `appsettings.json` file and update the `ConnectionStrings` section with your PostgreSQL credentials.

3.  **Database Migration**
    Open the terminal in the project directory and run the following command to generate the database schema:
    ```bash
    dotnet ef database update
    ```

4.  **Run the Application**
    Execute the following command to start the API:
    ```bash
    dotnet run
    ```

5.  **Access Documentation**
    Once the application is running, navigate to the Swagger UI to test endpoints:
    `https://localhost:7xxx/swagger`

## API Endpoints Overview

### Auth
* `POST /api/auth/register`: Register a new .
* `POST /api/auth/login`: Authenticate and receive a Bearer Token.

### Products
* `GET /api/products`: Retrieve a list of all products with pagination and filtering.
* `POST /api/products`: Create a new product (Admin only).
* `PUT /api/products/{id}`: Update an existing product (Admin only).
* `DELETE /api/products/{id}`: Delete a product (Admin only).

### Orders
* `POST /api/orders`: Place a new order. The system automatically identifies the customer from the token.
* `GET /api/orders`: Retrieve order history for the authenticated .

### Dashboard (Admin Only)
* `GET /api/dashboard/stats`: Retrieve high-level metrics (Revenue, Total Orders, Best Seller).

## License

This project is developed for educational and portfolio purposes.

Created by Enes Gülkurusu.
