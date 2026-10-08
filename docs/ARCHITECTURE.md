# Architecture notes

## Current phase

```text
React / TypeScript
        ↓ HTTP + JSON
ASP.NET Core / C#
        ↓ EF Core + Npgsql
PostgreSQL
```

I chose ASP.NET Core because I want to practice C# on the server and carry that experience into Unity later. React gives the closet a straightforward browser interface. PostgreSQL fits garments with reusable tags and, eventually, outfits with multiple garments.

The API is one project. `Domain` defines records and relationships, `Data` handles EF configuration and migrations, and `Contracts` defines the public response. Endpoint wiring stays in `Program.cs`. I use EF directly rather than wrapping it in a repository that would repeat its query API.

## Data and query behavior

`Garment` stores a UUID, name, category, subtype, optional brand/size/notes, status, and UTC created/updated timestamps. Four lookup tables (`Color`, `Season`, `Occasion`, `StyleTag`) connect through four join tables. Unique lookup names prevent duplicate labels; composite join keys prevent repeated assignments. EF also creates indexes on lookup foreign keys for reverse queries, and category has its own index.

I kept category/subtype as bounded strings for the initial read-only slice. Before adding writes, I will define validation, normalization, and whether those values need their own taxonomy. Status is an enum stored as a readable string. Future write operations must set timestamps explicitly; Phase 1 only reads fixed fixtures.

The list endpoint uses async, cancellation-aware, no-tracking queries. Split queries avoid multiplying rows across four relationship collections. This costs several database round trips, which is acceptable for ten demo records. I will revisit projection/pagination when the dataset and query requirements grow.

The initial migration includes the fictional dataset. It is repeatable through EF's migration history and does not seed on every API start. A private environment will need a schema-only baseline or separate fixture mechanism before its first migration; I will not apply public demo fixtures blindly to a real wardrobe.

## API contract

`GET /api/garments` returns an alphabetically ordered array of explicit `GarmentResponse` DTOs. IDs are UUID strings, timestamps are ISO 8601, status is a string, and multivalued attributes are string arrays. Unspecified optional fields are null. No EF navigation objects or join-table details cross the boundary.

`GET /api/health` checks connectivity and returns only `{ "status": "healthy" }` or `{ "status": "unavailable" }`, with HTTP 200 or 503. Connectivity does not guarantee every migration has been applied; migrations remain an explicit setup step.

Development Swagger lives at `/swagger`; its OpenAPI document is `/swagger/v1/swagger.json`. TypeScript mirrors the DTO manually for now. I considered `openapi-typescript` for types and NSwag/OpenAPI Generator for clients. A generator becomes useful once more endpoints make drift costly; I can add a pinned tool and a CI regeneration check then. The current DTO is deliberately language-neutral for Unity.

## Configuration and trust boundaries

The API's connection string comes from environment configuration, never committed credentials. Local PostgreSQL binds to loopback. Development CORS permits the fixed Vite origin. Production will require explicit origins or same-origin routing and a deployment-specific connection string.

`VITE_API_BASE_URL` is public build configuration, not a secret. Empty configuration uses relative URLs. The page does not hold garment fixtures; browser smoke tests verify data fetched through the API.

## Future clients

```text
React wardrobe client ──↘
                         REST API → PostgreSQL
Unity dress-up client ──↗
```

Unity comes later because the data/API foundation needs to work before rendering or gameplay depends on it. Both clients will share garment identity and metadata through the API. Future outfits can reference garments through a separate many-to-many relationship without changing garment IDs. Game assets and equipment slots will be modeled when the Unity slice needs them.

Real wardrobe data will be separate from public demo data. No import, private URL, personal size, or photograph belongs in the public fixtures. A later Notion/CSV import will validate and transform a private export into the stable production schema.

## Verification

xUnit uses the actual PostgreSQL provider, migrations, and ASP.NET test host. The test database must end in `_test` and contains only fixtures. Testing Library covers asynchronous browser UI states. Playwright covers the real API-to-browser path and connection-failure feedback. GitHub Actions repeats those checks with disposable PostgreSQL services.
