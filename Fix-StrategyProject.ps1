# Fix-StrategyProject.ps1
# Purpose: Align EF Core versions across all projects in AlgoDDD solution

$solutionRoot = "C:\Algo-DDD"

Write-Host "Aligning EF Core versions across all projects in $solutionRoot..."

# List of projects to update
$projects = @(
    "$solutionRoot\CoreContexts\Strategy\src\AlgoDDD.Strategy\AlgoDDD.Strategy.csproj",
    "$solutionRoot\SupportingContexts\MarketData\src\AlgoDDD.MarketData\AlgoDDD.MarketData.csproj",
    "$solutionRoot\SupportingContexts\IdentityAccess\src\AlgoDDD.IdentityAccess\AlgoDDD.IdentityAccess.csproj",
    "$solutionRoot\SharedKernel\src\AlgoDDD.SharedKernel\AlgoDDD.SharedKernel.csproj"
)

foreach ($proj in $projects) {
    Write-Host "Updating EF Core packages in $proj..."
    cd (Split-Path $proj)

    # Remove any existing EF Core packages
    dotnet remove package Microsoft.EntityFrameworkCore
    dotnet remove package Microsoft.EntityFrameworkCore.Design
    dotnet remove package Microsoft.EntityFrameworkCore.Relational
    dotnet remove package Microsoft.EntityFrameworkCore.InMemory

    # Add unified EF Core 8.0.11 packages
    dotnet add package Microsoft.EntityFrameworkCore --version 8.0.11
    dotnet add package Microsoft.EntityFrameworkCore.Design --version 8.0.11
    dotnet add package Microsoft.EntityFrameworkCore.Relational --version 8.0.11
    dotnet add package Microsoft.EntityFrameworkCore.InMemory --version 8.0.11
}

# Rebuild solution
cd $solutionRoot
dotnet clean
dotnet restore
dotnet build
dotnet test

Write-Host "EF Core aligned to 8.0.11 across all projects. Build and tests executed."
