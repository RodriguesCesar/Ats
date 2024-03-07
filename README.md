# TOTVS Career Role Opportunity Project

This project aims to demonstrate proficiency in building microservices architecture using best practices with .NET Core 6, Domain-Driven Design (DDD), unit and integration tests, Docker, and Angular v17 for frontend development. The project serves as an examination for potential career opportunities at TOTVS, showcasing the ability to implement a scalable and maintainable solution following industry standards.

## Project Overview

The project consists of two main components:

1. *Microservices Backend with .NET Core 6 and DDD*:
    - Implements microservices architecture using .NET Core 6, adhering to the principles of Domain-Driven Design (DDD).
    - Each microservice represents a distinct domain or business function, facilitating modularity and scalability.
    - Utilizes best practices for building APIs, including RESTful design principles and proper validation.
    - Implements unit tests for each domain logic to ensure reliability and maintainability.
    - Integrates Docker for containerization, enabling easy deployment and scalability.
    - Provides comprehensive documentation for developers to understand the domain models, API endpoints, and integration procedures.

2. *Frontend Application with Angular v17*:
    - Develops the frontend using Angular v17, a robust framework for building dynamic web applications.
    - Implements responsive and user-friendly interfaces following modern design principles.
    - Integrates with the backend microservices via RESTful APIs to fetch and manipulate data.
    - Utilizes Angular services and components to encapsulate functionality and enhance code maintainability.
    - Implements unit and integration tests for frontend components to ensure quality and reliability.
    - Offers seamless navigation and interactive user experiences to enhance usability.

## Getting Started

To run the project locally, follow these steps:

1. *Clone the Repository*:
    
    git clone https://github.com/RodriguesCesar/Ats.git
    

2. *Backend Setup*:
    - Navigate to the backend directory:
    
    cd backend
    
    - Install dependencies:
    
    dotnet restore
    
    - Build the solution:
    
    dotnet build
    
    - Run unit tests:
    
    dotnet test
    
    - Start the microservices using Docker:
    
    docker-compose up
    

3. *Frontend Setup*:
    - Navigate to the frontend directory:
    
    cd frontend
    
    - Install dependencies:
    
    npm install
    
    - Build the project:
    
    ng build
    
    - Run the frontend application:
    
    ng serve
    

4. *Access the Application*:
    - Once both backend and frontend are running, access the application at http://localhost:4200.

## Contributing

Contributions to improve the project are welcome. Feel free to submit pull requests with enhancements, bug fixes, or new features.

## License

This project is licensed under the [MIT License](LICENSE).
