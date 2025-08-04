# TU Runner – UniMap Auth Service

This repository contains the user management and authentication service for the TUBify app, developed during the PP3S project at TU Berlin. The service handles user-related operations such as login, registration, and saving user preferences (e.g., favorite meals and places).

## Tech Stack

- **Language**: C# (.NET 9)
- **Framework**: ASP.NET Core Web API
- **Architecture**: MVC (Controllers, Services, Models)
- **Database**: PostgreSQL (required, runs via Docker Compose)
- **Containerization**: Docker, Docker Compose

## Project Structure

```
.
├── Contracts/         # DTOs and request/response contracts
├── Controllers/       # API endpoint definitions
├── Data/              # DB context
├── Entities/          # DB models
├── Modells/           # Business models
├── Services/          # Application logic
├── Extensions/        # Custom extensions / helpers
├── Migrations/        # EF Core migrations
├── Properties/        # Project properties
├── appsettings*.json  # Environment configuration
├── Dockerfile         # Image definition
├── docker-compose.yml # Compose file for API
```

## Running the Service

1. Ensure Docker and Docker Compose are installed.
2. Configure environment variables in `.env` or directly in [`docker-compose.yml`](docker-compose.yml).
3. Start the service:

    ```sh
    docker compose up -d --build
    ```

> Backend will be available at: [http://localhost:5000](http://localhost:5000)  
> Swagger UI: [http://localhost:5000/swagger/index.html](http://localhost:5000/swagger/index.html)

## Configuration

App settings are stored in:

* [`appsettings.json`](appsettings.json)
* [`appsettings.Development.json`](appsettings.Development.json)
* `.env` (for DB credentials or secrets)

Environment variables used (see [`docker-compose.yml`](docker-compose.yml)):

- `DB_CONNECTION_STRING`
- `JWT_SECRET_KEY`
- `JWT_EXPIRES_HOURS`
- `JWT_REFRESH_TOKEN_EXPIRES_DAYS`

Example DB connection string:

```
Host=localhost;Port=5432;Database=unimap_auth;Username=postgres;Password=your_password
```

## API Endpoints

Main endpoints (see [Controllers](Controllers/)):

* `POST /api/Users/register` – Register a new user
* `POST /api/Users/login` – Authenticate user and issue token
* `GET /api/Users/getUser` – Get user profile and favorites
* `POST /api/Users/addFavouriteMeal` – Add favorite meal
* `DELETE /api/Users/removeFavouriteMeal/{id}` – Remove favorite meal
* `POST /api/FavoritePlaces/add` – Add favorite place
* `GET /api/FavoritePlaces` – Get favorite places
* `DELETE /api/FavoritePlaces/{favoriteId}` – Remove favorite place
* `GET /api/StudyProgram` – Get study programs
* `POST /api/Tokens/refreshToken` – Refresh JWT token

> Full API documentation available at `/swagger/index.html` when running the service.

## Notes

* Built as a microservice for the larger TUBify ecosystem
* Intended to be run alongside other backend services (e.g., routing)
* Uses EF Core for database migrations (see [`Migrations`](Migrations/))
* Based on MVC principles and DTO separation

---

## Contributors

Developed by the TU Berlin **PP3S Project Group D** in Summer Semester 2025
