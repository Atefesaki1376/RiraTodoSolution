# MicroCrafter13 Clean Architecture Template (DDD)

Welcome to the **MicroCrafter13 Clean Architecture (DDD)** solution template! This template is designed to provide a starting point for building clean, scalable, and maintainable applications using Domain-Driven Design principles.

---

## **Project Structure**

This solution is organized into multiple layers to adhere to **Clean Architecture** principles, ensuring a separation of concerns and a clear boundary between different parts of the application.

### Core
- **Domain**: Contains the core business logic, domain entities, aggregates, value objects, and domain events.
- **Domain.Shared**: Includes shared kernel components such as value objects and reusable types.

### Application
- **Application**: Implements application use cases (commands and queries) and business rules.
- **Application.Contracts**: Defines interfaces for external dependencies (e.g., repositories, external APIs).

### Infrastructure
- **EFCore**: Contains the Entity Framework Core implementation for persistence and data access.

### Presentation
- This is where the front-end or API projects will go. For example:
  - Blazor (UI)
  - Web API (Backend)

### Solution Items
- Solution-wide configuration files, documentation, and other shared resources.

---

## **Getting Started**

### Prerequisites
- [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- A code editor like [Visual Studio](https://visualstudio.microsoft.com/) or [Visual Studio Code](https://code.visualstudio.com/).
- Docker (optional, for containerized infrastructure)

### Installation
1. **Create a New Project**:
   - Open Visual Studio.
   - Select "Create a New Project."
   - Search for and select the **MicroCrafter13 Clean Architecture Template (DDD)**.
   - Follow the prompts to configure your new project.

2. **Run the Project**:
   - Set up necessary infrastructure (e.g., databases) by running `docker-compose` (if applicable).
   - Use `dotnet run` to start your project.

---

## **Customizing the Template**

### Adding New Projects
- Use the existing folder structure to maintain organization.
- Add your bounded contexts under `Domain` and `Application` for new business areas.

### Configuration
- Modify the `appsettings.json` files in the infrastructure layer to configure dependencies like databases, caching, or external APIs.
- Use environment variables for sensitive data.

---

## **Features**
- Domain-Driven Design (DDD) support
- Entity Framework Core integration
- Clean Architecture principles
- Fully modular and scalable
- Preconfigured layers for maintainable code
- automapper
- fluentvalidation for validate dtos

---

## **Roadmap**
- Add a default `Presentation` project for Web API or Blazor UI.
- Include CI/CD pipeline templates (e.g., GitHub Actions, Azure Pipelines).
- Extend support for additional persistence options (e.g., MongoDB).

---

## **Contributing**
We welcome contributions! If you want to add new features or suggest improvements:
1. Fork this repository.
2. Create a new branch (`feature/my-feature`).
3. Submit a pull request with details about your changes.

---

## **License**
This project is licensed under the MIT License. See `LICENSE` for details.

---

## **Acknowledgments**
Thanks to the .NET community for inspiration and guidance in building this template!
