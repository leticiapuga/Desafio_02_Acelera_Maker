#!/usr/bin/env bash
set -euo pipefail

if [ -z "${SONAR_TOKEN:-}" ]; then
  echo "Defina a variável SONAR_TOKEN antes de executar a análise."
  exit 1
fi

dotnet tool install --global dotnet-sonarscanner || true
export PATH="$PATH:$HOME/.dotnet/tools"

dotnet sonarscanner begin /k:"blog-pessoal-dotnet" /d:sonar.host.url="http://localhost:9000" /d:sonar.login="$SONAR_TOKEN" /d:sonar.cs.opencover.reportsPaths="coverage/**/coverage.opencover.xml"
dotnet build
dotnet test BlogPessoal.Tests/BlogPessoal.Tests.csproj /p:CollectCoverage=true /p:CoverletOutputFormat=opencover /p:CoverletOutput=../coverage/
dotnet sonarscanner end /d:sonar.login="$SONAR_TOKEN"
