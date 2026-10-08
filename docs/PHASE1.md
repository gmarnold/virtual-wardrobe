# Phase 1 report

I have verified the complete PostgreSQL → API → React path locally.

## Repository and history

Public repository: [gmarnold/virtual-wardrobe](https://github.com/gmarnold/virtual-wardrobe).

| Commit | Responsibility |
| --- | --- |
| `4a2f693` | Initialize main, ignore local/private/generated state, configure formatting |
| `0fb9c05` | Garment domain, lookup relationships, demo seed migration, Compose |
| `284f68b` | Garment/health endpoints and development Swagger |
| `2d8c6b9` | Configurable API-backed React cards and UI states |
| `925a7a0` | API, serialization, UI, and browser tests; local command helper |
| `b01d5bb` | CI with PostgreSQL services and frontend/backend checks |
| `docs: document architecture and local setup` | README, architecture notes, decisions, this report |

All commits use my existing Git identity. The documentation commit can be found by its message in `git log --oneline`.

## Stack and structure

React/TypeScript/Vite → ASP.NET Core/C#/EF Core → PostgreSQL. Unity is planned, with no code in this phase.

`apps/api` holds the API, domain, contracts, persistence, and migrations. `apps/web` holds the browser client and UI/browser tests. `tests/api` holds backend tests. `scripts`, `docs`, and `.github/workflows` contain local commands, notes, and CI.

| Tool | Verified version |
| --- | --- |
| Node / npm | 22.19.0 / 10.9.3 |
| .NET SDK / EF Core | 10.0.401 / 10.0.12 |
| PostgreSQL | 18 image |
| Npgsql EF provider / Swagger | 10.0.3 / 10.3.0 |
| React / TypeScript / Vite | 19.3.0 / 6.0.3 / 8.3.3 |
| Vitest / jsdom / Playwright | 5.0.3 / 27.4.0 / 1.64.0 |
| Docker / Compose | 29.8.2 / 5.5.1 |

## Schema and interview decisions

- `Garment`: UUID ID, name, category/subtype, nullable brand/size/notes, string-backed status, UTC created/updated timestamps.
- `Color`, `Season`, `Occasion`, `StyleTag`: integer keys and unique bounded names.
- `GarmentColor`, `GarmentSeason`, `GarmentOccasion`, `GarmentStyleTag`: many-to-many joins with composite keys and lookup indexes.
- Ten fictional garments with deterministic identities and relationship assignments.

UUIDs leave room for imports and independent clients. Relational tags keep future filters and challenge rules queryable. One API project limits ceremony. DTOs keep persistence separate from the JSON contract. PostgreSQL integration tests exercise the actual provider and migration. The compact [decision log](DECISIONS.md) records alternatives, tradeoffs, and revisit conditions.

## Run and inspect

Create ignored environment files and set a local password as described in the [README](../README.md). Then, from the root:

```bash
bash scripts/wardrobe.sh db
bash scripts/wardrobe.sh migrate
bash scripts/wardrobe.sh api
```

In another terminal:

```bash
cd apps/web
npm ci
npm run dev
```

- Web: `http://localhost:5173`
- Swagger: `http://localhost:5080/swagger`
- `GET /api/garments`: ten database-backed DTOs, sorted by name
- `GET /api/health`: safe status, HTTP 200/503
- OpenAPI: `http://localhost:5080/swagger/v1/swagger.json` in Development

## Tests and verification

| Check | Result |
| --- | --- |
| Fresh database migration and seed | Passed |
| Repeat migration | No changes; already current |
| `bash scripts/wardrobe.sh test-api` | 4 passed |
| `dotnet build --configuration Release` | Passed, zero warnings/errors |
| `npm run test` | 5 passed |
| `npm run lint`, `npm run typecheck`, `npm run build` | Passed |
| `npm run test:e2e` | 2 passed: real ten-card flow and connection-error feedback |
| Live API, health, Swagger page/document | HTTP 200 |
| CORS origin check | Local Vite allowed; unrelated origin omitted |
| Desktop/mobile visual review | Cards readable; no mobile horizontal overflow |
| Repository audit | No local secrets, private fixture data, exports, photos, or private Notion references |

CI runs backend restore/build/test and frontend install/lint/typecheck/test/build/browser checks. It uses disposable PostgreSQL services and test-only credentials. The live run status is available in [GitHub Actions](https://github.com/gmarnold/virtual-wardrobe/actions).

## Current limits and next task

The app is read-only and unauthenticated. It has no filters, writes, outfits, uploads, production deployment, Notion import, or Unity client yet. Frontend types mirror the DTO manually. Health checks connectivity rather than migration completeness. Public seed fixtures must be separated from the schema baseline before private production data is introduced.

My recommended Phase 2 first task is a small category/color/season filtering slice: define query parameters, test filtered PostgreSQL results, and add matching browser controls. That exercises the relationship design while keeping the next milestone small.
