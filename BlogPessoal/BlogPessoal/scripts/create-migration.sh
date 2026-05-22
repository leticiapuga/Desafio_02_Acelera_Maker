#!/usr/bin/env bash
set -euo pipefail

MIGRATION_NAME=${1:-InitialCreate}
dotnet tool install --global dotnet-ef || true
export PATH="$PATH:$HOME/.dotnet/tools"

dotnet ef migrations add "$MIGRATION_NAME" --project BlogPessoal.csproj
dotnet ef database update --project BlogPessoal.csproj
