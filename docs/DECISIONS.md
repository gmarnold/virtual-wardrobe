# Engineering decisions

These are the choices I want to be able to explain as the project develops.

## 1. ASP.NET Core API

- **Decision:** C# and ASP.NET Core instead of a Node API.
- **Context:** I want a web application now and a Unity client later.
- **Options considered:** ASP.NET Core; Node/TypeScript.
- **Why I chose this:** It gives me practical C# server experience and a strongly typed API contract.
- **Tradeoffs:** Two language toolchains rather than TypeScript everywhere.
- **Revisit:** If operating the API outweighs the value of the C# stack; there is no immediate reason to change.

## 2. PostgreSQL

- **Decision:** Relational storage from the first slice.
- **Context:** Garments share tags and will participate in many outfits.
- **Options considered:** PostgreSQL; SQLite; document storage.
- **Why I chose this:** Relationships, constraints, and filtering belong naturally in SQL. Local and CI tests use the production database engine.
- **Tradeoffs:** Docker is required for the documented local workflow.
- **Revisit:** Hosting constraints or measured operational needs, rather than a preference for fewer setup steps.

## 3. React management UI, Unity deferred

- **Decision:** React/TypeScript for management; Unity starts in Phase 3.
- **Context:** The first milestone is real database-backed cards in a browser.
- **Options considered:** Browser first; Unity first; both together.
- **Why I chose this:** I can stabilize data and API behavior before game assets and rendering introduce another set of problems.
- **Tradeoffs:** This phase provides no dress-up gameplay.
- **Revisit:** Once wardrobe/outfit operations are usable and the next slice benefits from Unity.

## 4. One API project

- **Decision:** Domain, data, and contracts are folders in one project.
- **Context:** A read-only list does not justify several deployment units or libraries.
- **Options considered:** One project; separate Domain/Infrastructure/API projects.
- **Why I chose this:** Responsibilities are visible without dependency plumbing or empty abstractions.
- **Tradeoffs:** Folder boundaries cannot prevent all coupling at compile time.
- **Revisit:** When independent domain logic, additional hosts, or accidental coupling makes extraction useful.

## 5. UUID garment identity

- **Decision:** UUIDs for garments; integer keys for small lookup tables.
- **Context:** Imports, multiple clients, and future offline game metadata need stable references.
- **Options considered:** Sequential integers; random UUIDs; time-ordered UUIDs.
- **Why I chose this:** UUIDs can be assigned independently of a database sequence. Demo IDs are fixed; future creations can use `Guid.NewGuid()`.
- **Tradeoffs:** Larger indexes and poorer insertion locality than integer/time-ordered keys. IDs do not provide authorization.
- **Revisit:** Measured insertion/index pressure or actual offline synchronization requirements.

## 6. Separate lookup tables and joins

- **Decision:** Colors, seasons, occasions, and styles are relational lookups with many-to-many joins.
- **Context:** Filters and future challenge rules need individual values.
- **Options considered:** Enums; database arrays; lookup tables; comma-separated strings.
- **Why I chose this:** Lookups support reusable names, unique constraints, and reverse queries. Seasons follow the same query model as the extensible tags.
- **Tradeoffs:** More tables and joins; integer lookup IDs are local implementation details. Category/subtype remain strings until write validation establishes their taxonomy.
- **Revisit:** If season semantics become truly fixed, or localization/tag metadata requires richer lookup records.

## 7. Public synthetic fixtures

- **Decision:** Ten fictional garments in the initial migration; no real wardrobe import.
- **Context:** This repository is public, while my closet is private.
- **Options considered:** Real exports; fixtures; an empty database.
- **Why I chose this:** Fixtures make a clone immediately demonstrable without exposing personal information.
- **Tradeoffs:** Migration fixtures are convenient now but must be separated from the schema baseline before introducing a private environment.
- **Revisit:** Before production schema stabilization and the private Notion/CSV import.

## 8. Explicit DTOs and manual TypeScript contract

- **Decision:** Publish OpenAPI and maintain the small TypeScript interface manually.
- **Context:** There are only two endpoints, but both future clients need a clean contract.
- **Options considered:** Manual types; `openapi-typescript`; NSwag/OpenAPI Generator clients.
- **Why I chose this:** A short interface keeps the initial workflow understandable. Mapping and serialization tests protect the server boundary.
- **Tradeoffs:** No automated guarantee against frontend type drift; compile-time types do not validate incoming JSON at runtime.
- **Revisit:** Add generated types/clients and a CI drift check as endpoint count or client complexity increases.

## 9. PostgreSQL integration tests

- **Decision:** Use a dedicated PostgreSQL test database, not EF's in-memory provider.
- **Context:** Migrations and relationship queries are central to the milestone.
- **Options considered:** In-memory EF; SQLite; PostgreSQL service; per-test containers.
- **Why I chose this:** The real provider tests the actual schema, seed migration, joins, and HTTP responses with little extra infrastructure.
- **Tradeoffs:** Tests need an available database. They reuse a fixture database and are read-only; they are not yet designed for competing write-test suites.
- **Revisit:** Before adding write operations, use isolated databases/cleanup so tests cannot affect one another.
