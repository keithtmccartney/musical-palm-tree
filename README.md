# musical-palm-tree
[C#] .NET Technical Task

# Notes
* Adheres to string validation, data submission, and data search functionality
* SOLID-focused, DRY-practiced .NET 8 MVC application over Sqlite with code-first database creation. No API requirements, no XSS concerns, EF-managed so SQL-injection concerns are considerably lifted.
* Clone, build, validate, submit, search

# Unit Tests
* N/A, no requirement as out of scope

# Postman Collection
* N/A, MVC-only, no requirement for Web API due to minimal scope

# Migrations
* EF via Sqlite (see appsettings configuration and Infrastructure-layer dependency injection), stand-alone local database created on project load

# Future plans
* Replace with Web API and introduce Angular/React front-end (though for nature of application, Blazor is also a suitable front-end), currently MVC over jQuery, Razor-pages will suffice with scope as an ASP.NET Core solution
* MSTest project implementation through Shouldly and Moq
* CICD YAML pipeline write-up either through GitHub Actions or Azure
