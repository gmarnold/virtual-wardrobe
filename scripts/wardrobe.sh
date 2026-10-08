#!/usr/bin/env bash
set -euo pipefail
cd "$(dirname "$0")/.."
if [[ ! -f .env ]]; then
  echo "Copy .env.example to .env and set POSTGRES_PASSWORD first." >&2
  exit 1
fi
set -a
source .env
set +a
: "${POSTGRES_PASSWORD:?Set POSTGRES_PASSWORD in .env}"
export ConnectionStrings__Wardrobe="Host=localhost;Port=${POSTGRES_PORT:-5432};Database=$POSTGRES_DB;Username=$POSTGRES_USER;Password=$POSTGRES_PASSWORD"
DOTNET_BIN="${DOTNET_BIN:-dotnet}"
if ! command -v "$DOTNET_BIN" >/dev/null && [[ -x /usr/local/share/dotnet/dotnet ]]; then
  DOTNET_BIN=/usr/local/share/dotnet/dotnet
fi
case "${1:-help}" in
  db) docker compose up -d --wait ;;
  migrate) "$DOTNET_BIN" restore apps/api; "$DOTNET_BIN" tool restore; "$DOTNET_BIN" ef database update --project apps/api ;;
  api) exec "$DOTNET_BIN" run --project apps/api --launch-profile http ;;
  test-api)
    if ! docker compose exec -T postgres sh -c 'psql -U "$POSTGRES_USER" -d postgres -Atc "SELECT datname FROM pg_database"' | grep -qx virtual_wardrobe_test; then
      docker compose exec -T postgres sh -c 'createdb -U "$POSTGRES_USER" virtual_wardrobe_test'
    fi
    export TEST_DATABASE_CONNECTION="Host=localhost;Port=${POSTGRES_PORT:-5432};Database=virtual_wardrobe_test;Username=$POSTGRES_USER;Password=$POSTGRES_PASSWORD"
    exec "$DOTNET_BIN" test
    ;;
  reset-development)
    if [[ "${2:-}" != --confirm-development-reset ]]; then
      echo "This deletes the local Compose database volume. Run reset-development --confirm-development-reset to proceed." >&2
      exit 1
    fi
    docker compose down --volumes
    ;;
  *) echo "Usage: bash scripts/wardrobe.sh {db|migrate|api|test-api|reset-development --confirm-development-reset}" ;;
esac
