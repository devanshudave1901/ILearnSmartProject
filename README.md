# ILearnSmartProject

## Project overview

ILearnSmartProject is a Razor Pages web application built with .NET 10 that provides an educational platform for creating, organizing, and delivering interactive learning content. The project demonstrates modern ASP.NET Core practices, layered architecture, and common software development concepts suitable for learning and further extension.

## Goals

- Provide a simple, extensible Razor Pages application template for educational content
- Demonstrate clean project structure, configuration, and deployment for .NET 10
- Include examples of persistence, authentication, form handling, validation, and testing

## Key features

- Razor Pages UI for course, lesson, and quiz management
- User authentication and role-based authorization (Admin, Instructor, Student)
- Data persistence via Entity Framework Core with a relational database (e.g., SQL Server)
- Server-side validation and client-side enhancements (Bootstrap + unobtrusive validation)
- Unit and integration tests for core services and data access

## Tech stack

- .NET 10 (ASP.NET Core)
- Razor Pages (preferred project type for this workspace)
- Entity Framework Core for ORM and migrations
- SQL Server (LocalDB / Developer edition) or SQLite for lightweight testing
- Bootstrap for responsive UI
- xUnit for unit tests
- Git for source control; Visual Studio 2026 recommended for development

## Project structure (high level)

- `/Pages` — Razor Pages handlers and views (UI layer)
- `/Data` — EF Core DbContext, entities, migrations
- `/Services` — business logic and application services
- `/wwwroot` — static files (CSS, client JS, images)
- `/Tests` — automated unit and integration tests

## Software development concepts illustrated

- Layered architecture and separation of concerns
- Dependency injection and configuration via `appsettings.json`
- Entity mapping and migrations with EF Core
- Authentication & authorization (cookie-based or Identity)
- Validation: server-side model validation and client-side unobtrusive validation
- Logging and diagnostics using built-in ASP.NET Core logging
- Automated testing and CI-friendly design

## Payment, access control, and certificates

- Payment integration: The platform provides access-based courses; a course is visible/unlocked to a user only after successful payment.
- Payment gateway: Integration planned with a third-party gateway (example: Stride sandbox). A payment adapter/wrapper is used to connect the gateway API to the app's flow.
- Post-payment actions: On successful payment the system updates purchase records, unlocks access, sends purchase confirmation emails, and triggers certificate generation when course completion conditions are met.
- Certificate generation: When a user completes a course, a PDF certificate is generated server-side and emailed to the user.

## Azure hosting and storage

- Production deployment: the application is published to Azure App Service for hosting.
- Database: production data is hosted on Azure SQL Database (configured in connection strings and appsettings for production).
- Media storage: course videos and large media assets are stored in Azure Blob Storage and accessed via secured URLs/managed identity or SAS tokens.
- Ensure appropriate Azure configuration (CORS, storage access policies, managed identities, and Key Vault for secrets) before production rollout.

## Design patterns

Three primary design patterns identified for the core workflows:

1. **Repository pattern**  
   - Used to abstract data access and communicate with the database via repository interfaces and implementations. Application services call repository methods for persistence operations.  
   - Reference: https://www.geeksforgeeks.org/system-design/repository-design-pattern/

2. **Adapter pattern**  
   - Implemented as a wrapper between the application payment flow and the external Stride payment gateway API to adapt incompatible interfaces and isolate third-party logic.

3. **Observer pattern**  
   - Used to notify interested components when payment state changes (e.g., payment -> successful). Observers handle email notifications, database updates, UI state changes, and other side effects.

> Note: MVC is referenced as part of architecture planning but the implementation is Razor Pages; MVC is not counted as one of the three design patterns.

## Functional requirements

- Users can create and log in to accounts.
- Users can view available courses with descriptions and prices.
- Users can register for selected courses.
- Users can pay for courses via credit cards (payment gateway integration).
- The system unlocks the selected course after successful payment.
- The system sends an email notification upon purchase.
- Admin users can view aggregated purchase statistics and per-course purchase counts.
- Users can add personal notes under their purchased courses.
- The system sends a PDF certificate via email after a user completes a course and marks it complete.
- The system shows a list indicating who joined which course and who created each account.
- Admins can de-enroll any user from a course; an email is sent informing the user that a refund will be processed.

## Non-functional requirements

- Passwords must be securely hashed in the database.
- The UI should be professional, easy to follow, and motivating for learners.
- The system should be highly available and load data quickly.
- Security: use encryption in transit (HTTPS), secure cookie settings, and least-privilege access for different roles.
- Follow secure coding practices when integrating external APIs and handling payments.

## Web APIs and integration notes

- Any RESTful Web APIs used (internal or third-party) should be documented and secured (authentication, rate-limiting, and input validation).
- If the application exposes Web APIs for frontend or third-party integration, use RESTful conventions and proper versioning.
- For payment gateway integration, use sandbox/test environments during development and ensure PCI compliance practices in production.

## Getting started (development)

### Prerequisites

- .NET 10 SDK
- Visual Studio 2026 (Community/Professional/Enterprise) or VS Code
- SQL Server / LocalDB or SQLite

### Run locally

1. Clone the repository
```bash
git clone https://github.com/devanshudave1901/ILearnSmartProject.git
```
2. Open the solution in Visual Studio 2026
3. Update configuration in `appsettings.Development.json` (connection string, secrets, payment gateway sandbox keys)
4. Apply EF Core migrations and seed data:

- Using Package Manager Console:
```powershell
Update-Database
```
- Or using dotnet CLI:
```bash
dotnet ef database update --project ILearnSmartProject
```

5. Run the application (F5) or via CLI:
```bash
dotnet run --project ILearnSmartProject
```

## Running tests

Run unit and integration tests from Visual Studio Test Explorer or using dotnet CLI:

```bash
dotnet test
```

## Configuration

- `appsettings.json` contains default configuration keys (ConnectionStrings, Logging, Authentication settings)
- `appsettings.Development.json` is used for developer overrides
- For secrets (API keys, connection strings), prefer user secrets or environment variables in CI

## Database and migrations

- Use EF Core migrations to evolve schema. Add migrations with:

```bash
dotnet ef migrations add <Name> --project ILearnSmartProject
```

- Apply migrations as part of startup/deploy or via CLI/CI pipeline.

## Security considerations

- Protect sensitive configuration using secrets or environment variables
- Use HTTPS and HSTS in production
- Enforce strong password policies and secure cookie settings
- Validate and sanitize user input; avoid returning raw exceptions to clients

## Performance and scalability

- Use caching (in-memory, distributed) for frequently-read data
- Paginate queries and use efficient indexing in the database
- Offload long-running work to background services (e.g., `IHostedService`)

## Deployment

- Deploy to IIS, Azure App Service, or any container host supporting .NET 10
- CI/CD pipelines should run `dotnet build`, `dotnet test`, and apply DB migrations before swapping/launching

## Contribution guidelines

- Fork the repository and open a pull request with a clear description of changes
- Keep changes small and focused; include tests for new behavior
- Follow the existing coding style and naming conventions

## Troubleshooting

- If migrations fail, ensure the configured connection string points to an available server and the user has permissions
- Use Logging (Console and Debug) to gather runtime errors and stack traces

## License

Specify the applicable open-source license here (MIT, Apache 2.0, etc.). If none, add a LICENSE file to the repository.

## Contact / Maintainers

For questions or issues, open an issue on the repository or contact the maintainers via the project GitHub.

---

This README is intentionally general and intended to be adapted to the specific features implemented in this repository. Replace placeholders (e.g., license, maintainers) with project-specific information.
