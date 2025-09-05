[readme-api.md](https://github.com/user-attachments/files/22132949/readme-api.md)
# **Simple CRM \- Backend API**

This project is a robust, RESTful API built with .NET 8 and Clean Architecture. It serves as the backend for the Simple CRM application, handling all data management, business logic, and user authentication.

## **✨ Features**

* **Secure Authentication:** Uses ASP.NET Core Identity with JWT for secure, token-based authentication.  
* **Role-Based Authorization:** Supports 'Admin' and 'User' roles to protect endpoints.  
* **Clean Architecture:** A well-structured solution with separate projects for Domain, Application, Infrastructure, and API layers, ensuring separation of concerns and maintainability.  
* **Full CRUD Operations:** Provides complete Create, Read, Update, and Delete functionality for Contacts, Companies, and Deals.  
* **CQRS Pattern:** Uses the MediatR library to separate commands (writes) from queries (reads).  
* **Unit of Work & Generic Repository:** Implements a clean data access layer for efficient and transactional database operations.  
* **Dashboard Analytics:** A dedicated endpoint provides aggregated data for a dashboard view.

## **🛠️ Tech Stack**

* **Framework:** .NET 8, ASP.NET Core Web API  
* **Architecture:** Clean Architecture, CQRS  
* **Database:** PostgreSQL  
* **ORM:** Entity Framework Core 8  
* **Authentication:** ASP.NET Core Identity, JWT (JSON Web Tokens)  
* **Dependency Injection:** MediatR

## **✅ Prerequisites**

Before you begin, ensure you have the following installed on your machine:

* **.NET 8 SDK:** [Download .NET 8](https://dotnet.microsoft.com/download/dotnet/8.0)  
* **PostgreSQL Server:** A running instance of PostgreSQL (version 14 or higher is recommended). This can be installed locally or run via Docker.  
* **Code Editor:** Visual Studio 2022, JetBrains Rider, or Visual Studio Code.

## **🚀 Setup and Installation**

1. **Clone the repository:**  
   git clone \<your-repo-url\>

2. Configure Application Secrets:  
   Open the src/Crm.Api/appsettings.json file. You must update the following sections:  
   * **ConnectionStrings**: Point the DefaultConnection to your local PostgreSQL database.  
   * **Jwt**: Provide a long, secret, and unique string for the Key. The Issuer should match the URL your API runs on (e.g., https://localhost:7123).
```
{  
  "ConnectionStrings": {  
    "DefaultConnection": "Server=localhost;Port=5432;Database=your\_crm\_db\_name;User Id=postgres;Password=your\_password;"  
  },  
  "Jwt": {  
    "Issuer": "https://localhost:7123",  
    "Audience": "http://localhost:4200",  
    "Key": "YOUR\_SUPER\_LONG\_AND\_SECRET\_KEY\_GOES\_HERE"  
  }  
}
```

3. Restore Dependencies:  
   Open a terminal in the root solution folder (where the .sln file is) and run:
   ```
   dotnet restore
    ```
   
4. Run Database Migrations:  
   This command will create all the necessary tables in your database.  
   ```
   dotnet ef database update \--startup-project src/Crm.Api
   ```

5. Run the Application:  
   The application will seed the database with an 'Admin' and 'User' role, and create a default super admin (admin@example.com).
   ```
   dotnet run \--project src/Crm.Api/Crm.Api.csproj
   ```

7. The API will now be running, and you can access the Swagger UI at https://localhost:\<your\_port\>/swagger to test the endpoints.
