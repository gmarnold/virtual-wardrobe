# Virtual Wardrobe

I'm building Virtual Wardrobe because I wanted a better way to organize my actual closet, plan outfits, and try combinations before pulling everything off a hanger.

The project is intended to be both a practical wardrobe tool and a dress-up game. The web app will manage garments and outfits; a Unity client will eventually turn the same data into a playful styling experience.

## Current status

Phase 1 is a small complete vertical slice: PostgreSQL → ASP.NET Core → `GET /api/garments` → React garment cards. The demo contains ten fictional garments. The page handles loading, empty results, failed requests, and retrying.

**Unity client — planned for Phase 3.** No Unity project or private wardrobe integration exists yet.

## Architecture and tech stack

```text
React / TypeScript / Vite
          ↓ REST + JSON
ASP.NET Core / C# / EF Core
          ↓
PostgreSQL
```

I chose a single API project with clear domain, persistence, and contract folders. This gives each concern a place without adding layers that have no job yet. See [architecture notes](docs/ARCHITECTURE.md) and the [decision log](docs/DECISIONS.md).

Toolchain used: Node 22.19.0, npm 10.9.3, .NET SDK 10.0.401, PostgreSQL 18, React 19, TypeScript 6, and Vite 8. Backend tests use xUnit and a real PostgreSQL database; frontend tests use Vitest and Testing Library; browser smoke tests use Playwright. Dependency versions are recorded in the project files and npm lockfile.

## Repository layout

```text
apps/api/        ASP.NET Core API, domain, DTOs, EF configuration and migrations
apps/web/        React client and frontend/browser tests
tests/api/       API integration and serialization tests
docs/            Architecture, decisions, and Phase 1 verification notes
scripts/         Local database/API commands
.github/         CI workflow
```

## Running locally

Prerequisites: Node 22.19+ in a supported release, npm, .NET 10 SDK, and running Docker Desktop. The SDK patch is pinned in `global.json`; Node is recorded in `.nvmrc`. With nvm installed, run `nvm install` and `nvm use` at the repository root.

1. Create local configuration:

   ```bash
   cp .env.example .env
   cp apps/web/.env.example apps/web/.env
   openssl rand -hex 24
   ```

   Paste the generated value after `POSTGRES_PASSWORD=` in `.env`. A hexadecimal password works directly with the shell helper and PostgreSQL connection string. These files are ignored by Git. The helper sources the root `.env` as shell configuration, so use only trusted local assignments.

2. Start PostgreSQL and apply migrations:

   ```bash
   bash scripts/wardrobe.sh db
   bash scripts/wardrobe.sh migrate
   ```

3. Start the API in one terminal:

   ```bash
   bash scripts/wardrobe.sh api
   ```

4. Start the web app in another terminal:

   ```bash
   cd apps/web
   npm ci
   npm run dev
   ```

Open [My Closet](http://localhost:5173). Swagger is at [localhost:5080/swagger](http://localhost:5080/swagger). Use **Try it out** on `GET /api/garments`, or visit [the JSON endpoint](http://localhost:5080/api/garments) directly. [Health](http://localhost:5080/api/health) returns HTTP 200 when the database connects and HTTP 503 otherwise.

Stop the API with Ctrl+C and reload the web page to see the error state. Restart the API and choose **Try again** to recover. Stop PostgreSQL with `docker compose stop`; this preserves its data.

On macOS, if `dotnet` is not on PATH, the helper finds `/usr/local/share/dotnet/dotnet`. For direct SDK commands, reopen Terminal after installation or use that full path. `DOTNET_BIN` can also specify a different installation.

## Configuration

The API reads `ConnectionStrings__Wardrobe`. The helper builds it from the ignored root `.env`. When running without the helper, set that environment variable explicitly using a PostgreSQL connection string.

The frontend reads `VITE_API_BASE_URL` at build time. The development example points to port 5080. If omitted, requests use the same origin; production can use a reverse proxy or a build-specific public API origin. Browser configuration must never contain secrets.

Development CORS permits only `http://localhost:5173`. The Vite server uses a fixed port so it cannot silently move to a disallowed origin. Production must explicitly configure `Cors__AllowedOrigins__0` (and additional numbered entries as needed), or use same-origin hosting. Swagger is available only in Development.

## Database and migrations

`Garment` uses UUIDs, scalar metadata, a string-backed status enum, and UTC timestamps. Colors, seasons, occasions, and style tags use lookup tables and many-to-many join tables. Lookup names are unique; joins have composite keys and reverse lookup indexes. Category is indexed. No tags are stored as comma-separated text.

The initial EF migration creates the schema and inserts synthetic fixtures with fixed IDs and timestamps. Applying it repeatedly does not duplicate the seed records. The API never migrates the database on startup; schema changes remain explicit.

To add a migration after changing the model:

```bash
# Load the same local configuration used by the helper.
set -a
source .env
set +a
export ConnectionStrings__Wardrobe="Host=localhost;Port=$POSTGRES_PORT;Database=$POSTGRES_DB;Username=$POSTGRES_USER;Password=$POSTGRES_PASSWORD"
dotnet tool restore
dotnet ef migrations add DescribeTheChange --project apps/api
bash scripts/wardrobe.sh migrate
```

**Development reset — deletes all data in this project's local Compose volume:**

```bash
bash scripts/wardrobe.sh reset-development --confirm-development-reset
bash scripts/wardrobe.sh db
bash scripts/wardrobe.sh migrate
```

This is separate from the normal migration command. It also deletes the local test database. Do not point these development tools at private production data.

## Testing

From the root, with PostgreSQL running:

```bash
bash scripts/wardrobe.sh test-api
dotnet build --configuration Release
```

The helper creates `virtual_wardrobe_test`, supplies `TEST_DATABASE_CONNECTION`, and runs the backend tests. The fixture applies actual migrations and refuses database names without the `_test` suffix. For an externally managed test database, create it first and set `TEST_DATABASE_CONNECTION` before running `dotnet test`.

From `apps/web`:

```bash
npm run lint
npm run typecheck
npm run test
npm run build
npx playwright install chromium
npm run test:e2e
```

Before browser tests, start PostgreSQL and run migrations. Playwright starts the API and Vite if they are not already running, verifies all ten cards through the real API, and checks the browser error state when a request fails.

CI runs on pushes to main and pull requests. PostgreSQL service containers support backend tests and a browser smoke test. CI credentials exist only for disposable service databases and require no repository secrets.

## Phase 1 scope and limitations

This phase provides a read-only list and health endpoint, development API documentation, demo cards, migrations, tests, and CI. There is no authentication, filtering UI, pagination, garment editing, outfit management, upload flow, or production deployment yet. The visual design is an early cream/lavender direction; cards use typographic placeholders rather than garment photographs. Product naming lives mainly in the web configuration and page metadata.

## Roadmap

- Phase 2: start with API-backed category/color/season filters and document the query contract; then add garment management and outfit modeling as the next slice requires.
- Later: import my existing wardrobe from Notion/CSV after the production schema is stable. A private validation/transformation script will map the export into PostgreSQL.
- Phase 3: add a Unity client consuming the same REST API, followed by clothing assets, layering, saved outfits, and styling challenges.

## Privacy and demo data

All tracked garments, brands, notes, and tags are fictional demo fixtures. There are no personal sizes, purchase records, private Notion URLs or IDs, photographs, or wardrobe exports. My real wardrobe will live separately from public fixture data. Environment files, local state, and private export directories are ignored.
