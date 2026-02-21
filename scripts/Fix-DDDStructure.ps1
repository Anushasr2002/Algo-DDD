# Root path of your project
$root = "C:\Algo-DDD\CoreContexts\Strategy\src\AlgoDDD.Strategy"

# Ensure Domain subfolders exist
$domainPath = Join-Path $root "Domain"
$entitiesPath = Join-Path $domainPath "Entities"
$valueObjectsPath = Join-Path $domainPath "ValueObjects"
$servicesPath = Join-Path $domainPath "Services"
$infraServicesPath = Join-Path $root "Infrastructure\Services"

New-Item -ItemType Directory -Force -Path $entitiesPath
New-Item -ItemType Directory -Force -Path $valueObjectsPath
New-Item -ItemType Directory -Force -Path $servicesPath
New-Item -ItemType Directory -Force -Path $infraServicesPath

# Move and rename files into correct folders
# BacktestResult
Get-ChildItem -Path $root -Recurse -Filter "BackTestResult.cs" | ForEach-Object {
    Move-Item $_.FullName (Join-Path $entitiesPath "BacktestResult.cs") -Force
}

# Signal
Get-ChildItem -Path $root -Recurse -Filter "Signal.cs" | ForEach-Object {
    Move-Item $_.FullName (Join-Path $entitiesPath "Signal.cs") -Force
}

# StrategyEntity
Get-ChildItem -Path $root -Recurse -Filter "StrategyEntity.cs" | ForEach-Object {
    Move-Item $_.FullName (Join-Path $entitiesPath "StrategyEntity.cs") -Force
}

# StrategyId (ValueObject)
Get-ChildItem -Path $root -Recurse -Filter "StrategyId.cs" | ForEach-Object {
    Move-Item $_.FullName (Join-Path $valueObjectsPath "StrategyId.cs") -Force
}

# StrategyEngine
Get-ChildItem -Path $root -Recurse -Filter "StrategyEngine.cs" | ForEach-Object {
    Move-Item $_.FullName (Join-Path $servicesPath "StrategyEngine.cs") -Force
}

# IMarketDataProvider
Get-ChildItem -Path $root -Recurse -Filter "IMarketDataProvider.cs" | ForEach-Object {
    Move-Item $_.FullName (Join-Path $servicesPath "IMarketDataProvider.cs") -Force
}

# InMemoryMarketDataProvider
Get-ChildItem -Path $root -Recurse -Filter "InMemoryMarketDataProvider.cs" | ForEach-Object {
    Move-Item $_.FullName (Join-Path $infraServicesPath "InMemoryMarketDataProvider.cs") -Force
}

Write-Host "DDD folder structure fixed successfully!"
