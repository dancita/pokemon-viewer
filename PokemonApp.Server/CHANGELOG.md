# Changelog

All notable backend changes to this project are documented in this file.

The format follows [Keep a Changelog](https://keepachangelog.com)  
and adheres to [Semantic Versioning](https://semver.org).

---

## [1.0.0] - 2025-07-10

### Added
- Initial backend project setup with ASP.NET Core Web API.
- Integrated Entity Framework Core with SQL Server.
- Created Pokemon-related models.
- Implemented logic for storing Pokémon abilities and related data.
- Added input validation to API endpoints.
- Configured JWT authentication for API Endpoints.
- Added custom exception handling middleware.

### Changed
- Refactored service structure and dependency injection setup.
- Extracted external URLs to `appsettings.json` for better config management.
- Improved backend and model consistency with `PokemonApiId` support.
- Changed startup and middleware configuration.

### Fixed
- Improved validation logic and error handling throughout the API
- Added various refactorings, including changes to the database schema.