# ConstructionCompany

Учебный backend-проект на ASP.NET Core Web API для управления объектами строительства и иерархией складов (SupplyHub).

Проект используется для практики архитектуры, работы с Docker, Entity Framework Core, MediatR и JWT-аутентификации.

## Технологии

- .NET 8
- ASP.NET Core Web API
- Entity Framework Core
- SQLite
- MediatR
- JWT Authentication
- AutoMapper
- Docker / Docker Compose
- Swagger (OpenAPI)

## Архитектура

Проект разделён на слои:

- **Presentation** — Web API, контроллеры
- **Application** — CQRS, команды, запросы, DTO
- **Domain** — доменные сущности
- **Persistence** — EF Core, DbContext, репозитории

Используется подход CQRS через MediatR и Repository pattern.

## Функциональность

- Аутентификация и регистрация пользователей (JWT)
- Управление объектами строительства (CRUD)
- Управление иерархией поставок (SupplyHub)
- Ролевая авторизация (Admin / User)
- Swagger UI для тестирования API

## Запуск через Docker
Выбрать Docker Compose для запуска
