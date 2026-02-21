# Root path of your Strategy project
$root = "C:\Algo-DDD\CoreContexts\Strategy\src\AlgoDDD.Strategy"

# --- Step 1: Ensure correct folder structure ---
$domainPath = Join-Path $root "Domain"
$entitiesPath = Join-Path $domainPath "Entities"
$valueObjectsPath = Join-Path $domainPath "ValueObjects"
$servicesPath = Join-Path $domainPath "Services"
$infraServicesPath = Join-Path $root "Infrastructure\Services"

New-Item -ItemType Directory -Force -Path $entitiesPath
New-Item -ItemType Directory -Force -Path $valueObjectsPath
New-Item -ItemType Directory -Force -Path $servicesPath
New-Item -ItemType Directory -Force -Path $infraServicesPath

# --- Step 2: Move and rename files into correct folders ---
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

# --- Step 3: Fix namespaces automatically ---
$baseNamespace = "AlgoDDD.Strategy.Domain"

Get-ChildItem -Path $domainPath -Recurse -Filter *.cs | ForEach-Object {
    $file = $_.FullName
    $content = Get-Content $file

    if ($file -like "*\Entities\*") {
        $expected = "$baseNamespace.Entities"
    } elseif ($file -like "*\ValueObjects\*") {
        $expected = "$baseNamespace.ValueObjects"
    } elseif ($file -like "*\Services\*") {
        $expected = "$baseNamespace.Services"
    } else {
        $expected = $baseNamespace
    }

    $newContent = $content | ForEach-Object {
        if ($_ -match "namespace ") {
            "namespace $expected"
        } else {
            $_
        }
    }

    Set-Content -Path $file -Value $newContent -Encoding UTF8
    Write-Host "✅ Fixed namespace in $file → namespace $expected"
}

Write-Host "🎉 DDD folder structure and namespaces fixed successfully!"
