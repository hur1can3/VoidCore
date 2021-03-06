[CmdletBinding()]
param(
  [switch] $IncludeRecommended
)

dotnet tool install --global dotnet-format
dotnet tool update --global dotnet-format
dotnet tool install --global dotnet-reportgenerator-globaltool
dotnet tool update --global dotnet-reportgenerator-globaltool
dotnet tool install --global InheritDocTool
dotnet tool update --global InheritDocTool

if (-not $IncludeRecommended) {
  Exit $LASTEXITCODE
}

dotnet tool install --global nbgv
dotnet tool update --global nbgv
